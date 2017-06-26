using AprendaDotNet.VideoOnDemand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace AprendaDotNet.VideoOnDemand.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;


        public HomeController(SignInManager<ApplicationUser> signInMgr)
        {
            _signInManager = signInMgr;
        }


        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
