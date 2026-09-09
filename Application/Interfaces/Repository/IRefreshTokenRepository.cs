using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshToken token, CancellationToken cancellationToken);
    Task<RefreshToken?> GetValidRefreshTokenAsync(string token , CancellationToken cancellationToken);
    Task DeleteTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
