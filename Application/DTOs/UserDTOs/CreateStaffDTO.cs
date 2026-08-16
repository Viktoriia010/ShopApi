using ShopDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.UserDTOs;

public class CreateStaffDTO
{
    public string Email { get; set; } = string.Empty;
    //public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; } 
    
}
