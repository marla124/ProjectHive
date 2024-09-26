using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;
namespace ProjectHive.Services.ProjectsAPI;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<ProjectDto, Project>().ReverseMap();
        CreateMap<ProjectTaskStatusDto, StatusTasks>().ReverseMap();
        CreateMap<ProjectDto, ProjectViewModel>().ReverseMap();
        CreateMap<ProjectDto, CreateProjectRequestViewModel>().ReverseMap();
        CreateMap<ProjectDto, UpdateProjectRequestViewModel>().ReverseMap();
        CreateMap<ProjectTaskDto, ProjectTaskViewModel>().ReverseMap();
        CreateMap<ProjectTaskDto, ProjectTask>().ReverseMap();
        CreateMap<ProjectTaskDto, UpdateTaskRequestViewModel>().ReverseMap();
        CreateMap<ProjectTaskDto, CreateTaskRequestViewModel>().ReverseMap();
        CreateMap<ProjectStatusDto, ProjectStatus>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
