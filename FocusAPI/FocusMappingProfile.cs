﻿using AutoMapper;
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
            CreateMap<Trip, TripDto>()
                .ForMember(m => m.CurrentAvailableSeats, c => c.MapFrom(s => s.AvailableSeats - s.Reservations.Sum(x => x.Participants.Count)));
            CreateMap<TripCategory, TripCategoryDto>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(m => m.TripName, c => c.MapFrom(x => x.Trip.Name));
        }
    }
}
