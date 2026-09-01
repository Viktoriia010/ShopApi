using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.UserDTOs;
using Shop.Application.Interfaces.Services;
using System.Security.Claims;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAuthService _authService):ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO dto)
    {
        var user = await _authService.RegisterAsync(dto);
        if (user.User == null || user.Token == null|| user.RefreshToken.RefreshToken == null)
            return BadRequest("Користувач за таким email вже існує");

        Response.Cookies.Append("refreshToken", user.RefreshToken.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(user.RefreshToken.ExpireDays)
        });
        return Ok(new { user = user.User, token = user.Token, refreshToken = user.RefreshToken.RefreshToken });
    }
    [HttpPost("authentication")]
    public async Task<IActionResult> UserAuthentication([FromBody] UserLoginDTO dto)
    {
        var token = await _authService.UserAuthenticationAsync(dto);
        if (token.AccessToken == null || token.RefreshToken.RefreshToken == null)
        {
            return Unauthorized("Невірний email чи пароль");
        }
        Response.Cookies.Append("refreshToken", token.RefreshToken.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(token.RefreshToken.ExpireDays)
        });
        return Ok(new { token.AccessToken , token.RefreshToken.RefreshToken });
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> GetRefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        var result = await _authService.RefreshTokenAsync(refreshToken);


        if (result == null)
            return Unauthorized();

        return Ok(new
        {
            accessToken = result
        });
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
            return Unauthorized();

        var profile = await _authService.GetProfileAsync(email);

        if (profile == null)
            return NotFound("Користувача не знайдено");

        return Ok(profile);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
            return Ok();

        await _authService.LogoutAsync(refreshToken);

        Response.Cookies.Delete("refreshToken");

        return Ok();
    }
}
