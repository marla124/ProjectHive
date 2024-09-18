using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.AuthAPI.Models;
using ProjectHive.Services.AuthAPI.Services;

namespace ProjectHive.Services.AuthAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TokenController(ITokenService tokenService, IUserService userService) : Controller
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> GenerateToken(LoginModel request, CancellationToken cancellationToken)
    {
        var isUserCorrect = await userService.CheckPasswordCorrect(request.Email, request.Password, cancellationToken);
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
    [Route("[action]")]

    public async Task<IActionResult> GenerateTokenByRefresh(RefreshTokenModel request, CancellationToken cancellationToken)
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
    [Route("Revoke/{refreshToken}")]
    public async Task<IActionResult> RevokeToken(Guid refreshToken, CancellationToken cancellationToken)
    {
        var isRefreshTokenValid = await tokenService.CheckRefreshToken(refreshToken, cancellationToken);
        if (isRefreshTokenValid)
        {
            await tokenService.RemoveRefreshToken(refreshToken, cancellationToken);
            return Ok();
        }
        return Unauthorized();
    }
}

