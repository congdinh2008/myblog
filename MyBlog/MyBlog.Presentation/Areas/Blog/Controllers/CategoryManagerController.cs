using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLogicLayer.CategoryServices;
using MyBlog.Models;

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

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 2)
        {
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
                filter = c => c.CategoryName.Contains(searchString) || c.Slug.Contains(searchString);
            }

            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = q => q.OrderByDescending(c => c.CategoryName);
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
                    orderBy = q => q.OrderBy(c => c.CategoryName);
                    break;
            }

            var categories = await _categoryServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(categories);
        }
    }
}
