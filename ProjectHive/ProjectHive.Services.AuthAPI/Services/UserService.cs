using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.Core.Business;
using System.Security.Cryptography;
using System.Text;

namespace ProjectHive.Services.AuthAPI.Services;

public class UserService : Service<UserDto, User, ProjectHiveAuthDbContext>, IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;
    public UserService(IUserRepository userRepository, IMapper mapper,
        IUnitOfWork unitOfWork, IConfiguration configuration) : base(userRepository, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.userRepository = userRepository;
        this.configuration = configuration;
    }

    public async Task<int> RegisterUser(UserDto dto, CancellationToken cancellationToken)
    {
        var userRole = await unitOfWork.UserRoleRepository
        .FindBy(role => role.Role
        .Equals("User")).FirstOrDefaultAsync();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordHash = MdHashGenerate(dto.Password),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UserRoleId = userRole.Id,
        };
        await unitOfWork.UserRepository.CreateOne(user, cancellationToken);

        return await unitOfWork.Commit();
    }
    private string MdHashGenerate(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            var salt = configuration["AppSettings:PasswordSalt"];
            byte[] inputBytes = Encoding.UTF8.GetBytes($"{input}{salt}");
            byte[] HashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(HashBytes);
        }
    }
    public bool IsUserExists(string email)
    {
        return unitOfWork.UserRepository
            .FindBy(user => user.Email.Equals(email)).Any();
    }

    public async Task<bool> CheckPasswordCorrect(string email, string password)
    {
        var currentPasswordHash = (await unitOfWork.UserRepository
            .FindBy(user => user.Email.Equals(email)).FirstOrDefaultAsync())?.PasswordHash;
        var passwordHash = MdHashGenerate(password);
        return currentPasswordHash?.Equals(passwordHash) ?? false;
    }

    public async Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return mapper.Map<UserDto>(userRepository.GetByEmail(email, cancellationToken));
    }
}
