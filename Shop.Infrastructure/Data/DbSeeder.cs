using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces.Helpers;
using ShopDomain.Enums;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data;

public class DbSeeder
{
    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
        var hashHelper = scope.ServiceProvider.GetRequiredService<IHashHelper>();

        await context.Database.MigrateAsync();

        var adminExists = await context.Users.AnyAsync(u => u.Role == UserRole.Admin);

        if (!adminExists)
        {
            var admin = new User
            {
                Email = "admin@shop.com",
                PasswordHash = hashHelper.Hash("Admin_123!"),
                Role = UserRole.Admin,
                IsActive = true
            };

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}
