using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FocusAPI.Services
{
    public interface ITripService
    {
        public TripDto GetById(int id);
        public IEnumerable<TripDto> GetAll();
    }
    public class TripService : ITripService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;

        public TripService(FocusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TripDto GetById(int id)
        {
            var trip = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .FirstOrDefault(t => t.Id == id && t.IsEnabled == true);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            var tripDto = _mapper.Map<TripDto>(trip);
            return tripDto;
        }

        public IEnumerable<TripDto> GetAll()
        {
            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .Where(t => t.IsEnabled == true && t.To > DateTime.Now)
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<TripDto>>(trips);
            return tripDtos;
        }
    }
}
