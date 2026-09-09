
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Shop.Api.Interfaces;
using Shop.Api.Middleware;
using Shop.Api.Services;
using Shop.Application.Interfaces.Helpers;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using Shop.Application.Mapping;
using Shop.Application.Queries.Product;
using Shop.Application.Services;
using Shop.Infrastructure.Configuration;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Helpers;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Services;
using ShopDomain.Models;
using StackExchange.Redis;
using System.Text;

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
        var configuration = builder.Configuration;
         // ================= JWT Settings =================
        var jwtSettings = configuration      
            .GetSection("Jwt")
            .Get<JwtSettings>()
            ?? throw new Exception("JWT settings not configured.");
        //Реєстрація налаштувань в DI, можемо їх читати будь-де
        builder.Services.Configure<JwtSettings>(
        configuration.GetSection("Jwt"));
        //=================== RabitMq ===================
        builder.Services.Configure<RabbitMqSettings>(
            builder.Configuration.GetSection("RabbitMq")
        );
        builder.Services.AddHostedService<RabbitMqReaderService>();
        //==================MEDIATR======================
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetProductByIdHandler).Assembly);
        });
        // ================= AutoMapper =================
        builder.Services.AddAutoMapper(
            _ => { },typeof(CategoryProfile).Assembly,typeof(ProductProfile).Assembly,
            typeof(UserProfile).Assembly
            );

        // ================= CORS =================
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:5173") 
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); 
            });
        });

        
        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowAll", policy =>
        //    {
        //        policy.AllowAnyOrigin()
        //              .AllowAnyMethod()
        //              .AllowAnyHeader();
        //    });
        //});

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("ProductionPolicy", policy =>
        //    {
        //        policy.WithOrigins("https://example.com", "https://www.example.com")
        //              .WithMethods("GET", "POST", "PUT", "DELETE")
        //              .WithHeaders("Content-Type", "Authorization");
        //    });
        //});
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        // ================= Swagger + JWT =================
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "Enter JWT token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            //options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            //{
            //    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            //});
        });
        //builder.Services.AddSwaggerGen();
        //builder.Services.AddSwaggerGen();

        //===================CACHE=======================
        builder.Services.AddMemoryCache();
        //===================REDIS=======================
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = builder.Configuration.GetConnectionString("RedisServerConnection");
            return ConnectionMultiplexer.Connect(config);
        });
        //------------------SERVICES-------------
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IImageService,  ImageService>();
        //builder.Services.AddScoped<ICachingService, MemoryCachingService>();
        builder.Services.AddScoped<ICachingService, RedisCachingService>();
        builder.Services.AddScoped<IAuthService,  AuthService>();
        builder.Services.AddScoped<IJWTService,  JWTService>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<IPasswordResetTokenService, PasswordResetTokenService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IQueueService, RabbitMqService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();
        builder.Services.AddScoped<IMongoDbService, MongoDbService>();
        builder.Services.AddHostedService<RabbitMqOrderReaderService>();

        //------------------HELPERS-------------
        builder.Services.AddSingleton<IHashHelper, HashHelper>();
        //------------------REPOSITORIES-------------
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        builder.Services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();
        // ================= Authentication =================
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
         {
             //Правила перевірки токена
             options.TokenValidationParameters = new TokenValidationParameters            {
             ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Key)
                ),
                ClockSkew = TimeSpan.Zero
            };
    });
        builder.Services.AddAuthorization();
        var app = builder.Build();
        //------------------SEEDING--------------
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                DbSeeder.SeedAdminAsync(services).Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database");
            }

        }


        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("AllowAll");
        app.UseCors("AllowFrontend");
        app.UseMiddleware<CancellationTokenHandleMiddleware>();

        //app.UseCors("ProductionPolicy");
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.MapOpenApi();
        //}

        //app.UseHttpsRedirection();


        app.UseMiddleware<RequestTimerMiddleware>();
        //app.UseMiddleware<UserCheckMiddleware>();
        app.UseStaticFiles();
        //app.UseAuthorization();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        //app.UseRequestTimer();

        app.Run();
    }
}
