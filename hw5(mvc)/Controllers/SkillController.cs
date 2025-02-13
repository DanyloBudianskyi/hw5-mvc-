using hw5_mvc_.Models;
using hw5_mvc_.Models.Forms;
using hw5_mvc_.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace hw5_mvc_.Controllers
{
    public class SkillController(ILogger<SkillController> logger, SkillService service, FileService fileService) : Controller
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
        public async Task<IActionResult> Create([FromForm] SkillForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var model = new Skill();
            form.Update(model);

            if (form.Icon != null)
            {
                var imageFile = await fileService.SaveAsync("skillIcons", form.Icon);
                model.Icon = imageFile;
            }

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
            var model = service.FindById(id);
            if (model.Icon != null)
            {
                ViewData["icon"] = model.Icon;
            }
            ViewData["id"] = id;
            return View(new SkillForm(service.FindById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] SkillForm form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["id"] = id;
                return View(form);
            }
            
            var model = service.FindById(id);
            if (form.Icon != null)
            {
                var imageFile = await fileService.SaveAsync("skillIcons", form.Icon);
                if(model.Icon != null)
                {
                    fileService.Delete(model.Icon);
                }
                model.Icon = imageFile;
            }
            form.Update(model);

            service.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var skillToDelete = service.FindById(id);
            fileService.Delete(skillToDelete.Icon);
            service.Delete(skillToDelete);
            return RedirectToAction("Index");
        }
    }
}
