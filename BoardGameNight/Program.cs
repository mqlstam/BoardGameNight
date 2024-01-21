// BoardGameNight/Program.cs

using System.Reflection;
using BoardGameNight;
using BoardGameNight.Configurations;
using BoardGameNight.Data;
using BoardGameNight.GraphQL.Schemas;
using BoardGameNight.Models;
using BoardGameNight.Repositories;
using BoardGameNight.Repositories.Implementations;
using BoardGameNight.Repositories.Interfaces;
using BoardGameNight.Services;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Enable synchronous IO
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MinimumAge", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));
});
builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();

// Repositories
builder.Services.AddScoped<IBordspelRepository, BordspelRepository>();
builder.Services.AddScoped<IBordspelGenreRepository, BordspelGenreRepository>();
builder.Services.AddScoped<ISoortBordspelRepository, SoortBordspelRepository>();
builder.Services.AddScoped<IBordspellenavondRepository, BordspellenavondRepository>();

// Services
builder.Services.AddScoped<BlobStorageService>();
builder.Services.AddScoped<LocalFileStorageService>();
builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("BlobStorageSettings"));

// Global exception handler
builder.Services.AddScoped<GlobalExceptionHandler>();
builder.Services.AddControllers(options =>
    options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandler))));

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddDefaultIdentity<Persoon>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Board Game Night API", Version = "v1" });

    // Define the GraphQL endpoint in Swagger
    c.AddServer(new OpenApiServer() { Url = "/graphql" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // Configure Swagger to include the GraphQL endpoint
    c.MapType<GraphQLHttpRequest>(() => new OpenApiSchema { Type = "object" });
});

// GraphQL
builder.Services.AddScoped<BordspelType>();
builder.Services.AddScoped<BordspellenavondType>();
builder.Services.AddScoped<ISchema, BordspelSchema>(services => new BordspelSchema(new SelfActivatingServiceProvider(services)));
builder.Services.AddSingleton<IGraphQLTextSerializer, GraphQL.NewtonsoftJson.GraphQLSerializer>();
builder.Services.AddGraphQL(options =>
{
    // Configuration for GraphQL
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Board Game Night API V1");
    // Add the GraphQL endpoint to the Swagger UI
    c.SwaggerEndpoint("/graphql", "GraphQL API");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.UseGraphQLPlayground("/ui/playground");

app.Run();