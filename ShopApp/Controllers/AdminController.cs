using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.UserDTOs;
using Shop.Application.Interfaces.Services;
using Shop.Application.Services;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AdminController(IAdminService _adminService) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost("create-staff")]

    public async Task<IActionResult> CreateStaff(CreateStaffDTO dto)
    {
        var result = await _adminService.CreateStaffAsync(dto);

        if (!result)
            return BadRequest();

        return Ok();
    }


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
    {
        var result = await _adminService.ResetPasswordAsync(dto);

        if (!result)
            return BadRequest("Invalid or expired token.");

        return Ok();
    }
}
