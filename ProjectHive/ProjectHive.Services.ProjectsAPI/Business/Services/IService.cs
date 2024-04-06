namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public interface IService<TDto>
    {
        public Task DeleteMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken);
        public Task CreateMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken);
        public Task<TDto?> Create(TDto dto, CancellationToken cancellationToken);
        public Task DeleteById(Guid id, CancellationToken cancellationToken);
        public Task<TDto?> GetById(Guid Id, CancellationToken cancellationToken);
        public Task<TDto?> Update(TDto dto, CancellationToken cancellationToken);
    }
}
