using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IAuthRepository
{
    Task<User?> RegisterUserAsync(User user, string hash);
    Task<bool> IsExistEmailAsync(string email);
    Task<User?> GetUserByEmailAsync(string email);

    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);

}