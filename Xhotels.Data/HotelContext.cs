using Microsoft.EntityFrameworkCore;
using Xhotels.Data.Models;

namespace Xhotels.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }

    }
}
