using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;
using System.Security.Claims;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class UserService : Service<UserDto, User, ProjectHiveProjectDbContext>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> CreateUser(ProjectDto dto, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FindBy(u => u.Id == dto.CreatorUserId).FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                user = new User { Id = dto.CreatorUserId };
                await _unitOfWork.UserRepository.CreateOne(user, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);
                return _mapper.Map<UserDto>(user);
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
