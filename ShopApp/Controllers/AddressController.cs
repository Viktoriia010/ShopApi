using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Requests.Categories;
using Shop.Application.Commands.Category;
using Shop.Application.DTOs.AddressDTOs;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.Interfaces.Services;
using Shop.Application.Services;
using ShopDomain.Models;
using System.Security.Claims;

namespace Shop.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class AddressController(IAddressService _addressService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] AddressCreateDTO dto, CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return BadRequest("Email не найдено");
        }
        var resp = await _addressService.AddAddressAsync(email, dto, cancellationToken);
        return Ok(resp);
    }
}
