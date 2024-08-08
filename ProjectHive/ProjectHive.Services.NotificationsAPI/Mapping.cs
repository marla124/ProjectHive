using AutoMapper;
using ProjectHive.Services.NotificationsAPI.Dto;
using ProjectHive.Services.NotificationsAPI.Models;

namespace ProjectHive.Services.NotificationsAPI;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<NotificationDto, NotificationModel>().ReverseMap();
    }
}
