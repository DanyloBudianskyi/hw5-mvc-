using hw5_mvc_.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hw5_mvc_
{
    public class UserInfo
    {
        public UserInfo() {  
            ImageFiles = new List<ImageFile>();
            UserSkills = new List<UserSkill>(); 
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        public decimal Salary { get; set; }
        public int Experience { get; set; }
        public DateTime Birthday { get; set; }
        public string? Profession { get; set; }
        public virtual ImageFile? MainImageFile { get; set; }
        public virtual ICollection<ImageFile> ImageFiles { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
	}
}
