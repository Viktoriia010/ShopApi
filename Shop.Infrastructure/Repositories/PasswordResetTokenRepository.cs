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
    public async Task AddAsync(PasswordResetToken token, CancellationToken cancellationToken)
    {
        await _context.PasswordResetTokens.AddAsync(token, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PasswordResetToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken)
    {
        return await _context.PasswordResetTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);
    }
    public async Task UpdateAsync(PasswordResetToken token, CancellationToken cancellationToken)
    {
        _context.PasswordResetTokens.Update(token);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
