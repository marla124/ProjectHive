using AutoMapper;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.AuthAPI.Model;
using ProjectHive.Services.AuthAPI.Models.RequestModel;
namespace ProjectHive.Services.ProjectsAPI;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserDto, UserViewModel>().ReverseMap();
        CreateMap<UserDto, CreateUserRequestViewModel>().ReverseMap();
        CreateMap<UserDto, UpdateUserRequestViewModel>().ReverseMap();
    }
}
