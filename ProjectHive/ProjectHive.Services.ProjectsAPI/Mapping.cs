using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;
namespace ProjectHive.Services.ProjectsAPI
{
    public class Mapping
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapping = new MapperConfiguration(config =>
            {
                config.CreateMap<ProjectDto, Project>().ReverseMap();
                config.CreateMap<ProjectDto, ProjectViewModel>().ReverseMap();
                config.CreateMap<ProjectDto, CreateProjectRequestViewModel>().ReverseMap();
                config.CreateMap<ProjectDto, UpdateProjectRequestViewModel>().ReverseMap();
                config.CreateMap<ProjectTaskDto, ProjectTaskViewModel>().ReverseMap();
                config.CreateMap<ProjectTaskDto, ProjectTask>().ReverseMap();
            });
            return mapping;
        }
    }
}
