using Shop.Application.DTOs.UserDTOs;
using Shop.Application.Interfaces.Helpers;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Enums;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services;

public class AdminService(IAuthRepository _authRepository, IPasswordResetTokenService _passwordResetTokenService, IHashHelper _hashHelper, IPasswordResetTokenRepository _passwordResetTokenRepository, IEmailService _emailService) : IAdminService
{
    public async Task<bool> CreateStaffAsync(CreateStaffDTO dto)
    {
   
        if(await _authRepository.IsExistEmailAsync(dto.Email))
        {
            return false;
        }

        if (dto.Role != UserRole.Admin && dto.Role != UserRole.Moderator)
        {
            return false;
        }

        var user = new User
        {
            Email = dto.Email,
            Role = dto.Role,
            PasswordHash = string.Empty
        };

        await _authRepository.AddUserAsync(user);
        var token = _passwordResetTokenService.GeneratePasswordResetToken();
        var tokenHash = _passwordResetTokenService.HashToken(token);

        var resetToken = new PasswordResetToken
        {
            UserId = user.Id,
            TokenHash = tokenHash,
            ExpiresAt = DateTime.Now.AddMinutes(30),
            IsUsed = false
        };
        await _passwordResetTokenRepository.AddAsync(resetToken);
        var resetLink =
        $"https://localhost:7026/api/v1/Admin/reset-password?token={Uri.EscapeDataString(token)}"; 

        await _emailService.SendPasswordResetEmailAsync(user.Email,resetLink);
        return true;

    }
    public async Task<bool> ResetPasswordAsync(ResetPasswordDTO dto)
    {
        var tokenHash = _passwordResetTokenService.HashToken(dto.Token); 
        var resetToken = await _passwordResetTokenRepository.GetByTokenHashAsync(tokenHash);

        if (resetToken == null)
        {
            return false;
        }

        if (resetToken.IsUsed)
        {
            return false;
        }

        if (resetToken.ExpiresAt <= DateTime.UtcNow)
        {
            return false;
        }
        var user = resetToken.User;

        user.PasswordHash = _hashHelper.Hash(dto.NewPassword);

        resetToken.IsUsed = true;

        await _authRepository.UpdateUserAsync(user);

        await _passwordResetTokenRepository.UpdateAsync(resetToken);

        return true;
    }
}
