using hw5_mvc_.Models;
using hw5_mvc_.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace hw5_mvc_.Controllers
{
    public class UserInfoController(ILogger<UserInfoController> logger, UserInfoService service) : Controller
    {
        public IActionResult Index()
        {
            return View(service.GetAll());
        }
        public IActionResult View(int id)
        {
            return View(service.FindById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserInfoForm(new UserInfo());
            return View(model);
        }
        [HttpPost]
        public IActionResult Create([FromForm] UserInfoForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var model = new UserInfo();
            form.Update(model);

			var random = new Random();
			do
			{
				var id = random.Next(1, 1000);
				if (service.FindById(id) != null)
				{
					continue;
				}
                model.Id = id;
			} while (model.Id == 0);

            service.Add(model);
            service.SaveChanges();
            return RedirectToAction("Index");
		}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["id"] = id;
            return View(new UserInfoForm(service.FindById(id)));
        }
        [HttpPost]
        public IActionResult Edit(int id, [FromForm] UserInfoForm form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["id"] = id;
                return View(form);
            }
            var model = service.FindById(id);
            form.Update(model);

            service.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var userToDelete = service.FindById(id);
            service.Delete(userToDelete);
            service.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
