using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.AuthAPI.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UAParser;

namespace ProjectHive.Services.AuthAPI.Services
{
    public class TokenService(IConfiguration configuration, IUserService userService, IUnitOfWork unitOfWork, IMapper mapper) : ITokenService
    {
        public async Task<Guid> AddRefreshToken(string email, string userAgent, Guid userId, CancellationToken cancellationToken)
        {
            var user = await userService.GetByEmail(email, cancellationToken);
            if (user != null)
            {
                var refTokenDto = new RefreshTokenDto
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    ExpiringAt = DateTime.UtcNow.AddDays(5),
                    AssociateDeviceName = GetDeviceName(userAgent),
                    IsActive = true,
                    UserId = userId,
                };
                var refToken = mapper.Map<RefreshToken>(refTokenDto);
                await unitOfWork.AuthRepository.CreateRefreshToken(refToken, cancellationToken);
                return refToken.Id;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        private string GetDeviceName(string userAgent)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo clientInfo = uaParser.Parse(userAgent);
            string deviceName = clientInfo.Device.ToString();
            return deviceName;
        }
        public async Task<string> GenerateJwtToken(UserDto userDto, CancellationToken cancellationToken)
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
                new Claim("iss",iss),
                new Claim("userId", userDto.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> CheckRefreshToken(Guid requestRefreshToken, CancellationToken cancellationToken)
        {
            var token = await unitOfWork.AuthRepository.GetRefreshToken(requestRefreshToken, cancellationToken);
            if (token == null)
            {
                return false;
            }
            return true;
        }

        public async Task RemoveRefreshToken(Guid requestRefreshToken, CancellationToken cancellationToken)
        {
            await unitOfWork.AuthRepository.DeleteRefreshToken(requestRefreshToken, cancellationToken);
        }
    }
}
