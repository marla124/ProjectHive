using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class Service<TDto, TEntity> : IService<TDto> where TEntity : BaseEntity
    {

        private readonly IUnitOfWork<TEntity> _unitOfWork;
        private readonly IMapper _mapper;

        public Service(IUnitOfWork<TEntity> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TDto?> GetById(Guid Id, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<TDto>(await _unitOfWork.ProjectRepository.GetById(Id, cancellationToken));
            return project;
        }

        public async Task DeleteById(Guid Id, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProjectRepository.DeleteById(Id, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task Create(TDto dto, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<TEntity>(dto);
            await _unitOfWork.Repository.CreateOne(project, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task Update(TDto dto, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<TEntity>(dto);
            await _unitOfWork.Repository.Update(project, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task CreateMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
        {
            var projects = new List<TEntity>();
            foreach (var item in dtos)
            {
                projects.Add(_mapper.Map<TEntity>(item));
            }
            await _unitOfWork.Repository.CreateMany(projects, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task DeleteMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
        {
            var projects = new List<TEntity>();
            foreach (var item in dtos)
            {
                projects.Add(_mapper.Map<TEntity>(item));
            }
            await _unitOfWork.Repository.DeleteMany(projects, cancellationToken);
            await _unitOfWork.Commit();
        }
    }
}

