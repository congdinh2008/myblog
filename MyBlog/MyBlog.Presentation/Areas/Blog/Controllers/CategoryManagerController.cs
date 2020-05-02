using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLogicLayer.CategoryServices;
using MyBlog.Models;
using MyBlog.Presentation.Areas.Blog.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.Presentation.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class CategoryManagerController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryManagerController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
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

            Expression<Func<Category, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString) || c.Slug.Contains(searchString);
            }

            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

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

            var categories = await _categoryServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(categories);
        }

        public IActionResult Create()
        {
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
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
