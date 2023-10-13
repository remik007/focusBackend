using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Services
{
    public interface IReservationService
    {
        ReservationDto GetById(int id);
        IEnumerable<ReservationDto> GetAll();
        int Create(ReservationDto reservation);
    }
    public class ReservationService : IReservationService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly AppSettings _appConfig;

        public ReservationService(FocusDbContext context, IMapper mapper, IUserContextService userContextService, AppSettings appConfig)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _appConfig = appConfig;
        }

        public ReservationDto GetById(int id)
        {
            var reservation = _context.Reservations
                .Include(x => x.Participants)
                .FirstOrDefault(t => t.Id == id && t.OwnerId == _userContextService.GetUserId);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        public IEnumerable<ReservationDto> GetAll()
        {

            var reservations = _context.Reservations
                            .Include(x => x.Participants)
                            .Where(x =>
                                x.OwnerId == _userContextService.GetUserId
                                && x.To.AddDays(_appConfig.ReservationRetentionPeriodDays) > DateTime.Now)
                            .OrderByDescending(x => x.From)
                            .ToList();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationDtos;
        }

        public int Create(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var participants = _mapper.Map<List<Participant>>(reservationDto.Participants);
            reservation.OwnerId = (int)_userContextService.GetUserId;
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            participants.ForEach(x =>
            {
                _context.Participants.Add(x);
                _context.SaveChanges();
            });
            return reservation.Id;
        }
    }
}
