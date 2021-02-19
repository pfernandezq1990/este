using Microsoft.EntityFrameworkCore;
using Reserva.Data.Models;


namespace Reserva.Data{
    public class ReservationDbContext : DbContext {
        public ReservationDbContext() {
        }

        public ReservationDbContext(DbContextOptions options) : base (options) {
        }

        public virtual DbSet<Reservation> Bookings{ get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        
    }
}