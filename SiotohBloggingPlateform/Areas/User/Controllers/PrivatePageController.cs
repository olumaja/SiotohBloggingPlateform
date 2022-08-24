using Microsoft.AspNetCore.Mvc;

namespace SiotohBloggingPlateform.Areas.User.Controllers
{
    [Area("User")]
    public class PrivatePageController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult CreatePost()
        {
            return View();
        }
    }
}
