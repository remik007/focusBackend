using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Services
{
    public interface IAdminService
    {
        int CreateCategory(TripCategoryDto tripCategoryDto);
        int CreateReservation(ReservationDto reservationDto);
        int CreateSubPage(SubPageDto subPageDto);
        int CreateTransportType(TransportTypeDto typeDto);
        int CreateTrip(TripDto tripDto);
        void DeleteCategory(int id);
        void DeleteReservation(int id);
        void DeleteSubPage(int id);
        void DeleteTransportType(int id);
        void DeleteTrip(int id);
        IEnumerable<TripCategoryDto> GetAllCategories();
        IEnumerable<ReservationDto> GetAllReservations();
        IEnumerable<SubPageDto> GetAllSubPages();
        IEnumerable<TransportTypeDto> GetAllTransportTypes();
        IEnumerable<TripDto> GetAllTrips();
        IEnumerable<AppUserDto> GetUsers();
        TripCategoryDetailsDto GetCategoryByName(string category);
        TripCategoryDto GetCategoryById(int id);
        ContactDto GetContact();
        ReservationDto GetReservationById(int id);
        SubPageDto GetSubPageById(int id);
        TripDto GetTripById(int id);
        TransportTypeDto GetTransportTypeById(int id);
        int UpdateCategory(int id, TripCategoryDto tripCategoryDto);
        int UpdateContact(ContactDto contactDto);
        int UpdateReservation(int id, ReservationDto reservationDto);
        int UpdateSubPage(int id, SubPageDto subPageDto);
        int UpdateTransportType(int id, TransportTypeDto typeDto);
        int UpdateTrip(int id, TripDto tripDto);
    }
    public class AdminService : IAdminService
    {
        private readonly FocusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly AppSettings _appConfig;

        public AdminService(FocusDbContext context, IMapper mapper, IUserContextService userContextService, AppSettings appConfig)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _appConfig = appConfig;
        }

        //Reservations-------------------------------------------------------------------------------------------
        public ReservationDto GetReservationById(int id)
        {
            var reservation = _context.Reservations
                .Include(x => x.Participants)
                .FirstOrDefault(t => t.Id == id);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        public IEnumerable<ReservationDto> GetAllReservations()
        {

            var reservations = _context.Reservations
                            .Include(x => x.Participants)
                            .Include(x => x.Owner)
                            .OrderByDescending(x => x.From)
                            .ToList();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationDtos;
        }

        public int CreateReservation(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var participants = _mapper.Map<List<Participant>>(reservationDto.Participants);
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            participants.ForEach(x =>
            {
                _context.Participants.Add(x);
                _context.SaveChanges();
            });
            return reservation.Id;
        }

        public int UpdateReservation(int id, ReservationDto reservationDto)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            var updatedReservation = _mapper.Map<Reservation>(reservationDto);
            _context.Reservations.Update(updatedReservation);
            _context.SaveChanges();

            updatedReservation.Participants.ForEach(x =>
            {
                _context.Participants.Update(x);
                _context.SaveChanges();
            });

            return reservation.Id;
        }

        public void DeleteReservation(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        //Trips---------------------------------------------------------------------------------------------------
        public TripDto GetTripById(int id)
        {
            var trip = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .FirstOrDefault(t => t.Id == id);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            var tripDto = _mapper.Map<TripDto>(trip);
            return tripDto;
        }

        public IEnumerable<TripDto> GetAllTrips()
        {
            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<TripDto>>(trips);
            return tripDtos;
        }

        public int CreateTrip(TripDto tripDto)
        {
            var trip = _mapper.Map<Trip>(tripDto);
            _context.Trips.Add(trip);
            _context.SaveChanges();
            return trip.Id;
        }

        public int UpdateTrip(int id, TripDto tripDto)
        {
            var trip = _context.Trips.FirstOrDefault(x => x.Id == id);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            var updatedTrip = _mapper.Map<Trip>(tripDto);
            _context.Trips.Update(updatedTrip);
            _context.SaveChanges();

            return trip.Id;
        }

        public void DeleteTrip(int id)
        {
            var trip = _context.Trips.FirstOrDefault(x => x.Id == id);

            if (trip == null)
                throw new NotFoundException("Trip not found");

            trip.IsDeleted = true;
            _context.Trips.Update(trip);
            _context.SaveChanges();

        }

        //SubPages---------------------------------------------------------------------------------------------------
        public SubPageDto GetSubPageById(int id)
        {
            var subpage = _context.SubPages.FirstOrDefault(t => t.Id == id);

            if (subpage == null)
                throw new NotFoundException("SubPage not found");

            var subPageDto = _mapper.Map<SubPageDto>(subpage);
            return subPageDto;
        }

        public IEnumerable<SubPageDto> GetAllSubPages()
        {
            var subPages = _context.SubPages.ToList();

            var subPageDtos = _mapper.Map<List<SubPageDto>>(subPages);
            return subPageDtos;
        }

        public int CreateSubPage(SubPageDto subPageDto)
        {
            var subPage = _mapper.Map<SubPage>(subPageDto);
            _context.SubPages.Add(subPage);
            _context.SaveChanges();
            return subPage.Id;
        }

        public int UpdateSubPage(int id, SubPageDto subPageDto)
        {
            var subPage = _context.SubPages.FirstOrDefault(x => x.Id == id);

            if (subPage == null)
                throw new NotFoundException("SubPage not found");

            var updatedSubPage = _mapper.Map<SubPage>(subPageDto);
            _context.SubPages.Update(updatedSubPage);
            _context.SaveChanges();

            return subPage.Id;
        }

        public void DeleteSubPage(int id)
        {
            var subPage = _context.SubPages.FirstOrDefault(x => x.Id == id);

            if (subPage == null)
                throw new NotFoundException("SubPage not found");

            _context.SubPages.Remove(subPage);
            _context.SaveChanges();

        }

        //Contact---------------------------------------------------------------------------------------------------
        public ContactDto GetContact()
        {
            var contact = _context.SubPages
                .FirstOrDefault();

            if (contact == null)
                throw new NotFoundException("No contact details found");

            var contactDto = _mapper.Map<ContactDto>(contact);
            return contactDto;
        }

        public int UpdateContact(ContactDto contactDto)
        {
            var contact = _context.Contacts.FirstOrDefault();

            if (contact == null)
                throw new NotFoundException("Contact not found");

            var updatedContact = _mapper.Map<Contact>(contactDto);
            _context.Contacts.Update(updatedContact);
            _context.SaveChanges();

            return contact.Id;
        }

        //Categories---------------------------------------------------------------------------------------------------
        public TripCategoryDto GetCategoryById(int id)
        {
            var tripCategory = _context.TripCategories.FirstOrDefault(t => t.Id == id);

            if (tripCategory == null)
                throw new NotFoundException("Category not found");

            var tripCategoryDto = _mapper.Map<TripCategoryDto>(tripCategory);
            return tripCategoryDto;
        }

        public TripCategoryDetailsDto GetCategoryByName(string category)
        {
            var trips = _context.Trips
                .Include(t => t.TripCategory)
                .Include(t => t.TransportType)
                .Include(t => t.Reservations).ThenInclude(c => c.Participants)
                .Where(t => t.TripCategory.Name == category)
                .OrderByDescending(t => t.From)
                .ToList();

            var tripDtos = _mapper.Map<List<TripDto>>(trips);
            var tripCategoryDetailsDto = new TripCategoryDetailsDto() { Name = category, Trips = tripDtos };

            return tripCategoryDetailsDto;
        }

        public IEnumerable<TripCategoryDto> GetAllCategories()
        {
            var tripCategories = _context.TripCategories.ToList();

            var tripCategoryDtos = _mapper.Map<List<TripCategoryDto>>(tripCategories);
            return tripCategoryDtos;
        }

        public int CreateCategory(TripCategoryDto tripCategoryDto)
        {
            var tripCategory = _mapper.Map<TripCategory>(tripCategoryDto);
            _context.TripCategories.Add(tripCategory);
            _context.SaveChanges();
            return tripCategory.Id;
        }

        public int UpdateCategory(int id, TripCategoryDto tripCategoryDto)
        {
            var tripCategory = _context.TripCategories.FirstOrDefault(x => x.Id == id);

            if (tripCategory == null)
                throw new NotFoundException("Category not found");

            var updateTripCategory = _mapper.Map<TripCategory>(tripCategoryDto);
            _context.TripCategories.Update(updateTripCategory);
            _context.SaveChanges();

            return tripCategory.Id;
        }

        public void DeleteCategory(int id)
        {
            var tripCategory = _context.TripCategories.FirstOrDefault(x => x.Id == id);

            if (tripCategory == null)
                throw new NotFoundException("Category not found");

            _context.TripCategories.Remove(tripCategory);
            _context.SaveChanges();

        }

        //TransportTypes---------------------------------------------------------------------------------------------------
        public TransportTypeDto GetTransportTypeById(int id)
        {
            var type = _context.TransportTypes.FirstOrDefault(t => t.Id == id);

            if (type == null)
                throw new NotFoundException("Transport Type not found");

            var typeDto = _mapper.Map<TransportTypeDto>(type);
            return typeDto;
        }

        public IEnumerable<TransportTypeDto> GetAllTransportTypes()
        {
            var types = _context.TransportTypes.ToList();

            var typeDtos = _mapper.Map<List<TransportTypeDto>>(types);
            return typeDtos;
        }

        public int CreateTransportType(TransportTypeDto typeDto)
        {
            var type = _mapper.Map<TransportType>(typeDto);
            _context.TransportTypes.Add(type);
            _context.SaveChanges();
            return type.Id;
        }

        public int UpdateTransportType(int id, TransportTypeDto typeDto)
        {
            var type = _context.TransportTypes.FirstOrDefault(x => x.Id == id);

            if (type == null)
                throw new NotFoundException("Transport Type not found");

            var updateType = _mapper.Map<TransportType>(typeDto);
            _context.TransportTypes.Update(updateType);
            _context.SaveChanges();

            return type.Id;
        }

        public void DeleteTransportType(int id)
        {
            var type = _context.TransportTypes.FirstOrDefault(x => x.Id == id);

            if (type == null)
                throw new NotFoundException("Transport Type not found");

            _context.TransportTypes.Remove(type);
            _context.SaveChanges();

        }

        //Users---------------------------------------------------------------------------------------------------
        public IEnumerable<AppUserDto> GetUsers()
        {
            var users = _context.AppUsers.ToList();

            var userDtos = _mapper.Map<List<AppUserDto>>(users);
            return userDtos;
        }
    }
}
