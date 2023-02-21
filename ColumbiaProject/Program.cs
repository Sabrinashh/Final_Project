using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using ColumbiaProject.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ColumbiaDbContext>(opt =>
{
    opt.UseSqlServer(@"Server=DESKTOP-M3IRH0H\SQLEXPRESS;Database=Columbia;Trusted_Connection=TRUE");
});

//builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
//{
//    opt.Password.RequireDigit = false;
//    opt.Password.RequiredLength = 8;
//    opt.Password.RequireNonAlphanumeric = false;
//}).AddDefaultTokenProviders().AddEntityFrameworkStores<ColumbiaDbContext>();

builder.Services.AddScoped<LayoutService>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToAccessDenied = options.Events.OnRedirectToLogin = context =>
    {
        if (context.HttpContext.Request.Path.Value.StartsWith("/manage"))
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/manage/account/login" + redirectPath.Query);
        }
        else
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/account/login" + redirectPath.Query);
        }

        return Task.CompletedTask;
    };
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
   );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

