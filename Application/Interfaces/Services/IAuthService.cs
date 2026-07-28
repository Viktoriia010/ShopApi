using Shop.Application.DTOs.UserDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface IAuthService
{
    Task<(UserReadDTO? User, string? Token)> RegisterAsync(UserCreateDTO dto);
    Task<string?> UserAuthenticationAsync(UserLoginDTO dto);
}