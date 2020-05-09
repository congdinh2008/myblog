using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLogicLayer.CategoryServices;
using MyBlog.BusinessLogicLayer.PostServices;
using MyBlog.BusinessLogicLayer.TagServices;
using MyBlog.Models;
using MyBlog.Presentation.Areas.Blog.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyBlog.Presentation.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Authorize(Roles = "Administrator, Manager")]
    public class PostManagerController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ITagServices _tagServices;
        private readonly IPostServices _postServices;

        public PostManagerController(ICategoryServices categoryServices, ITagServices tagServices, IPostServices postServices)
        {
            _categoryServices = categoryServices;
            _tagServices = tagServices;
            _postServices = postServices;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";
            ViewData["PublishedSortParm"] = sortOrder == "Published" ? "published_desc" : "Published";
            ViewData["PublishedDateSortParm"] = sortOrder == "PublishedDate" ? "publishedDate_desc" : "PublishedDate";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Post, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Title.Contains(searchString) || c.Slug.Contains(searchString) || c.Author.UserName.Contains(searchString);
            }

            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;

            switch (sortOrder)
            {
                case "title_desc":
                    orderBy = q => q.OrderByDescending(c => c.Title);
                    break;
                case "Author":
                    orderBy = q => q.OrderBy(c => c.Author.UserName);
                    break;
                case "author_desc":
                    orderBy = q => q.OrderByDescending(c => c.Author.UserName);
                    break;
                case "Published":
                    orderBy = q => q.OrderBy(c => c.Published);
                    break;
                case "published_desc":
                    orderBy = q => q.OrderByDescending(c => c.Published);
                    break;
                case "PublishedDate":
                    orderBy = q => q.OrderBy(c => c.PublishedDate);
                    break;
                case "publishedDate_desc":
                    orderBy = q => q.OrderByDescending(c => c.PublishedDate);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Title);
                    break;
            }

            var posts = await _postServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(posts);
        }

        public IActionResult Create()
        {
            var postViewModel = new PostViewModel()
            {
                //Author = User,
            };

            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = categoryViewModel.Name,
                    Slug = categoryViewModel.Slug,
                    Content = categoryViewModel.Content,
                };
                try
                {
                    var result = await _categoryServices.AddAsync(category);
                    if (result > 0)
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
            }
            return View(categoryViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Slug = category.Slug,
                Name = category.Name,
                Content = category.Content,
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Id = categoryViewModel.Id,
                    Name = categoryViewModel.Name,
                    Slug = categoryViewModel.Slug,
                    Content = categoryViewModel.Content,
                };
                try
                {
                    var result = await _categoryServices.UpdateAsync(category);
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
            }
            return View(categoryViewModel);
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
                var result = await _categoryServices.DeleteAsync(id);
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
                var result = await _categoryServices.DeleteRangeAsync(selectedItems);
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
