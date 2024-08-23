using ITIAttendanceSystem.Models;
using ITIAttendanceSystem.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ITIAttendanceSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Connection");
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AttendanceDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(connectionString);
            });
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddSession(a =>{});
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(a =>
                {
                    a.LoginPath = "/account/login";
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
