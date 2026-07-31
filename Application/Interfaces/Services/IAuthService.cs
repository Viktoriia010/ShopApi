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
    Task<(UserReadDTO? User, string? Token, (string? RefreshToken, int ExpireDays) RefreshToken)> RegisterAsync(UserCreateDTO dto);
    Task<(string? AccessToken, (string? RefreshToken, int ExpireDays) RefreshToken)> UserAuthenticationAsync(UserLoginDTO dto);
    Task<string> RefreshTokenAsync(string refreshToken);
}