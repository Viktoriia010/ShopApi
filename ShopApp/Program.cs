
using Microsoft.EntityFrameworkCore;
using Shop.Api.Interfaces;
using Shop.Api.Middleware;
using Shop.Api.Services;
using Shop.Infrastructure.Data;

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
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //builder.Services.AddSingleton<IProductService, ProductService>();
        //builder.Services.AddSingleton<ICategoryService, CategoryService>();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.MapOpenApi();
        //}

        //app.UseHttpsRedirection();


        app.UseMiddleware<RequestTimerMiddleware>();
        app.UseMiddleware<UserCheckMiddleware>();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllers();
        //app.UseRequestTimer();

        app.Run();
    }
}
