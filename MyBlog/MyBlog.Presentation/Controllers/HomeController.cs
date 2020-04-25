using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BusinessLogicLayer.CategoryServices;
using MyBlog.Presentation.Models;

namespace MyBlog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryServices _categoryServices;

        public HomeController(ILogger<HomeController> logger, ICategoryServices categoryServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
        }

        public IActionResult Index()
        {
            var categories = _categoryServices.GetAll().Result;
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
