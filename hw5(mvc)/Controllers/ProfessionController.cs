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
    public class ProfessionController(ILogger<ProfessionController> logger, SiteContext context) : Controller
    {
        public IActionResult Index()
        {
            return View(context.Professions
                .ToList());
        }
        public IActionResult View(int id)
        {
            return View(context.Professions.First(x => x.Id == id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new Profession();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Profession form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var model = new Profession();
            model.Title = form.Title;

            await context.Professions.AddAsync(model);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            ViewData["id"] = id;
            return View(context.Professions.First(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] Profession form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["id"] = id;
                return View(form);
            }

            var model = context.Professions.First(x => x.Id == id);
            model.Title = form.Title;


            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var professionToDelete = context.Professions.First(x => x.Id == id);
            context.Professions.Remove(professionToDelete);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
