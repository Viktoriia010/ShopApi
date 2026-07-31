using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshToken token);
    Task<RefreshToken?> GetValidRefreshTokenAsync(string token);
}
