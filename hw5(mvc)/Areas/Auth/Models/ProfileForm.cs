using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hw5_mvc_.Areas.Auth.Models
{
    public class ProfileForm
    {
        public IFormFile? Image { get; set; }
        [DisplayName("Full name")]
        public string? FullName { get; set; }
        public string? Phone { get; set; }
    }
}
