using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace hw5_mvc_.Models
{
    public class User : IdentityUser<int>
    {
        [MaxLength(100)]
        public string? FullName { get; set; }
        public virtual ImageFile? ImageFile { get; set; }
    }
}
