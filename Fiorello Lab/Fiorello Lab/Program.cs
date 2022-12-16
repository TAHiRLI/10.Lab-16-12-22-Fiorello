using Fiorello_Lab.DAL;
using Fiorello_Lab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FioDbContext>(opt =>
{
    opt.UseSqlServer(@"Server=DESKTOP-6BCR9RQ;Database=FiorelloV3;Trusted_Connection=TRUE");
});


builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<FioDbContext>();


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
