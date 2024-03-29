using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectTask> _projectTaskRepository;
        private readonly IRepository<TEntity> _repository;
        private readonly ProjectHiveProjectDbContext _dbContext;
        public UnitOfWork(ProjectHiveProjectDbContext dbContext, IRepository<Project> projectRepository, IRepository<ProjectTask> projectTaskRepository, IRepository<TEntity> repository)
        {
            _dbContext = dbContext;
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
            _repository = repository;
        }
        public IRepository<Project> ProjectRepository => _projectRepository;
        public IRepository<ProjectTask> ProjectTaskRepository => _projectTaskRepository;
        public IRepository<TEntity> Repository => _repository;
        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
