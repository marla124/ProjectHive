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
            var dto = _mapper.Map<TDto>(await _unitOfWork.ProjectRepository.GetById(Id, cancellationToken));
            return dto;
        }

        public async Task DeleteById(Guid Id, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProjectRepository.DeleteById(Id, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task<TDto?> Create(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = await _unitOfWork.Repository.CreateOne(entity, cancellationToken);
            await _unitOfWork.Commit();

            return _mapper.Map<TDto>(createdEntity);
        }

        public async Task<TDto?> Update(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var updatedEntity = await _unitOfWork.Repository.Update(entity, cancellationToken);
            await _unitOfWork.Commit();

            return _mapper.Map<TDto>(updatedEntity);
        }

        public async Task CreateMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            await _unitOfWork.Repository.CreateMany(entities, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task DeleteMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            await _unitOfWork.Repository.DeleteMany(entities, cancellationToken);
            await _unitOfWork.Commit();
        }
    }
}

