using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLogicLayer.TagServices;
using MyBlog.Models;
using MyBlog.Presentation.Areas.Blog.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.Presentation.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class TagManagerController : Controller
    {
        private readonly ITagServices _tagServices;

        public TagManagerController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SlugSortParm"] = sortOrder == "Slug" ? "slug_desc" : "Slug";
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

            Expression<Func<Tag, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString) || c.Slug.Contains(searchString);
            }

            Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = q => q.OrderByDescending(c => c.Name);
                    break;
                case "Slug":
                    orderBy = q => q.OrderBy(c => c.Slug);
                    break;
                case "slug_desc":
                    orderBy = q => q.OrderByDescending(c => c.Slug);
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
                    orderBy = q => q.OrderBy(c => c.Name);
                    break;
            }

            var tags = await _tagServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(tags);
        }

        public IActionResult Create()
        {
            var tagViewModel = new TagViewModel();
            return View(tagViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = tagViewModel.Name,
                    Slug = tagViewModel.Slug,
                    Content = tagViewModel.Content,
                };
                try
                {
                    var result = await _tagServices.AddAsync(tag);
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
            return View(tagViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagServices.GetByIdAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            var tagViewModel = new TagViewModel()
            {
                Id = tag.Id,
                Slug = tag.Slug,
                Name = tag.Name,
                Content = tag.Content,
            };

            return View(tagViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag()
                {
                    Id = tagViewModel.Id,
                    Name = tagViewModel.Name,
                    Slug = tagViewModel.Slug,
                    Content = tagViewModel.Content,
                };
                try
                {
                    var result = await _tagServices.UpdateAsync(tag);
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
            return View(tagViewModel);
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
                var result = await _tagServices.DeleteAsync(id);
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
                var result = await _tagServices.DeleteRangeAsync(selectedItems);
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
