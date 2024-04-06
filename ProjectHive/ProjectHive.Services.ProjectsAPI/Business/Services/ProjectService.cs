using AutoMapper;
using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class ProjectService : Service<ProjectDto, Project, ProjectHiveProjectDbContext>, IProjectService
    {
        public ProjectService(IProjectRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}