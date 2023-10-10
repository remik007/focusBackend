using FocusAPI.Data;
using FocusAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FocusAPI.Services
{
    public interface ITripService
    {

    }
    public class TripService : ITripService
    {
        private readonly FocusDbContext _context;

        public TripService(FocusDbContext context)
        {
            _context = context;
        }

        public Trip GetTrip(int id)
        {
            var trip = _context.Trips
                .Include(t => t.TripType)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations)
                .FirstOrDefault(t => t.Id == id);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            return trip;
        }
    }
}
