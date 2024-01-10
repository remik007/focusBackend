using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;

namespace FocusAPI.Services
{
    public interface ISubPageService
    {
        public SubPageDto GetByName(string subPageName);
        public IEnumerable<SubPageDto> GetAll();
    }
    public class SubPageService : ISubPageService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;

        public SubPageService(FocusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SubPageDto GetByName(string subPageName)
        {
            var subPage = _context.SubPages
                .FirstOrDefault(t => t.Name == subPageName);

            if (subPage == null)
                throw new NotFoundException("Sub Page not found");

            var subPageDto = _mapper.Map<SubPageDto>(subPage);
            return subPageDto;
        }

        public IEnumerable<SubPageDto> GetAll()
        {
            var subPages = _context.SubPages.ToList();
            var subPageDtos = _mapper.Map<List<SubPageDto>>(subPages);
            return subPageDtos;
        }
    }
}
