using CoffeeAndWifi.WebApi.Models;
using CoffeeAndWifi.WebApi.Models.Domains;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        //for testing authorization in Swagger
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            BearerFormat = JwtBearerDefaults.AuthenticationScheme,
            Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        c.OperationFilter<SecurityRequirementsOperationFilter>();

        //to automatically create documentation in Swagger
        var xmlFile = "CoffeeAndWifi.WebApi.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //c.IncludeXmlComments(xmlPath);
    });

builder.Services.AddDbContext<CoffeeWifiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeWifiContext")));

builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

//authentication for JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)), //gets secret-key from AppSettings
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
