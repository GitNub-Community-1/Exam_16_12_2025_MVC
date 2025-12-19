using Domain.Models;
using Infastructure.AutoMapper;
using Infastructure.Data;
using Infastructure.Services;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DB
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connection));


builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddIdentity<User, IdentityRole<long>>(config =>
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/account/accessdenied";
    });
// Automapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ❗ СНАЧАЛА Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Auto-migrate
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.GetInfrastructure()
    .GetService<IMigrator>()!
    .MigrateAsync();

app.Run();