using AprendaDotNet.VideoOnDemand.Models;
using AprendaDotNet.VideoOnDemand.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace AprendaDotNet.VideoOnDemand.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        //271

        public HomeController(SignInManager<ApplicationUser> signInMgr)
        {
            _signInManager = signInMgr;
        }


        public IActionResult Index()
        {
            //var rep = new MockReadRepository();
            //var courses = rep.GetCourses("4ad684f8-bb70-4968-85f8-458aa7dc19a3");
            //var course = rep.GetCourse("4ad684f8-bb70-4968-85f8-458aa7dc19a3", 1);
            //var video = rep.GetVideo("4ad684f8-bb70-4968-85f8-458aa7dc19a3", 1);
            //var videos = rep.GetVideos("4ad684f8-bb70-4968-85f8-458aa7dc19a3");
            //var videosForModule = rep.GetVideos("4ad684f8-bb70-4968-85f8-458aa7dc19a3", 1);


            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            return RedirectToAction("Dashboard", "Membership");
            
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
