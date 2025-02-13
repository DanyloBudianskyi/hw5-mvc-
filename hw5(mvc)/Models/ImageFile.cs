namespace hw5_mvc_.Models
{
    public class ImageFile
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string Src => "/uploads/" + FileName;
    }
}
