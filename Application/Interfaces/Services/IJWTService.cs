using Shop.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface IJWTService
{
    public string GenerateAccessToken(UserLoginDTO userLoginDto, string role);
}