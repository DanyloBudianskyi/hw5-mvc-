using hw5_mvc_.Models;
using hw5_mvc_.Models.Forms;
using hw5_mvc_.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace hw5_mvc_.Controllers
{
    [Authorize]
    public class SkillController(ILogger<SkillController> logger, SiteContext context, FileService fileService) : Controller
    {
        public IActionResult Index()
        {
            return View(context.Skills
                .Include(x => x.Icon)
                .ToList());
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

            await context.Skills.AddAsync(model);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var model = context.Skills.First(x => x.Id == id);
            if (model.Icon != null)
            {
                ViewData["icon"] = model.Icon;
            }
            ViewData["id"] = id;
            return View(new SkillForm(context.Skills.First(x => x.Id == id)));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] SkillForm form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["id"] = id;
                return View(form);
            }

            var model = context.Skills.First(x => x.Id == id);
            if (form.Icon != null)
            {
                if(model.Icon != null)
                {
                    fileService.Delete(model.Icon);
                    context.ImageFiles.Remove(model.Icon);
                }
                model.Icon = await fileService.SaveAsync("skillIcons", form.Icon);
            }
            form.Update(model);

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var skillToDelete = context.Skills.First(x => x.Id == id);
            if (skillToDelete.Icon != null) context.ImageFiles.Remove(skillToDelete.Icon);
            context.Skills.Remove(skillToDelete);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
