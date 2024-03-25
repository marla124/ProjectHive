using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class ProjectService : Service<ProjectDto, Project>, IProjectService
    {
        public ProjectService(IUnitOfWork<Project> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    }
}
