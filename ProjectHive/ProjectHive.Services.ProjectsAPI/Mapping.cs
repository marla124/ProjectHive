using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;
namespace ProjectHive.Services.ProjectsAPI
{
    public class Mapping
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapping = new MapperConfiguration(config =>
            {
                config.CreateMap<ProjectDto, Project>();
                config.CreateMap<Project, ProjectDto>();
                config.CreateMap<ProjectDto, ProjectModel>();
                config.CreateMap<ProjectModel, ProjectDto>();
                config.CreateMap<ProjectTaskDto, ProjectTaskModel>();
                config.CreateMap<ProjectTaskModel, ProjectTaskDto>();
                config.CreateMap<ProjectTaskDto, ProjectTask>();
                config.CreateMap<ProjectTask, ProjectTaskDto>();
            });
            return mapping;
        }
    }
}
