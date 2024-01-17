using AutoMapper;
using FocusAPI.Data;
using FocusAPI.Models;
using FocusAPI.Services;

namespace FocusAPI.Data
{
    public class FocusSeeder
    {
        private readonly FocusDbContext _context;
        private readonly IAccountService _accountService;
        public FocusSeeder(FocusDbContext dbContext, IAccountService accountService)
        {
            _context = dbContext;
            _accountService = accountService;
        }
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.UserRoles.Any())
                {
                    var roles = GetRoles();
                    _context.UserRoles.AddRange(roles);
                    _context.SaveChanges();
                }
                if (!_context.AppUsers.Any())
                {
                    var users = GetUsers();
                    users.ForEach(x => _accountService.RegisterUser(x));
                }
                if (!_context.TripCategories.Any())
                {
                    var categories = GetTripCategories();
                    _context.TripCategories.AddRange(categories);
                    _context.SaveChanges();
                }
                if (!_context.TransportTypes.Any())
                {
                    var transportTypes = GetTransportTypes();
                    _context.TransportTypes.AddRange(transportTypes);
                    _context.SaveChanges();
                }
                if (!_context.Trips.Any())
                {
                    var trips = GetTrips();
                    _context.Trips.AddRange(trips);
                    _context.SaveChanges();
                }
                if (!_context.Reservations.Any())
                {
                    var reservations = GetReservations();
                    _context.Reservations.AddRange(reservations);
                    _context.SaveChanges();
                }
                if (!_context.Participants.Any())
                {
                    var participants = GetParticipants();
                    _context.Participants.AddRange(participants);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<UserRole> GetRoles()
        {
            var roles = new List<UserRole>()
            {
                new UserRole()
                {
                    Id = 1,
                    Name = "User"
                },
                new UserRole()
                {
                    Id = 2,
                    Name = "Admin"
                }
            };
            return roles;
        }

        private List<RegisterUserDto> GetUsers()
        {
            var users = new List<RegisterUserDto>()
            {
                new RegisterUserDto()
                {
                    UserName = "Admin",
                    Password = "admin",
                    FirstName = "Test",
                    LastName = "Admin",
                    PhoneNumber = "123123123",
                    Email = "remik007@gmail.com",
                    UserRoleId = 2,
                },
                new RegisterUserDto()
                {
                    UserName = "TestUser",
                    Password = "testuser",
                    FirstName = "Test",
                    LastName = "User",
                    PhoneNumber = "1231231232",
                    Email = "remik.majka@gmail.com",
                    UserRoleId = 1
                }
            };
            return users;
        }

        private IEnumerable<TripCategory> GetTripCategories()
        {
            var categories = new List<TripCategory>()
            {
                new TripCategory()
                {
                    Name = "Wczasy"
                },
                new TripCategory()
                {
                    Name = "Pielgrzymki"
                }
            };
            return categories;
        }

        private IEnumerable<TransportType> GetTransportTypes()
        {
            var transportTypes = new List<TransportType>()
            {
                new TransportType()
                {
                    Name = "Autokar"
                },
                new TransportType()
                {
                    Name = "Dojazd własny"
                },
                new TransportType()
                {
                    Name = "Samolot"
                }
            };
            return transportTypes;
        }

        private IEnumerable<Trip> GetTrips()
        {
            var trips = new List<Trip>()
            {
                new Trip()
                {
                    ShortName = "Porto",
                    Name = "Porto i Dolina Douro. Urokliwa architektura, klimatyczne uliczki, pyszne jedzenie i cudowni ludzie.",
                    ShortDescription = "Krótki opis",
                    Description = "Długi opis",
                    Prize = "2190",
                    OldPrize = "2390",
                    AvailableSeats = 20,
                    TransportTypeId = 1,
                    ImageUrl = "",
                    TripCategoryId = 1,
                    From = new DateTime(2024, 3, 19),
                    To = new DateTime(2024, 3, 22),
                    IsEnabled = true
                },
                new Trip()
                {
                    ShortName = "Porto2",
                    Name = "Porto i Dolina Douro. Urokliwa architektura, klimatyczne uliczki, pyszne jedzenie i cudowni ludzie.",
                    ShortDescription = "Krótki opis",
                    Description = "Długi opis",
                    Prize = "1590",
                    OldPrize = "1690",
                    AvailableSeats = 20,
                    TransportTypeId = 1,
                    ImageUrl = "",
                    TripCategoryId = 1,
                    From = new DateTime(2024, 2, 19),
                    To = new DateTime(2024, 2, 22),
                    IsEnabled = true
                },
                new Trip()
                {
                    ShortName = "Porto3",
                    Name = "Porto i Dolina Douro. Urokliwa architektura, klimatyczne uliczki, pyszne jedzenie i cudowni ludzie.",
                    ShortDescription = "Krótki opis",
                    Description = "Długi opis",
                    Prize = "1590",
                    OldPrize = "1690",
                    AvailableSeats = 20,
                    TransportTypeId = 1,
                    ImageUrl = "",
                    TripCategoryId = 1,
                    From = new DateTime(2024, 2, 19),
                    To = new DateTime(2024, 2, 22),
                    IsEnabled = false
                },
                new Trip()
                {
                    ShortName = "Watykan",
                    Name = "Porto i Dolina Douro. Urokliwa architektura, klimatyczne uliczki, pyszne jedzenie i cudowni ludzie.",
                    ShortDescription = "Krótki opis",
                    Description = "Długi opis",
                    Prize = "2590",
                    OldPrize = "2790",
                    AvailableSeats = 20,
                    TransportTypeId = 1,
                    ImageUrl = "",
                    TripCategoryId = 1,
                    From = new DateTime(2024, 2, 19),
                    To = new DateTime(2024, 2, 26),
                    IsEnabled = true
                },
                new Trip()
                {
                    ShortName = "Watykan2",
                    Name = "Porto i Dolina Douro. Urokliwa architektura, klimatyczne uliczki, pyszne jedzenie i cudowni ludzie.",
                    ShortDescription = "Krótki opis",
                    Description = "Długi opis",
                    Prize = "1590",
                    OldPrize = "1690",
                    AvailableSeats = 20,
                    TransportTypeId = 1,
                    ImageUrl = "",
                    TripCategoryId = 1,
                    From = new DateTime(2024, 3, 1),
                    To = new DateTime(2024, 3, 4),
                    IsEnabled = true
                },
                new Trip()
                {
                    ShortName = "Watykan3",
                    Name = "Porto i Dolina Douro. Urokliwa architektura, klimatyczne uliczki, pyszne jedzenie i cudowni ludzie.",
                    ShortDescription = "Krótki opis",
                    Description = "Długi opis",
                    Prize = "1590",
                    OldPrize = "1690",
                    AvailableSeats = 20,
                    TransportTypeId = 1,
                    ImageUrl = "",
                    TripCategoryId = 1,
                    From = new DateTime(2024, 3, 1),
                    To = new DateTime(2024, 3, 4),
                    IsEnabled = false
                }
            };
            return trips;
        }

        private IEnumerable<Reservation> GetReservations()
        {
            var reservations = new List<Reservation>()
            {
                new Reservation()
                {
                    OwnerId = 2,
                    TripId = 1,
                    DateCreated = DateTime.Now,
                    IsConfirmed = true,
                    IsPaid = false
                }
            };
            return reservations;
        }

        private IEnumerable<Participant> GetParticipants()
        {
            DateTime test = DateTime.Parse("1992-10-10");
            var participants = new List<Participant>()
            {
                new Participant()
                {
                    FirstName = "Adam",
                    LastName = "Małysz",
                    Birthday = new DateTime(1992, 10, 20),
                    DocumentNumber = "ASD123",
                    PhoneNumber = "555666777",
                    ReservationId = 1
                },
                new Participant()
                {
                    FirstName = "Iza",
                    LastName = "Małysz",
                    Birthday = new DateTime(1995, 12, 22),
                    DocumentNumber = "ASD111",
                    PhoneNumber = "555444333",
                    ReservationId = 1
                }
            };
            return participants;
        }
    }
}