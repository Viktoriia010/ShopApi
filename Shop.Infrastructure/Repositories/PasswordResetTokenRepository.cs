using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repository;
using Shop.Infrastructure.Data;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories;

public class PasswordResetTokenRepository(ShopDbContext _context) : IPasswordResetTokenRepository
{
    public async Task AddAsync(PasswordResetToken token)
    {
        await _context.PasswordResetTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task<PasswordResetToken?> GetByTokenHashAsync(string tokenHash)
    {
        return await _context.PasswordResetTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash);
    }
    public async Task UpdateAsync(PasswordResetToken token)
    {
        _context.PasswordResetTokens.Update(token);
        await _context.SaveChangesAsync();
    }
}
