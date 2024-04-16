using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.AuthAPI.Models;
using ProjectHive.Services.AuthAPI.Services;

namespace ProjectHive.Services.AuthAPI.Controllers;

public class TokenController(ITokenService tokenService, IUserService userService, IMapper mapper) : Controller
{
    public async Task<ActionResult> GenerateToken(LoginModel request, CancellationToken cancellationToken)
    {
        var isUserCorrect = await userService.CheckPasswordCorrect(request.Email, request.Password);
        if (isUserCorrect)
        {

            var userDto = await userService.GetByEmail(request.Email, cancellationToken);
            var jwtToken = await tokenService.GenerateJwtToken(userDto);
            var refreshToken = await tokenService.AddRefreshToken(userDto.Email,
                HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            return Ok(new TokenResponseModel { JwtToken = jwtToken, RefreshToken = refreshToken });
        }

        return Unauthorized();
    }
}

