using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;

namespace Shop.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult AddUser(User user)
    {
        return Ok(user);
    }


}
