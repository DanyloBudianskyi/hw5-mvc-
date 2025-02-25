using hw5_mvc_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace hw5_mvc_.Controllers
{
    public class HomeController(ILogger<HomeController> logger, SiteContext context) : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public async Task<IActionResult> Index([FromForm] SearchForm searchForm)
        {
            ViewData["professions"] = await context.Professions.ToListAsync();
            ViewData["userInfos"] = await context.UserInfos
                .Include(x => x.Profession)
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile)
                .ToListAsync();
				return View();

            
        }

		public async Task<IActionResult> Search([FromQuery] SearchForm searchForm)
		{
            IQueryable<UserInfo> query = context.UserInfos
                .Include(x => x.Profession)
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile);

            if (!String.IsNullOrEmpty(searchForm.Text)) {
                query = query.Where(x => x.Name.ToLower().Contains(searchForm.Text.ToLower()) ||
                    x.UserSkills.Any(x => x.Skill.Title.ToLower() == searchForm.Text.ToLower())
                ); 
            }

            return PartialView("_SearchResult", query.ToList());
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
