using Microsoft.AspNetCore.Mvc;
using SiotohBloggingPlateform.Core.Interfaces;
using SiotohBloggingPlateform.Model.ViewModels;
using System.Diagnostics;

namespace SiotohBloggingPlateform.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepo;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepo)
        {
            _logger = logger;
            _postRepo = postRepo;
        }

        public IActionResult Index()
        {
            var model = _postRepo.GetAllPosts();
            return View(model);
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
