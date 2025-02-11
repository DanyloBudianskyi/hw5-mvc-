using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hw5_mvc_
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Description { get; set; }
        public decimal Salary { get; set; }
        public int Experience { get; set; }
        public DateTime Birthday { get; set; }
        public List<Skill>? Skills { get; set; } = [];
        public string? Profession { get; set; }
    }
}
