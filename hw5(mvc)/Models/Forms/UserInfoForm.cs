using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using hw5_mvc_.Models;

namespace hw5_mvc_.Models.Forms
{
    public class UserInfoForm
    {
        public UserInfoForm() { }
        public UserInfoForm(UserInfo model) 
        {
            Name = model.Name;
            Email = model.Email;
            Description = model.Description;
            Salary = model.Salary;
            Experience = model.Experience;
            Birthday = model.Birthday;
            if(model.Profession != null) ProfessionId = Professions.FindIndex(p => p == model.Profession);
        }
        public void Update(UserInfo model)
        {
            model.Name = Name;
            model.Email = Email;
            model.Description = Description;
            model.Salary = Salary;
            model.Experience = Experience;
            model.Birthday = Birthday;
            model.Profession = Professions[ProfessionId];
        }

        [DisplayName("Full name")]
        [Required(ErrorMessage = "Full name is required")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(30)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public int Experience { get; set; }
        [DisplayName("Date of birthday")]
        [Required(ErrorMessage = "Date of birthday is required")]
        public DateTime Birthday { get; set; }
        public int ProfessionId { get; set; }
        public List<string> Professions => [
            "Designer",
            "Tester",
            "Frontend developer",
            "Backend developer",
            "Fullstack developer"
        ];
        [DisplayName("Profile image")]
        public IFormFile? Image { get; set; }

        public ICollection<IFormFile>? Gallery { get; set; }
        
    }
}
