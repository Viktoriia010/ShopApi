using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IPasswordResetTokenRepository
{
    Task AddAsync(PasswordResetToken token);
    Task<PasswordResetToken?> GetByTokenHashAsync(string tokenHash);
    Task UpdateAsync(PasswordResetToken token);
}