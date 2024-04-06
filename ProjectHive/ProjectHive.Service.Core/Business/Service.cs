using AutoMapper;
using ProjectHive.Service.Core.Data;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.Core.Business
{
    public class Service<TDto, TEntity, TDbContext> : IService<TDto> where TEntity : BaseEntity
    {

        private readonly IRepository<TEntity, TDbContext> _repository;
        private readonly IMapper _mapper;

        public Service(IRepository<TEntity, TDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TDto?> GetById(Guid Id, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<TDto>(await _repository.GetById(Id, cancellationToken));
            return project;
        }

        public async Task DeleteById(Guid Id, CancellationToken cancellationToken)
        {
            await _repository.DeleteById(Id, cancellationToken);
            await _repository.Commit();
        }

        public async Task Create(TDto dto, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<TEntity>(dto);
            await _repository.CreateOne(project, cancellationToken);
            await _repository.Commit();
        }

        public async Task Update(TDto dto, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<TEntity>(dto);
            await _repository.Update(project, cancellationToken);
            await _repository.Commit();
        }

        public async Task CreateMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
        {
            var projects = new List<TEntity>();
            foreach (var item in dtos)
            {
                projects.Add(_mapper.Map<TEntity>(item));
            }
            await _repository.CreateMany(projects, cancellationToken);
            await _repository.Commit();
        }

        public async Task DeleteMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
        {
            var projects = new List<TEntity>();
            foreach (var item in dtos)
            {
                projects.Add(_mapper.Map<TEntity>(item));
            }
            await _repository.DeleteMany(projects, cancellationToken);
            await _repository.Commit();
        }
    }
}

