﻿using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Services
{
    public interface IAdminService
    {
        int CreateReservation(ReservationDto reservationDto);
        int CreateTrip(TripDto tripDto);
        void DeleteReservation(int id);
        void DeleteTrip(int id);
        IEnumerable<ReservationDto> GetAllReservations();
        IEnumerable<TripDto> GetAllTrips();
        ReservationDto GetReservationById(int id);
        TripDto GetTripById(int id);
        int UpdateReservation(int id, ReservationDto reservationDto);
        int UpdateTrip(int id, TripDto reservationDto);
    }
    public class AdminService : IAdminService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly AppConfig _appConfig;

        public AdminService(FocusDbContext context, IMapper mapper, IUserContextService userContextService, AppConfig appConfig)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _appConfig = appConfig;
        }

        //Reservations-------------------------------------------------------------------------------------------
        public ReservationDto GetReservationById(int id)
        {
            var reservation = _context.Reservations
                .Include(x => x.Participants)
                .FirstOrDefault(t => t.Id == id);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        public IEnumerable<ReservationDto> GetAllReservations()
        {

            var reservations = _context.Reservations
                            .Include(x => x.Participants)
                            .Include(x => x.Owner)
                            .OrderByDescending(x => x.From)
                            .ToList();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationDtos;
        }

        public int CreateReservation(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var participants = _mapper.Map<List<Participant>>(reservationDto.Participants);
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            participants.ForEach(x =>
            {
                _context.Participants.Add(x);
                _context.SaveChanges();
            });
            return reservation.Id;
        }

        public int UpdateReservation(int id, ReservationDto reservationDto)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            var updatedReservation = _mapper.Map<Reservation>(reservationDto);
            _context.Reservations.Update(updatedReservation);
            _context.SaveChanges();

            updatedReservation.Participants.ForEach(x =>
            {
                _context.Participants.Update(x);
                _context.SaveChanges();
            });

            return reservation.Id;
        }

        public void DeleteReservation(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        //Trips---------------------------------------------------------------------------------------------------
        public TripDto GetTripById(int id)
        {
            var trip = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .FirstOrDefault(t => t.Id == id);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            var tripDto = _mapper.Map<TripDto>(trip);
            return tripDto;
        }

        public IEnumerable<TripDto> GetAllTrips()
        {
            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<TripDto>>(trips);
            return tripDtos;
        }

        public int CreateTrip(TripDto tripDto)
        {
            var trip = _mapper.Map<Trip>(tripDto);
            _context.Trips.Add(trip);
            _context.SaveChanges();
            return trip.Id;
        }

        public int UpdateTrip(int id, TripDto reservationDto)
        {
            var trip = _context.Trips.FirstOrDefault(x => x.Id == id);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            var updatedTrip = _mapper.Map<Trip>(reservationDto);
            _context.Trips.Update(updatedTrip);
            _context.SaveChanges();

            return trip.Id;
        }

        public void DeleteTrip(int id)
        {
            var trip = _context.Trips.FirstOrDefault(x => x.Id == id);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            trip.IsDeleted = true;
            _context.Trips.Update(trip);
            _context.SaveChanges();

        }
    }
}
