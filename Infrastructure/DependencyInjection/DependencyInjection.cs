using Application.Contracts;
using Application.Services;
using AutoMapper;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Contracts.Product;
using Application.Dtos;
using Application.Usecases.Product;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Database.Context;
using Infrastructure.Database.Repositories;
using Infrastructure.Mappings;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Database Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 23))));

        // Register Repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        // Register Usecases
        services.AddScoped<IAddStock, AddStockUsecase>();
        services.AddScoped<IConsumeStock, ConsumeStockUsecase>();
        services.AddScoped<IResetDailyConsumption, ResetDailyConsumptionUsecase>();

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // Register Mapping Service
        services.AddScoped<IMappingService<ProductDto, Product>, MappingAdapter<ProductDto, Product>>();

        return services;
    }
}