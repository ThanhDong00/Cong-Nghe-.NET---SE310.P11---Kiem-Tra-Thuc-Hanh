using Microsoft.EntityFrameworkCore;
using _21520724_KiemTraThucHanh.Models;
using _21520724_KiemTraThucHanh.Repository;

namespace _21520724_KiemTraThucHanh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("QlbanVaLiContext");
            builder.Services.AddDbContext<QlbanVaLiContext>(x => x.UseSqlServer(connectionString));

            builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();
            builder.Services.AddSession();

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
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Access}/{action=Login}/{id?}");

            app.Run();
        }
    }
}