using hw5_mvc_.Models;

namespace hw5_mvc_
{
    public class Skill
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public int Level { get; set; }
        public string Color { get; set; }
        public ImageFile? Icon { get; set; }
    }
}
