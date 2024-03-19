using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class UnitOfWork
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectTask> _projectTaskRepository;
        private readonly ProjectHiveProjectDbContext _dbContext;
        public UnitOfWork(ProjectHiveProjectDbContext dbContext, IRepository<Project> projectRepository, IRepository<ProjectTask> projectTaskRepository)
        {
            _dbContext = dbContext;
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
        }
        public IRepository<Project> ProjectRepository => _projectRepository;
        public IRepository<ProjectTask> ProjectTaskRepository => _projectTaskRepository;
        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
