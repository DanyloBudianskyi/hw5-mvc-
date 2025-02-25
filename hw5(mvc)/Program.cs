using hw5_mvc_.Models;
using hw5_mvc_.Models.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace hw5_mvc_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<UserInfoService>();
            //builder.Services.AddScoped<SkillService>();
            builder.Services.AddScoped<FileService>();
            //builder.Services.AddScoped<UserSkillService>();
            builder.Services.AddDbContext<SiteContext>(o =>
                o.UseSqlServer("Server=DESKTOP-8ISC2JM;Database=Site;Trusted_Connection=True;TrustServerCertificate=True;"));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.Run();
        }
    }
}
