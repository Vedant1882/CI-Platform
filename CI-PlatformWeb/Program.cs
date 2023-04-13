using CI_Entity.Models;
using CI_Platform.Repository.Interface;
using CI_Platform.Repository.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CIDbContext>((y => y.UseSqlServer("Server=VEDANT;Database=CI-Platform;Trusted_Connection=True;TrustServerCertificate=True;", optionsBuilder => optionsBuilder.CommandTimeout(10000)).EnableSensitiveDataLogging()));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout= TimeSpan.FromDays(1);
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Employee}/{controller=Home}/{action=landingpage}/{id?}");

app.Run();
