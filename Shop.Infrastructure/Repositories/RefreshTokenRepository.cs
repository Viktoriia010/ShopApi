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

public class RefreshTokenRepository(ShopDbContext _context) : IRefreshTokenRepository
{
    public async Task AddRefreshToken(RefreshToken token, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(token, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<RefreshToken?> GetValidRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token == token &&
                !x.IsRevoked &&
                x.ExpiresAt > DateTime.UtcNow, cancellationToken);
    }

    public async Task DeleteTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);

        if (token == null)
            return;

        token.IsRevoked = true;

        await _context.SaveChangesAsync(cancellationToken);
    }

}
