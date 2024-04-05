namespace ProjectHive.Services.Core.Business
{
    public interface IService<TDto>
    {
        public Task DeleteMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken);
        public Task CreateMany(IEnumerable<TDto> dtos, CancellationToken cancellationToken);
        public Task Create(TDto dto, CancellationToken cancellationToken);
        public Task DeleteById(Guid id, CancellationToken cancellationToken);
        public Task<TDto?> GetById(Guid Id, CancellationToken cancellationToken);
        public Task Update(TDto dto, CancellationToken cancellationToken);
    }
}
