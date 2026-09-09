using AutoMapper;
using Shop.Application.DTOs.UserDTOs;
using Shop.Application.Interfaces.Helpers;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services;

public class AuthService(IMapper _mapper, IAuthRepository _repository, IRefreshTokenRepository _refreshTokenRepository, IHashHelper _hashHelper, IJWTService _jwtService, IQueueService _queue) : IAuthService
{
    public async Task<(string? AccessToken, (string? RefreshToken, int ExpireDays) RefreshToken)> UserAuthenticationAsync(UserLoginDTO dto, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(dto.Email, cancellationToken);
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
        await _refreshTokenRepository.AddRefreshToken(refresh, cancellationToken);
        return (_jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString()), refreshToken);

    }

    public async Task<(UserReadDTO? User, string? Token,(string? RefreshToken, int ExpireDays) RefreshToken)> RegisterAsync(UserCreateDTO dto, CancellationToken cancellationToken)
    {
        var isExist = await _repository.IsExistEmailAsync(dto.Email, cancellationToken);
        if (!isExist)
        {
            var hash = _hashHelper.Hash(dto.Password);
            var user = _mapper.Map<User>(dto);
            var token = _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());
            var refreshToken = _jwtService.GenerateRefreshToken();
   
            var registerUser = await _repository.RegisterUserAsync(user, hash, cancellationToken);

            if (registerUser != null)
            {
                var refresh = new RefreshToken
                {
                    Token = refreshToken.Token,
                    UserId = registerUser.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(refreshToken.ExpireDays)
                };
                await _refreshTokenRepository.AddRefreshToken(refresh, cancellationToken);
                await _queue.PublishAsync("Users",  new { Email = registerUser.Email, Password = registerUser.PasswordHash }, cancellationToken);
                return (_mapper.Map<UserReadDTO>(registerUser), token, refreshToken);
            }
        }
        return (null, null, (null, 0));
    }
    public async Task<string> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var storedToken = await _refreshTokenRepository.GetValidRefreshTokenAsync(refreshToken, cancellationToken);

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

    public async Task LogoutAsync(string refreshToken, CancellationToken cancellationToken)
    {
        await _refreshTokenRepository.DeleteTokenAsync(refreshToken, cancellationToken);
    }

    public async Task<UserReadDTO?> GetProfileAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(email, cancellationToken);
        if(user == null) {
            return null;
        }
        return _mapper.Map<UserReadDTO>(user);
    }
}