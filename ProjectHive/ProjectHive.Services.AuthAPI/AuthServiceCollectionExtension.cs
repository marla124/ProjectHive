using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectHive.Services.AuthAPI.Data;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.AuthAPI.Services;
using ProjectHive.Services.Core.Data.Repository;
using System.Text;

namespace ProjectHive.Services.AuthAPI;

public static class AuthServiceCollectionExtension
{
    public static void RegisterServicesForAuthApi
    (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        //services.AddDbContext<ProjectHiveAuthDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddDbContext<ProjectHiveAuthDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryProjectTest");
        });

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRepository<UserRole, ProjectHiveAuthDbContext>, Repository<UserRole, ProjectHiveAuthDbContext>>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    public static void ConfigureJwt
    (this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var secret = configuration["Jwt:Secret"];
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });
    }
}
