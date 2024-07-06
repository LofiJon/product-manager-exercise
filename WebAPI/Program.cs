using Infrastructure.Database;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Application.Contracts;
using Application.Usecases;
using Application.Services;
using Infrastructure.Database.Repositories;
using MediatR;
using AutoMapper;
using System.Reflection;
using Infrastructure.Database.Context;
using Infrastructure.DependencyInjection;
using Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Logger
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product Manager",
        Description = "API for managing products",
        Contact = new OpenApiContact
        {
            Name = "Jonathan Malagueta da Costa",
            Url = new Uri("https://www.linkedin.com/in/jonathan-malagueta-524391150/")
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    await ProductsSeeders.Execute(db);
}

// Configure middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Manager API V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.Run();
