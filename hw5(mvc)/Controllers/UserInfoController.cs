using hw5_mvc_.Models;
using hw5_mvc_.Models.Forms;
using hw5_mvc_.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace hw5_mvc_.Controllers
{
    public class UserInfoController(
        ILogger<UserInfoController> logger,
        UserInfoService service,
        UserSkillService userSkillService,
        SkillService skillService,
        FileService fileService
        ) : Controller
    {
        public IActionResult Index()
        {
            return View(service.GetAll());
        }
        public IActionResult View(int id)
        {
            var userSkills = userSkillService.GetAll().Where(x => x.UserId == id).ToList();
            var skills = skillService.GetAll().ToList();
            ViewData["userSkills"] = userSkills;
            ViewData["skills"] = skills;
            return View(service.FindById(id));
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
                    model.ImageFiles.Add(imageFile);
                }
            }
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
            var form = new UserInfoForm(service.GetAll().First(x => x.Id == id));
            var userSkills = userSkillService.GetAll().Where(x => x.UserId == id).ToList();
            var skills = skillService.GetAll().ToList();
            var availableSkills = skills.Where(x => !userSkills.Select(x => x.SkillId).ToList().Contains(x.Id)).ToList();
            
            ViewData["userSkills"] = userSkills;
            ViewData["skills"] = skills;
            ViewData["availableSkills"] = availableSkills;

            return View(form);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] UserInfoForm form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["id"] = id;
                return View(form);
            }

            var model = service.FindById(id);
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
                    if (imageFile == null) Console.WriteLine("imagefile == null");
                    model.ImageFiles.Add(imageFile);
                }
            }

            form.Update(model);
            service.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var userToDelete = service.FindById(id);
            foreach (var item in userToDelete.ImageFiles.ToList())
            {
                fileService.Delete(item);
            }
            service.Delete(userToDelete);
            service.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ChangeMainImage(int id, [FromQuery] string src)
        {
            var model = service.FindById(id);
            model.MainImageFile = model.ImageFiles.First(x => x.Src == src);
            service.SaveChanges();
            return Json(new { Ok = true });
        }

        public IActionResult DeleteImage(int id, [FromQuery] string src)
        {
            var model = service.FindById(id);
            var file = model.ImageFiles.FirstOrDefault(x => x.Src == src);
            if (file != null)
            {
                fileService.Delete(file);
                model.ImageFiles.Remove(file);
                service.SaveChanges();
            }

            return Json(new { Ok = true });
        }
        public IActionResult AddSkill([FromBody] UserSkill data)
        {
            var skill = skillService.GetAll().First(x => x.Id == data.SkillId);
            var user = service.GetAll().First(x => x.Id == data.UserId);

            if (null != userSkillService.GetAll().FirstOrDefault(x => x.UserId == user.Id && x.SkillId == skill.Id))
            {
                Response.StatusCode = 400;
                return Json(new { Ok = false });
            }
            var newUserSkill = new UserSkill
            {
                UserId = user.Id,
                SkillId = skill.Id,
                Level = data.Level
            };
            var random = new Random();
            do
            {
                var id = random.Next(1, 1000);
                if (userSkillService.FindById(id) != null)
                {
                    continue;
                }
                newUserSkill.Id = id;
            } while (user.Id == 0);


            userSkillService.Add(newUserSkill);
            userSkillService.SaveChanges();
            return Json(new { Ok = true });
        }
        public IActionResult DeleteSkill([FromBody] UserSkill data)
        {
            var userSkill = userSkillService.GetAll().First(x => x.SkillId == data.SkillId && x.UserId == data.UserId);
            if (userSkill != null)
            {
                userSkillService.Delete(userSkill);
                userSkillService.SaveChanges();
                return Json(new { Ok = true });
            }
            return Json(new { Ok = false });
        }
        public IActionResult EditSkill([FromBody] UserSkill data)
        {
            var userSkill = userSkillService.GetAll().First(x => x.SkillId == data.SkillId && x.UserId == data.UserId);
            if (userSkill != null)
            {
                userSkill.Level = data.Level;
                userSkillService.SaveChanges();
                return Json(new { Ok = true });
            }
            return Json(new { Ok = false });
        }
    }
}