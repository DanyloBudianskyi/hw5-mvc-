using Microsoft.EntityFrameworkCore;

namespace hw5_mvc_.Models
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<UserSkill> UserSkills { get; set; }
        public virtual DbSet<ImageFile> ImageFiles { get; set; }
    }
}
