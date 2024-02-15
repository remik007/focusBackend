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
            CreateMap<SubPageDto, SubPage>();
            CreateMap<ContactDto, Contact>();
            CreateMap<TripDto, Trip>()
                .ForMember(m => m.TransportType, c => c.Ignore())
                .ForMember(m => m.TripCategory, c => c.Ignore());
            CreateMap<Trip, TripDto>()
                .ForMember(m => m.CurrentAvailableSeats, c => c.MapFrom(s => s.AvailableSeats - s.Reservations.Where(x => x.IsConfirmed == true).Sum(x => x.Participants.Count)));
            CreateMap<Trip, GetTripDto>()
                .ForMember(m => m.CurrentAvailableSeats, c => c.MapFrom(s => s.AvailableSeats - s.Reservations.Where(x => x.IsConfirmed == true).Sum(x => x.Participants.Count)));
            CreateMap<TripCategoryDto, TripCategory>();
            CreateMap<TripCategory, TripCategoryDto>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(m => m.TripName, c => c.MapFrom(x => x.Trip.Name))
                .ForMember(m => m.From, c => c.MapFrom(x => x.Trip.From))
                .ForMember(m => m.To, c => c.MapFrom(x => x.Trip.To));
            CreateMap<Trip, GetImageDto>();
            CreateMap<SubPage, GetImageDto>();
            CreateMap<SubPage, GetSubPageDto>();
        }
    }
}
