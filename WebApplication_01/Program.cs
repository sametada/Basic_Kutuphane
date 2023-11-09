using Microsoft.EntityFrameworkCore;
using WebApplication_01.Models;
using WebApplication_01.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UygulamaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>(); // _kitapTuruRepository nesnesinin olu�turulmas�n� sa�lar deoendency injection
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();
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

app.Run();
