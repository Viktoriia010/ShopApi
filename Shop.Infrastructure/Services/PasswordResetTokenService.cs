using Shop.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services;

public class PasswordResetTokenService : IPasswordResetTokenService
{
    public string GeneratePasswordResetToken()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
    }

    public string HashToken(string token)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToHexString(hash);
    }
}

//var tokenHash = _hashHelper.Hash(token);
//var isValid = _hashHelper.IsValidPassword(token, tokenHash);