using BoardGameNight.Data;
using BoardGameNight.Repositories.Implementations;
using BoardGameNight.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddScoped<IBordspelRepository, BordspelRepository>();
builder.Services.AddScoped<BlobStorageService>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("BlobStorageConnection");

    return new BlobStorageService(connectionString, serviceProvider.GetRequiredService<ILogger<BlobStorageService>>());
});

// Add controllers with views.
builder.Services.AddControllersWithViews();

// Configuring DbContext.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();