using AutoMapper;
using Shop.Application.DTOs.UserDTOs;
using Shop.Application.Interfaces.Helpers;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services;

public class AuthService(IMapper _mapper, IAuthRepository _repository, IRefreshTokenRepository _refreshTokenRepository, IHashHelper _hashHelper, IJWTService _jwtService) : IAuthService
{
    public async Task<(string? AccessToken, (string? RefreshToken, int ExpireDays) RefreshToken)> UserAuthenticationAsync(UserLoginDTO dto)
    {
        var user = await _repository.IsExistUserAsync(dto.Email);
        if (user == null)
        {
            return (null, (null, 0));
        }
        var res = _hashHelper.IsValidPassword(dto.Password, user.PasswordHash);
        if (!res)
        {
            return (null, (null, 0));
        }
        var refreshToken = _jwtService.GenerateRefreshToken();
        var refresh = new RefreshToken
        {
            Token = refreshToken.Token,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshToken.ExpireDays)
        };
        await _refreshTokenRepository.AddRefreshToken(refresh);
        return (_jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString()), refreshToken);

    }

    public async Task<(UserReadDTO? User, string? Token,(string? RefreshToken, int ExpireDays) RefreshToken)> RegisterAsync(UserCreateDTO dto)
    {
        var isExist = await _repository.IsExistEmailAsync(dto.Email);
        if (!isExist)
        {
            var hash = _hashHelper.Hash(dto.Password);
            var user = _mapper.Map<User>(dto);
            var token = _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());
            var refreshToken = _jwtService.GenerateRefreshToken();
   
            var registerUser = await _repository.RegisterUserAsync(user, hash);

            if (registerUser != null)
            {
                var refresh = new RefreshToken
                {
                    Token = refreshToken.Token,
                    UserId = registerUser.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(refreshToken.ExpireDays)
                };
                await _refreshTokenRepository.AddRefreshToken(refresh);
                return (_mapper.Map<UserReadDTO>(registerUser), token, refreshToken);
            }
        }
        return (null, null, (null, 0));
    }
    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetValidRefreshTokenAsync(refreshToken);

        if (storedToken == null)
        {
            return null;
        }

        var accessToken = _jwtService.GenerateAccessToken(
            _mapper.Map<UserLoginDTO>(storedToken.User),
            storedToken.User.Role.ToString()
        );

        return accessToken;
    }

}