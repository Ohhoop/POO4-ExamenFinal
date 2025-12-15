using CreditImpot.MVC.Interface;
using CreditImpot.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CreditImpot.MVC.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CreditImpotMVCContextConnection") ?? throw new InvalidOperationException("Connection string 'CreditImpotMVCContextConnection' not found.");

// Construire le chemin absolu vers la base de donnees
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "CreditImpot.MVC.db");
connectionString = $"Data Source={dbPath}";

Console.WriteLine($"=== CHEMIN DE LA BASE DE DONNEES: {dbPath} ===");
Console.WriteLine($"=== FICHIER EXISTE: {File.Exists(dbPath)} ===");

builder.Services.AddDbContext<CreditImpotMVCContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 7;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CreditImpotMVCContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
string? uriString = builder.Configuration.GetValue<string>("UrlAPI") ?? throw new Exception("Config missing: UrlAPI");

builder.Services.AddHttpClient<IWeatherForecastService, WeatherForecastServiceProxy>(client => client.BaseAddress = new Uri(uriString));
builder.Services.AddHttpClient<IFraisGardeService, FraisGardeServiceProxy>(client => client.BaseAddress = new Uri(uriString));

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
