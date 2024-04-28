using CatalogService.Domain.ContextKeeper;
using Microsoft.EntityFrameworkCore;
using CatalogService.BLL.Builders;
using CatalogService.DAL.Builders;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(
        (options) => options.UseSqlServer("Server=EPLTVILW0053\\SQLEXPRESS;Database=CatalogServiceDb;User Id=ManLog;Password=ManoBataiBuvoDu;TrustServerCertificate=True;", x=>x.MigrationsAssembly("CatalogService")));
builder.Services.AddBllServices();
builder.Services.AddDalServices();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
