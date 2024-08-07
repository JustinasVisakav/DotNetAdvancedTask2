using CatalogService.DAL.ContextKeeper;
using Microsoft.EntityFrameworkCore;
using CatalogService.BLL.Builders;
using CatalogService.DAL.Builders;
using Microsoft.OpenApi.Models;
using System.Reflection;
using CatalogService.BLL.Wrappers;
using CatalogService.Configuration.Interfaces;
using CatalogService.Configuration;
using CatalogSercice.RabbitMq.Interfaces;
using CatalogService.RabbitMq.Models;
using Microsoft.AspNetCore.Authorization;
using CatalogService.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
})
    .AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API v1", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddDbContext<DatabaseContext>(
        (options) => options.UseSqlServer("Server=EPLTVILW0053\\SQLEXPRESS;Database=CatalogServiceDb;User Id=ManLog;Password=ManoBataiBuvoDu;TrustServerCertificate=True;", x => x.MigrationsAssembly("CatalogService")));
builder.Services.AddBllServices();
builder.Services.AddDalServices();
builder.Services.AddScoped<IRabbitMq, RabbitMqWrapper>();
builder.Services.AddSingleton<FailedMessageStorage>();
builder.Services.AddSingleton<IConfig, Config>();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();
app.UseMiddleware<AuthMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(
options =>
{
    var descriptions = app.DescribeApiVersions();
    foreach (var description in descriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
