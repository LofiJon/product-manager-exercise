using System.Reflection;
using Application.Contracts.Product;
using Application.Dtos;
using Application.Services;
using Application.Usecases.Product;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Database.Context;
using Infrastructure.Database.Repositories;
using Infrastructure.Mappings;
using Infrastructure.Pageable;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
namespace Infrastructure.DependencyInjection;

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
        services.AddScoped<IPageableProduct, PageableProductUsecase>();
        services.AddScoped<IAddProduct, AddProductUsecase>();
        services.AddScoped<IDeleteProduct, DeleteProductUsecase>();

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        

        // Register IHttpContextAccessor manually
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Register IUriService
        services.AddSingleton<IUriService>(o =>
        {
            var accessor = o.GetRequiredService<IHttpContextAccessor>();
            var request = accessor?.HttpContext?.Request;
            var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
            return new UriAdapter(uri);
        });

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