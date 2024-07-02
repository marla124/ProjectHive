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
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper,
        IUnitOfWork unitOfWork, IConfiguration configuration) : base(userRepository, mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<UserDto> RegisterUser(UserDto dto, CancellationToken cancellationToken)
    {
        var userRole = await _unitOfWork.UserRoleRepository
        .FindBy(role => role.Role
        .Equals("User")).FirstOrDefaultAsync(cancellationToken);
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordHash = MdHashGenerate(dto.Password),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UserRoleId = userRole.Id,
        };
        await _unitOfWork.UserRepository.CreateOne(user, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<UserDto>(user);
    }
    private string MdHashGenerate(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            var salt = _configuration["AppSettings:PasswordSalt"];
            byte[] inputBytes = Encoding.UTF8.GetBytes($"{input}{salt}");
            byte[] HashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(HashBytes);
        }
    }
    public bool IsUserExists(string email)
    {
        return _unitOfWork.UserRepository
            .FindBy(user => user.Email.Equals(email)).Any();
    }

    public async Task<bool> CheckPasswordCorrect(string email, string password, CancellationToken cancellationToken)
    {
        var currentPasswordHash = (await _unitOfWork.UserRepository
            .FindBy(user => user.Email.Equals(email)).FirstOrDefaultAsync(cancellationToken))?.PasswordHash;
        var passwordHash = MdHashGenerate(password);
        return currentPasswordHash?.Equals(passwordHash) ?? false;
    }

    public async Task<UserDto> GetUserByRefreshToken(Guid refreshToken, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshToken(refreshToken, cancellationToken);
        return _mapper.Map<UserDto>(user);
    }

}
