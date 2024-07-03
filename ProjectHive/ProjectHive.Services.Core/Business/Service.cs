using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Data;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.Core.Business;

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
        var dto = _mapper.Map<TDto>(await _repository.GetById(Id, cancellationToken));
        return dto;
    }

    public async Task<TDto[]?> GetMany()
    {
        var dtoarr = await _repository
        .GetAsQueryable()
        .Select(dto => _mapper.Map<TDto>(dto))
        .ToArrayAsync();
        return dtoarr;
    }

    public async Task DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        await _repository.DeleteById(Id, cancellationToken);
        await _repository.Commit(cancellationToken);
    }

    public async Task<TDto?> Create(TDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(dto);
        var createdEntity = await _repository.CreateOne(entity, cancellationToken);
        await _repository.Commit(cancellationToken);
        return _mapper.Map<TDto>(createdEntity);
    }

    public async Task<TDto?> Update(TDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(dto);
        var createdEntity = await _repository.Update(entity, cancellationToken);
        await _repository.Commit(cancellationToken);
        return _mapper.Map<TDto>(createdEntity);
    }

    public async Task CreateMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
        await _repository.CreateMany(entities, cancellationToken);
        await _repository.Commit(cancellationToken);
    }

    public async Task DeleteMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
        await _repository.DeleteMany(entities, cancellationToken);
        await _repository.Commit(cancellationToken);
    }
}
