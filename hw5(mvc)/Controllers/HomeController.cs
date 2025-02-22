using hw5_mvc_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hw5_mvc_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult About()
        //{
        //    var User = new UserInfo
        //    {
        //        Name = "Danil Budyanskiy",
        //        Email = "test@test.com",
        //        Description = "Description",
        //        Skills = new List<Skill>
        //        {
        //            new Skill
        //            {
        //                Title = "Python",
        //                Level = 30,
        //            },
        //            new Skill {
        //                Title = "C#",
        //                Level = 50,
        //            },
        //            new Skill
        //            {
        //                Title = "JS",
        //                Level = 40
        //            }
        //        }
        //    };

        //    return View(User);
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
