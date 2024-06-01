using Microsoft.EntityFrameworkCore;
using prog3_practica_08.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Prg3EfPr1Context>(options => options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PRG3_EF_PR1;Integrated Security=true; TrustServerCertificate=True"));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
