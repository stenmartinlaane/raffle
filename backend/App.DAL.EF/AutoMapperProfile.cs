using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.ActivityEvent, App.DTO.v1_0.ActivityEvent>().ReverseMap();
        CreateMap<App.Domain.MinutesAdded, App.DTO.v1_0.MinutesAdded>().ReverseMap();
        CreateMap<App.Domain.ParticipantEvent, App.DTO.v1_0.ParticipantEvent>().ReverseMap();
        CreateMap<App.Domain.RaffleItem, App.DTO.v1_0.RaffleItem>().ReverseMap();
    }
}