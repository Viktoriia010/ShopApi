using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface IPasswordResetTokenService
{
    string GeneratePasswordResetToken();
    string HashToken(string token);
}
