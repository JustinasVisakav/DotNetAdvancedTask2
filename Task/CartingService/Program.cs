using CartingService.BLL.ServiceAppBuilder;
using CartingService.DAL.DalAppBuilders;
using CartingService.BLL.Services;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;
using System.Reflection;
using CatingService.BLL.Services;
using CatingService.BLL.Interfaces;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<RabbitMqService>();
builder.Services.AddSingleton<IItemUpdateService, ItemUpdateService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart API v1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Cart API v2", Version = "v2" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddDatabase();
builder.Services.AddBllServices();
builder.Services.AddScoped<CartService>();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();



// Configure the HTTP request pipeline.
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

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
