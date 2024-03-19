using Microsoft.EntityFrameworkCore;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class UnitOfWork
    {
        private readonly ProjectHiveProjectDbContext _dbContext;
        public UnitOfWork(ProjectHiveProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
