using BoardGameNight;
using BoardGameNight.Configurations;
using BoardGameNight.Data;
using BoardGameNight.Models;
using BoardGameNight.Repositories.Implementations;
using BoardGameNight.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBordspelRepository, BordspelRepository>();
builder.Services.AddScoped<BlobStorageService>();
builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("BlobStorageSettings"));

//

// Add the global exception handler
builder.Services.AddScoped<GlobalExceptionHandler>();

// Apply the global exception handler to all actions
builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandler))));

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add default identity
builder.Services.AddDefaultIdentity<Persoon>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// map razor pages
app.MapRazorPages();
app.Run();