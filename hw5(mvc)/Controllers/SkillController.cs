using hw5_mvc_.Models;
using hw5_mvc_.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace hw5_mvc_.Controllers
{
    public class SkillController(ILogger<SkillController> logger, SkillService service) : Controller
    {
        public IActionResult Index()
        {
            return View(service.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new SkillForm(new Skill());
            return View(model);
        }
        [HttpPost]
        public IActionResult Create([FromForm] SkillForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var model = new Skill();
            form.Update(model);

            var random = new Random();
            do
            {
                var id = random.Next(1, 30);
                if (service.FindById(id) != null)
                {
                    continue;
                }
                model.Id = id;
            } while (model.Id == 0);

            service.Add(model);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewData["id"] = id;
            return View(new SkillForm(service.FindById(id)));
        }
        [HttpPost]
        public IActionResult Edit(int id, [FromForm] SkillForm form)
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
            return RedirectToAction("Index");
        }
    }
}
