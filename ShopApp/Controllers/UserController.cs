using Microsoft.AspNetCore.Mvc;
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
