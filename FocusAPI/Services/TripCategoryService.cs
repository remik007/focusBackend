using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Services
{
    public interface ITripCategoryService
    {
        public TripCategoryDto GetById(int id);
        public IEnumerable<TripCategoryDto> GetAll();
        public TripCategoryDetailsDto GetByName(string category);
        public TripCategoryDetailsDto Search(SearchDto searchDto);
        public IEnumerable<GetImageDto> GetCategoryImages(string category);
        public IEnumerable<GetImageDto> GetSearchImages(SearchDto searchDto);
    }
    public class TripCategoryService : ITripCategoryService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TripCategoryService(FocusDbContext context, IMapper mapper, AppSettings appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public TripCategoryDto GetById(int id)
        {
            var tripCategory = _context.TripCategories
                .FirstOrDefault(t => t.Id == id);

            if (tripCategory == null)
                throw new NotFoundException("Trip Category not found");

            var tripCategoryDto = _mapper.Map<TripCategoryDto>(tripCategory);
            return tripCategoryDto;
        }

        public IEnumerable<TripCategoryDto> GetAll()
        {
            var tripCategories = _context.TripCategories.ToList();
            var tripCategoryDtos = _mapper.Map<List<TripCategoryDto>>(tripCategories);
            return tripCategoryDtos;
        }

        public TripCategoryDetailsDto GetByName(string category)
        {
            var tripCategory = _context.TripCategories
                .Any(t => t.Name == category);

            if (!tripCategory)
                throw new NotFoundException("Trip Category not found");

            

            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .Where(t => t.TripCategory.Name == category && t.IsEnabled == true && t.IsDeleted != true && t.To.AddDays(_appSettings.DisplayTripDateRangeInDays) > DateTime.Now)
                .OrderByDescending(t => t.From)
                .ToList();
            var categoryId = _context.TripCategories.FirstOrDefault(t => t.Name == category).Id;
            var tripDtos = _mapper.Map<List<GetTripDto>>(trips);
            var tripCategoryDetailsDto = new TripCategoryDetailsDto() { Id = categoryId, Name = category, Trips = tripDtos };

            return tripCategoryDetailsDto;
        }

        public TripCategoryDetailsDto Search(SearchDto search)
        {
            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .Where(t => t.IsEnabled == true 
                    && t.IsDeleted != true 
                    && t.To.AddDays(_appSettings.DisplayTripDateRangeInDays) > DateTime.Now 
                    && (search.Country == null || t.Country == search.Country)
                    && (search.TransportType == null || t.TransportType.Name == search.TransportType)
                    && (search.DepartureCity == null || t.DepartureCity == search.DepartureCity)
                    && (search.From == null || t.From >= search.From)
                    && (search.To == null || t.To <= search.To)
                    )
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<GetTripDto>>(trips);
            var tripCategoryDetailsDto = new TripCategoryDetailsDto() { Name = "Wyniki wyszukiwania", Trips = tripDtos };

            return tripCategoryDetailsDto;
        }

        public IEnumerable<GetImageDto> GetCategoryImages(string category)
        {
            var tripCategory = _context.TripCategories
                .Any(t => t.Name == category);

            if (!tripCategory)
                throw new NotFoundException("Trip Category not found");



            var trips = _context.Trips
                .Where(t => t.TripCategory.Name == category && t.IsEnabled == true && t.IsDeleted != true && t.To.AddDays(_appSettings.DisplayTripDateRangeInDays) > DateTime.Now)
                .OrderByDescending(t => t.From)
                .ToList();
            var getImageDtos = _mapper.Map<List<GetImageDto>>(trips);

            return getImageDtos;
        }

        public IEnumerable<GetImageDto> GetSearchImages(SearchDto search)
        {
            var trips = _context.Trips
                .Where(t => t.IsEnabled == true
                    && t.IsDeleted != true
                    && t.To.AddDays(_appSettings.DisplayTripDateRangeInDays) > DateTime.Now
                    && (search.Country == null || t.Country == search.Country)
                    && (search.TransportType == null || t.TransportType.Name == search.TransportType)
                    && (search.DepartureCity == null || t.DepartureCity == search.DepartureCity)
                    && (search.From == null || t.From >= search.From)
                    && (search.To == null || t.To <= search.To)
                    )
                .OrderByDescending(t => t.From)
                .ToList();

            var getImageDtos = _mapper.Map<List<GetImageDto>>(trips);

            return getImageDtos;
        }
    }
}
