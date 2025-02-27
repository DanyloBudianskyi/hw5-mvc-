using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hw5_mvc_.Models
{
    public class SiteContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public SiteContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<UserSkill> UserSkills { get; set; }
        public virtual DbSet<ImageFile> ImageFiles { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ImageFile>()
                .HasMany(x => x.UserInfos)
                .WithMany(x => x.ImageFiles)
                .UsingEntity<Dictionary<string, string>>(
                "ImageFileUserInfo",
                x => x.HasOne<UserInfo>().WithMany().HasForeignKey("UserInfosId"),
                x => x.HasOne<ImageFile>().WithMany().HasForeignKey("ImageFilesId")
                ); modelBuilder.Entity<UserInfo>()
                .HasOne<ImageFile>(x => x.MainImageFile);
        }
    }
}
