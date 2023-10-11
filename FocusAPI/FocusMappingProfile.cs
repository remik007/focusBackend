using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Models;

namespace FocusAPI
{
    public class FocusMappingProfile : Profile
    {
        public FocusMappingProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<Contact, ContactDto>();
            CreateMap<Participant, ParticipantDto>();
            CreateMap<SubPage, SubPageDto>();
            CreateMap<TransportType, TransportTypeDto>();
            CreateMap<Trip, TripDto>();
            CreateMap<TripCategory, TripCategoryDto>();
        }
    }
}
