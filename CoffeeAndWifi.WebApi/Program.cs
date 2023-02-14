using CoffeeAndWifi.WebApi.Models;
using CoffeeAndWifi.WebApi.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        var xmlFile = "CoffeeAndWifi.WebApi.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //c.IncludeXmlComments(xmlPath);
    });

builder.Services.AddDbContext<CoffeeWifiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeWifiContext")));

builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
