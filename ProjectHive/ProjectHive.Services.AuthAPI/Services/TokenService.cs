using Microsoft.IdentityModel.Tokens;
using ProjectHive.Services.AuthAPI.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectHive.Services.AuthAPI.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public async Task<string> GenerateJwtToken(UserDto userDto)
        {
            var isLifetime = int.TryParse(configuration["Jwt:Lifetime"], out var lifetime);
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]);
            var iss = configuration["Jwt:Issuer"];
            var aud = configuration["Jwt:Audience"];

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("aud",aud),
                new Claim("iss",iss)
                }),
                Expires = DateTime.UtcNow.AddMinutes(lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
