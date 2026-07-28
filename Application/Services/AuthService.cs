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

public class AuthService(IMapper _mapper, IAuthRepository _repository, IHashHelper _hashHelper, IJWTService _jwtService) : IAuthService
{
    public async Task<string?> UserAuthenticationAsync(UserLoginDTO dto)
    {
        var user = await _repository.IsExistUserAsync(dto.Email);
        if (user == null)
        {
            return null;
        }
        var res = _hashHelper.IsValidPassword(dto.Password, user.PasswordHash);
        if (!res)
        {
            return null;
        }
        return _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());

    }

    public async Task<(UserReadDTO? User, string? Token)> RegisterAsync(UserCreateDTO dto)
    {
        var isExist = await _repository.IsExistEmailAsync(dto.Email);
        if (!isExist)
        {
            var hash = _hashHelper.Hash(dto.Password);
            var user = _mapper.Map<User>(dto);
            var token = _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());
           // var refreshToken = _jwtService.GenerateRefreshToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());
            var registerUser = await _repository.RegisterUserAsync(user, hash);
            if (registerUser != null)
                return (_mapper.Map<UserReadDTO>(registerUser), token);
        }
        return (null, null);
    }

}