using Microsoft.EntityFrameworkCore;
using SiotohBloggingPlateform.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using SiotohBloggingPlateform.Model.Models;

var builder = WebApplication.CreateBuilder(args);

var dbConnections = builder.Configuration.GetConnectionString("DefaultConnections");
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(dbConnections));

builder.Services.AddIdentity<User, IdentityRole>(
        options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredUniqueChars = 0;
        }
    ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

DbInitializer.Initialise(app.Services.CreateScope().ServiceProvider).Wait();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
