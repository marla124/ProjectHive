using AutoMapper;
using ProjectHive.Services.AuthAPI.Data;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.Core.Business;

namespace ProjectHive.Services.AuthAPI.Services;

public class UserService : Service<UserDto, User, ProjectHiveAuthDbContext>, IUserService
{
    public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper) { }
}
