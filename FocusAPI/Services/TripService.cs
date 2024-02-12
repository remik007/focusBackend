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
        public HeaderDto GetHeader();
        public IEnumerable<TripDto> GetHighlightedTrips();
    }
    public class TripService : ITripService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TripService(FocusDbContext context, IMapper mapper, AppSettings appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public TripDto GetById(int id)
        {
            var trip = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .FirstOrDefault(t => t.Id == id && t.IsEnabled == true && t.IsDeleted != true && t.To.AddDays(_appSettings.DisplayTripDateRangeInDays) > DateTime.Now);

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
                .Where(t => t.IsEnabled == true && t.IsDeleted != true &&  t.To.AddDays(_appSettings.DisplayTripDateRangeInDays) > DateTime.Now )
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<TripDto>>(trips);
            return tripDtos;
        }

        public HeaderDto GetHeader()
        {
            var categories = _context.TripCategories.OrderBy(x => x.Name).ToList();
            var categoriesDto = _mapper.Map<List<TripCategoryDto>>(categories);
            var transportTypes = _context.TransportTypes.OrderBy(x => x.Name).ToList();
            var transportTypesDto = _mapper.Map<List<TransportTypeDto>>(transportTypes);
            var subPages = _context.SubPages.ToList();
            var subPagesDto = _mapper.Map<List<SubPageDto>>(subPages);
            var countries = _context.Trips.Select(x => x.Country).Distinct().OrderBy(x => x).ToList();
            var departureCities = _context.Trips.Select(x => x.DepartureCity).Distinct().OrderBy(x => x).ToList();
            var contactDetails = _context.Contacts.FirstOrDefault();
            var contactDetailsDto = _mapper.Map<ContactDto>(contactDetails);

            var headerDto = new HeaderDto()
            {
                Countries = countries,
                DepartureCities = departureCities,
                SubPages = subPagesDto,
                TripCategories = categoriesDto,
                TransportTypes = transportTypesDto,
                ContactDetails = contactDetailsDto
            };
            return headerDto;
        }

        public IEnumerable<TripDto> GetHighlightedTrips()
        {
            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Where(t => t.IsHighlighted == true && t.IsDeleted != true && t.IsEnabled == true)
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<TripDto>>(trips);
            return tripDtos;
        }
    }
}
