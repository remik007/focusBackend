using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Data
{
    public class FocusDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<SubPage> SubPages { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripType> TripTypes { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public FocusDbContext(DbContextOptions<FocusDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserName).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(16);
                entity.Property(e => e.FirstName).HasMaxLength(255);
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.Property(e => e.UserRoleId).IsRequired();

                entity.HasData(
                    new AppUser
                    {
                        Id = 1,
                        UserName = "Admin",
                        Password = "admin",
                        FirstName = "Test",
                        LastName = "Admin",
                        PhoneNumber = "123123123",
                        Email = "remik007@gmail.com",
                        UserRoleId = 2
                    },
                    new AppUser
                    {
                        Id = 2,
                        UserName = "TestUser",
                        Password = "testuser",
                        FirstName = "Test",
                        LastName = "User",
                        PhoneNumber = "1231231232",
                        Email = "remik0072@gmail.com",
                        UserRoleId = 1
                    }
                );

            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasData(
                    new UserRole
                    {
                        Id = 1,
                        Name = "User"
                    },
                    new UserRole
                    {
                        Id = 2,
                        Name = "Admin"
                    }
                );
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.AddressLine1).HasMaxLength(255);
                entity.Property(e => e.AddressLine2).HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).HasMaxLength(16);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Facebook).HasMaxLength(255);
                entity.Property(e => e.Instagram).HasMaxLength(255);
                entity.Property(e => e.Google).HasMaxLength(255);
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(255).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Birthday).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(16);
                entity.Property(e => e.DocumentNumber).HasMaxLength(255);
                entity.Property(e => e.ReservationId).IsRequired();
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<SubPage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ShortName).HasMaxLength(63).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(1023).IsRequired();
                entity.Property(e => e.ShortDescription).HasMaxLength(2047);
                entity.Property(e => e.Description).HasMaxLength(20479);
                entity.Property(e => e.ImageUrl).HasMaxLength(1023);
            });

            modelBuilder.Entity<TransportType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<TripType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ShortName).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(1023).IsRequired();
                entity.Property(e => e.ShortDescription).HasMaxLength(2047);
                entity.Property(e => e.Description).HasMaxLength(20479);
                entity.Property(e => e.Prize).HasMaxLength(32);
                entity.Property(e => e.OldPrize).HasMaxLength(32);
                entity.Property(e => e.AvailableSeats).IsRequired();
                entity.Property(e => e.From).IsRequired();
                entity.Property(e => e.To).IsRequired();
                entity.Property(e => e.ImageUrl).HasMaxLength(1023);
            });
        }
    }
}
