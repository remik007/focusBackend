using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;

namespace FocusAPI.Services
{
    public interface IContactService
    {
        public ContactDto GetContact();
    }
    public class ContactService : IContactService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;

        public ContactService(FocusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ContactDto GetContact()
        {
            var contact = _context.SubPages
                .FirstOrDefault();

            if (contact == null)
                throw new NotFoundException("No contact details found");

            var contactDto = _mapper.Map<ContactDto>(contact);
            return contactDto;
        }
    }
}
