using Shop.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface IAdminService
{
    Task<bool> CreateStaffAsync(CreateStaffDTO dto);
    Task<bool> ResetPasswordAsync(ResetPasswordDTO dto);
}
