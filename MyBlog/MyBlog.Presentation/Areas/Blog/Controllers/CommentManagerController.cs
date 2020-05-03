using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLogicLayer.CommentServices;
using MyBlog.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.Presentation.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Authorize(Roles ="Administrator, Manager")]
    public class CommentManagerController : Controller
    {
        private readonly ICommentServices _commentServices;

        public CommentManagerController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ContentSortParm"] = string.IsNullOrEmpty(sortOrder) ? "content_desc" : "";
            ViewData["UsernameSortParm"] = sortOrder == "Username" ? "username_desc" : "Username";
            ViewData["PostTitleSortParm"] = sortOrder == "PostTitle" ? "postTitle_desc" : "PostTitle";
            ViewData["CreatedDateSortParm"] = sortOrder == "CreatedDate" ? "createdDate_desc" : "CreatedDate";
            ViewData["ModifiedDateSortParm"] = sortOrder == "ModifiedDate" ? "modifiedDate_desc" : "ModifiedDate";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Comment, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Content.Contains(searchString) || c.Post.Title.Contains(searchString) || c.User.UserName.Contains(searchString);
            }

            Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null;

            switch (sortOrder)
            {
                case "content_desc":
                    orderBy = q => q.OrderByDescending(c => c.Post.Title);
                    break;
                case "Username":
                    orderBy = q => q.OrderBy(c => c.User.UserName);
                    break;
                case "username_desc":
                    orderBy = q => q.OrderByDescending(c => c.User.UserName);
                    break;
                case "PostTitle":
                    orderBy = q => q.OrderBy(c => c.Post.Title);
                    break;
                case "postTitle_desc":
                    orderBy = q => q.OrderByDescending(c => c.Post.Title);
                    break;
                case "CreatedDate":
                    orderBy = q => q.OrderBy(c => c.CreatedDate);
                    break;
                case "createdDate_desc":
                    orderBy = q => q.OrderByDescending(c => c.CreatedDate);
                    break;
                case "ModifiedDate":
                    orderBy = q => q.OrderBy(c => c.ModifiedDate);
                    break;
                case "modifiedDate_desc":
                    orderBy = q => q.OrderByDescending(c => c.ModifiedDate);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Post.Title);
                    break;
            }

            string includeProperties = "User,Post";

            var comments = await _commentServices.GetAsync(filter: filter, orderBy: orderBy, includeProperties: includeProperties, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentServices.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Approved = !comment.Approved;
            try
            {
                var result = await _commentServices.UpdateAsync(comment);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _commentServices.DeleteAsync(id);
                if (!result)
                {
                    return BadRequest();
                }
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll([FromBody] object[] selectedItems)
        {
            if (selectedItems == null)
            {
                return NotFound();
            }

            try
            {
                var x = selectedItems[0].ToString();
                var result = await _commentServices.DeleteRangeAsync(selectedItems);
                if (!result)
                {
                    return BadRequest();
                }
                else
                {
                    return Json(result);
                }
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
