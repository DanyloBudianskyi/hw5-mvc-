using System.ComponentModel.DataAnnotations.Schema;

namespace hw5_mvc_.Models
{
    [Table("Profession")]

    public class Profession
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
