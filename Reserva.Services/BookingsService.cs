using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reserva.Data;
using Reserva.Data.Models;

namespace Reserva.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly ReservationDbContext _context;

        public BookingsService(ReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> List()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<int> Create(Reservation reservation)
        {
            if (_context != null)
            {
                _context.Bookings.Add(reservation);
                await _context.SaveChangesAsync();

                return reservation.Id;
            }

            return 0;
        }

        public async Task<Reservation> GetById(int id)
        {
            var reservation = await _context.Bookings.FindAsync(id);
            if (reservation != null)
            {
                return reservation;
            }
            else
            {
                return null;
            }
        }

        public async Task Update(Reservation reservation)
        {
            _context.Bookings.Update(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int? id)
        {
            int result = 0;
            var reservation = await _context.Bookings.FirstOrDefaultAsync(r => r.Id==id);

            if (reservation != null)
            {
                _context.Bookings.Remove(reservation);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

    }
}