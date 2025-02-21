using hw5_mvc_.Models;
using hw5_mvc_.Models.Forms;
using hw5_mvc_.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace hw5_mvc_.Controllers
{
    public class UserInfoController(
        ILogger<UserInfoController> logger,
        SiteContext context,
        FileService fileService
        ) : Controller
    {
        public IActionResult Index()
        {
            return View(context.UserInfos
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile)
                .ToList());
        }
        public async Task<IActionResult> View(int id)
        {
            var model = context.UserInfos
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile)
                .First(x => x.Id == id);
            var userSkills = model.UserSkills;
            var skills = await context.Skills
                .Include(x => x.Icon)
                .ToListAsync();

            ViewData["userSkills"] = userSkills;
            ViewData["skills"] = skills;

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserInfoForm(new UserInfo());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UserInfoForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var model = new UserInfo();
            form.Update(model);
            if (form.Image != null)
            {
                var imageFile = await fileService.SaveAsync("userInfos", form.Image);
                model.MainImageFile = imageFile;
                model.ImageFiles.Add(imageFile);
            }
            if (form.Gallery != null)
            {
                foreach (var item in form.Gallery)
                {
                    var imageFile = await fileService.SaveAsync("userInfos", item);
                    context.ImageFiles.Add(imageFile);
                    model.ImageFiles.Add(imageFile);
                }
            }

            context.UserInfos.Add(model);

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["id"] = id;
            var model = await context.UserInfos
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .ThenInclude(x => x.Icon)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile)
                .FirstAsync(x => x.Id == id);

            var form = new UserInfoForm(model);


            var userSkills = model.UserSkills;
            var skills = await context.Skills
                .Include(x => x.Icon)
                .ToListAsync();
            var availableSkills = skills.Where(x => !userSkills.Select(x => x.Skill.Id).ToList().Contains(x.Id)).ToList();

            ViewData["userSkills"] = userSkills;
            ViewData["skills"] = skills;
            ViewData["availableSkills"] = availableSkills;

            return View(form);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] UserInfoForm form)
        {
            var model = await context.UserInfos
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile)
                .FirstAsync(x => x.Id == id);

            if (!ModelState.IsValid)
            {
                ViewData["id"] = id;
                var userSkills = model.UserSkills;
                var skills = await context.Skills
                    .Include(x => x.Icon)
                    .ToListAsync();
                var availableSkills = skills.Where(x => !userSkills.Select(x => x.Skill.Id).ToList().Contains(x.Id)).ToList();

                ViewData["userSkills"] = userSkills;
                ViewData["skills"] = skills;
                ViewData["availableSkills"] = availableSkills;
                return View(form);
            }

            if (form.Image != null)
            {
                var imageFile = await fileService.SaveAsync("userInfos", form.Image);
                model.MainImageFile = imageFile;
                model.ImageFiles.Add(imageFile);
            }
            if (form.Gallery != null)
            {
                foreach (var item in form.Gallery)
                {
                    var imageFile = await fileService.SaveAsync("userInfos", item);
                    model.ImageFiles.Add(imageFile);
                }
            }

            form.Update(model);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await context.UserInfos.FirstAsync(x => x.Id == id);
            foreach (var item in userToDelete.ImageFiles.ToList())
            {
                fileService.Delete(item);
            }
            context.UserInfos.Remove(userToDelete);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeMainImage(int id, [FromQuery] int imageId)
        {
            var model = await context.UserInfos
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile)
                .FirstAsync(x => x.Id == id);

            model.MainImageFile = model.ImageFiles.First(x => x.Id == imageId);
            await context.SaveChangesAsync();
            return Json(new { Ok = true });
        }

        public async Task<IActionResult> DeleteImage(int id, [FromQuery] int imageId)
        {
            var model = await context.UserInfos
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .Include(x => x.ImageFiles)
                .Include(x => x.MainImageFile)
                .FirstAsync(x => x.Id == id);

            var file = model.ImageFiles.FirstOrDefault(x => x.Id == imageId);
            if (file != null)
            {
                fileService.Delete(file);
                context.ImageFiles.Remove(file);
                model.ImageFiles.Remove(file);
                context.SaveChangesAsync();
            }

            return Json(new { Ok = true });
        }
        [HttpPost]
        public async Task<IActionResult> AddSkill(int id, [FromBody] UserSkillForm data)
        {
            var user = await context.UserInfos
                .Include(x => x.UserSkills)
                .ThenInclude(x => x.Skill)
                .FirstAsync(x => x.Id == id);

            //TODO form
            var skill = await context.Skills.FirstAsync(x => x.Id == data.SkillId);


            if (null != user.UserSkills.FirstOrDefault(x => x.Skill.Id == skill.Id))
            {
                // Already added
                Response.StatusCode = 400;
                return Json(new { Ok = false, Error = "Alredy exists" });
            }

            user.UserSkills.Add(new UserSkill
            {
                Level = data.Level,
                Skill = skill,
                UserInfo = user
            });

            await context.SaveChangesAsync();

            return Json(new { Ok = true });
        }
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var userSkill = await context.UserSkills.FirstAsync(x => x.Id == id);
            if (userSkill != null)
            {
                context.UserSkills.Remove(userSkill);
                await context.SaveChangesAsync();
                return Json(new { Ok = true });
            }
            return Json(new { Ok = false });
        }
        public async Task<IActionResult> EditSkill([FromBody] UserSkill data)
        {
            var userSkill = await context.UserSkills.FirstAsync(x => x.Id == data.Id);
            if (userSkill != null)
            {
                userSkill.Level = data.Level;
                await context.SaveChangesAsync();
                return Json(new { Ok = true });
            }
            return Json(new { Ok = false });
        }
    }
}