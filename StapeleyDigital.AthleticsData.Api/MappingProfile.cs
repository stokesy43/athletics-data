using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StapeleyDigital.AthelticsData.Domain;
using StapeleyDigital.AthleticsData.Dto;

namespace StapeleyDigital.AthleticsData.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Athlete, AthleteDto>();
            CreateMap<DeviceForCreationDto, Device>()
                .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => src.DeviceId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DeviceName));
            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.UniqueId))
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Athletes, opt => opt.MapFrom(src => src.DeviceAthletes));
            CreateMap<DeviceAthlete, AthleteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AthleteId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Athlete.Name))
                .ForMember(dest => dest.PowerOf10Id, opt => opt.MapFrom(src => src.Athlete.PowerOf10Id));
            CreateMap<Performance, PerformanceDto>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.StandardName, opt => opt.MapFrom(src => src.Standard.Name))
                .ForMember(dest => dest.AthleteName, opt => opt.MapFrom(src => src.Athlete.Name))
                .ForMember(dest => dest.MeetingName, opt => opt.MapFrom(src => src.Meeting.Name));
            CreateMap<PerformanceForCreationDto, Performance>()                
                .ForMember(dest => dest.Athlete, opt => opt.Ignore())
                .ForMember(dest => dest.Standard, opt => opt.Ignore())
                .ForMember(dest => dest.Meeting, opt => opt.Ignore())
                .ForMember(dest => dest.Event, opt => opt.Ignore());
                
        }
    }
}
