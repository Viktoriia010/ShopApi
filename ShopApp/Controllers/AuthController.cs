using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.UserDTOs;
using Shop.Application.Interfaces.Services;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAuthService _authService):ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO dto)
    {
        var user = await _authService.RegisterAsync(dto);
        if (user.User == null || user.Token == null)
            return BadRequest("Користувач за таким email вже існує");

        //Response.Cookies.Append("accessToken", user.Token, new CookieOptions
        //{
        //    HttpOnly = true,
        //    Secure = true,
        //    SameSite = SameSiteMode.Strict,
        //    Expires = DateTimeOffset.UtcNow.AddMinutes(30)
        //});
        return Ok(new { user = user.User, token = user.Token });
    }
    [HttpPost("authentication")]
    public async Task<IActionResult> UserAuthentication([FromBody] UserLoginDTO dto)
    {
        var token = await _authService.UserAuthenticationAsync(dto);
        if (token == null)
        {
            return Unauthorized("Невірний email чи пароль");
        }
        return Ok(new { token });
    }
        //[HttpPost("refresh")]
        //public async Task<IActionResult> GetRefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];
        //    if (string.IsNullOrEmpty(refreshToken))
        //        return Unauthorized();
        //    Response.Cookies.Append("refreshToken", user.Token,
        //    new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //        SameSite = SameSiteMode.Strict,
        //        //Expires = DateTimeOffset.UtcNow.AddMinutes(30)
        //    });
        //    return Ok(new { user = user.User, token = user.Token });
        //}
    }
