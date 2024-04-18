using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.AuthAPI.Models;
using ProjectHive.Services.AuthAPI.Services;

namespace ProjectHive.Services.AuthAPI.Controllers;

public class TokenController(ITokenService tokenService, IUserService userService) : Controller
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> GenerateToken(LoginModel request, CancellationToken cancellationToken)
    {
        var isUserCorrect = await userService.CheckPasswordCorrect(request.Email, request.Password);
        if (isUserCorrect)
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var userDto = await userService.GetByEmail(request.Email, cancellationToken);
            var jwtToken = await tokenService.GenerateJwtToken(userDto, cancellationToken);
            var refreshToken = await tokenService.AddRefreshToken(userDto.Email, userAgent, userDto.Id, cancellationToken);
            return Ok(new TokenResponseModel { JwtToken = jwtToken, RefreshToken = refreshToken });
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("Refresh")]

    public async Task<IActionResult> GenerateToken(RefreshTokenModel request, CancellationToken cancellationToken)
    {
        var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        var isRefreshTokenValid = await tokenService.CheckRefreshToken(request.RefreshToken, cancellationToken);
        if (isRefreshTokenValid)
        {
            var userDto = await userService.GetUserByRefreshToken(request.RefreshToken, cancellationToken);
            var jwtToken = await tokenService.GenerateJwtToken(userDto, cancellationToken);
            var refreshToken = await tokenService.AddRefreshToken(userDto.Email, userAgent, userDto.Id, cancellationToken);
            await tokenService.RemoveRefreshToken(request.RefreshToken, cancellationToken);
            return Ok(new TokenResponseModel { JwtToken = jwtToken, RefreshToken = refreshToken });
        }

        return Unauthorized();
    }

    [HttpDelete]
    [Route("Revoke")]
    public async Task<IActionResult> RevokeToken(RefreshTokenModel request, CancellationToken cancellationToken)
    {
        var IsRefreshTokenValid = await tokenService.CheckRefreshToken(request.RefreshToken, cancellationToken);
        if (IsRefreshTokenValid)
        {
            await tokenService.RemoveRefreshToken(request.RefreshToken, cancellationToken);
            return Ok();
        }
        return Unauthorized();
    }
}

