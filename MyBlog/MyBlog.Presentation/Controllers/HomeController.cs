using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BusinessLogicLayer.PostServices;
using MyBlog.Presentation.Models;
using System.Diagnostics;

namespace MyBlog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostServices _postServices;

        public HomeController(ILogger<HomeController> logger, IPostServices postServices)
        {
            _logger = logger;
            _postServices = postServices;
        }

        public IActionResult Index()
        {
            var posts = _postServices.GetAll().Result;
            return View(posts);
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
