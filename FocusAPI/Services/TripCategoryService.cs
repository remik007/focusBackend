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
    }
    public class TripCategoryService : ITripCategoryService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;

        public TripCategoryService(FocusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .Where(t => t.TripCategory.Name == category && t.IsEnabled == true && t.To > DateTime.Now && t.To > DateTime.Now)
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<TripDto>>(trips);
            var tripCategoryDetailsDto = new TripCategoryDetailsDto() { Name = category, Trips = tripDtos };

            return tripCategoryDetailsDto;
        }
    }
}
