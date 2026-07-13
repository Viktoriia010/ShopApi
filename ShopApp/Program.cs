
using Microsoft.EntityFrameworkCore;
using Shop.Api.Interfaces;
using Shop.Api.Middleware;
using Shop.Api.Services;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using Shop.Application.Mapping;
using Shop.Application.Services;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Repositories;
using ShopDomain.Models;

namespace Shop.Api;
//public static class MiddlewareExtensions
//{
//    public static IApplicationBuilder UseRequestTimer(this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<RequestTimerMiddleware>();
//    }
//}
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ShopDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
        });
        // ================= AutoMapper =================
        builder.Services.AddAutoMapper(
            _ => { },typeof(CategoryProfile).Assembly
            );

        // ================= CORS =================
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //------------------SERVICES-------------
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IImageService,  ImageService>();
        //------------------REPOSITORIES-------------
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("AllowAll");
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.MapOpenApi();
        //}

        //app.UseHttpsRedirection();


        app.UseMiddleware<RequestTimerMiddleware>();
        //app.UseMiddleware<UserCheckMiddleware>();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllers();
        //app.UseRequestTimer();

        app.Run();
    }
}
