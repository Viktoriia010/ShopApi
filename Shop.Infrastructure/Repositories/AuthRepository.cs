using Shop.Application.Interfaces.Repository;
using Shop.Infrastructure.Data;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Shop.Infrastructure.Repositories;

public class AuthRepository(ShopDbContext _context) : IAuthRepository
{
    public async Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistEmailAsync(string email, CancellationToken cancellationToken)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        if (userFromDb == null)
            return false;
        return true;
    }
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        if (userFromDb == null)
            return null;
        return userFromDb;
    }



    public async Task<User?> RegisterUserAsync(User user, string hash, CancellationToken cancellationToken)
    {
        user.PasswordHash = hash;
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return await _context.Users.FirstOrDefaultAsync(us => (us.Email == user.Email && us.PasswordHash == user.PasswordHash), cancellationToken);
        /*
         * TODO:
         1) Перевірити чи немає вже у БД такого email
         2) Захешувати пароль
         3) Додати користувача у БД
         4) Зробити токен, скоріше за все не тут будемо робити
         */
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}