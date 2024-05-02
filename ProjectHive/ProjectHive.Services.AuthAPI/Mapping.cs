using AutoMapper;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.AuthAPI.Model;
using ProjectHive.Services.AuthAPI.Models;
using ProjectHive.Services.AuthAPI.Models.RequestModel;
namespace ProjectHive.Services.AuthAPI;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<RegisterModel, UserDto>().ReverseMap();
        CreateMap<LoginModel, UserDto>().ReverseMap();
        CreateMap<UserDto, UserViewModel>().ReverseMap();
        CreateMap<UserDto, RegisterUserRequestViewModel>().ReverseMap();
        CreateMap<UserDto, UpdateUserRequestViewModel>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
    }
}
