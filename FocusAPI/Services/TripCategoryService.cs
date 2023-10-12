using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;

namespace FocusAPI.Services
{
    public interface ITripCategoryService
    {
        public TripCategoryDto GetById(int id);
        public IEnumerable<TripCategoryDto> GetAll();
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
    }
}
