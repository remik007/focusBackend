using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Services
{
    public interface ITransportTypeService
    {
        public IEnumerable<TransportTypeDto> GetAll();
    }
    public class TransportTypeService : ITransportTypeService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;

        public TransportTypeService(FocusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<TransportTypeDto> GetAll()
        {
            var transportTypes = _context.TransportTypes.ToList();
            var transportTypeDtos = _mapper.Map<List<TransportTypeDto>>(transportTypes);
            return transportTypeDtos;
        }

    }
}
