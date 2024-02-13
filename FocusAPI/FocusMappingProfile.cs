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
            CreateMap<TripDto, Trip>();
            CreateMap<Trip, TripDto>()
                .ForMember(m => m.CurrentAvailableSeats, c => c.MapFrom(s => s.AvailableSeats - s.Reservations.Where(x => x.IsConfirmed == true).Sum(x => x.Participants.Count)));
            CreateMap<TripCategory, TripCategoryDto>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(m => m.TripName, c => c.MapFrom(x => x.Trip.Name))
                .ForMember(m => m.From, c => c.MapFrom(x => x.Trip.From))
                .ForMember(m => m.To, c => c.MapFrom(x => x.Trip.To));
        }
    }
}
