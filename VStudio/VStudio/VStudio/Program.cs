using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VStudio.Models;

namespace VStudio;

public class Program
{
    public static IConfiguration _config { get; set; }

    public Program(IConfiguration config)
    {
        _config = config;
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<NorthwindDBContext>(options =>
        {
            options.UseSqlServer("Server=DESKTOP-MLES57C;Database=NorthwindDB;Trusted_Connection=true;TrustServerCertificate=true");
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Supplier}/{action=Index}/{id?}");

        app.Run();
    }
}