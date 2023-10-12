using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Services
{
    public interface IAdminService
    {
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

        //Reservations
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

        //
    }
}
