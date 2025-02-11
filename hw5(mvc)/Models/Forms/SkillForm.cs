using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using hw5_mvc_.Models;

namespace hw5_mvc_.Models.Forms
{
    public class SkillForm
    {
        public SkillForm() { }
        public SkillForm(Skill model)
        {
            Title = model.Title;
            if(model.Color != null) { Color = Colors.GetValueOrDefault(model.Color); }
        }
        public void Update(Skill model)
        {
            model.Title = Title;
            model.Color = Color;
        }

        [DisplayName("Skill title")]
        [Required(ErrorMessage = "Skill title is required")]
        [MinLength(2)]
        [MaxLength(20)]
        public string Title { get; set; }
        public string Color { get; set; }
        public Dictionary<string, string> Colors => new Dictionary<string, string>()
        {
            {"Red", "#8B0000" },
            {"Orange", "#FF4500" },
            {"Yellow", "#FFD700" },
            {"Blue", "#0000CD" },
            {"Light blue", "#1E90FF" },
            {"Purple", "#4B0082" },
            {"Black", "#000000" },
            {"Gray", "#808080" }
            
        };
    }
}
