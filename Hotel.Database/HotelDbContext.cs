using System.ComponentModel;
using System.Data.Common;
using Hotel.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Database
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options) : base (options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<MethodOfPayment> MethodOfPayments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomType>()
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Room>()
                .HasIndex(p => p.Number)
                .IsUnique(true);
        }
    }
}