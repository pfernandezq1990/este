using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Reserva.Data;
using Reserva.Data.Models;
using Reserva.Data.VM;

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
            var reservation = await _context.Bookings.FirstOrDefaultAsync(r => r.Id == id);

            if (reservation != null)
            {
                _context.Bookings.Remove(reservation);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        // public async Task<string> GetContact(int contact_id)
        // {
        //     // return await _context.Database.ExecuteSqlRaw("SELECT public.getcontact", contact_id);
        //     // var result = new contactvm{
        //     //     Name = tem.Name,
        //     // }
            
        //     var result = new contactvm();
        //     NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=reserva;");
        //     conn.Open();
        //     NpgsqlTransaction tran = conn.BeginTransaction();

        //     using (var cmd = new NpgsqlCommand("getContact", conn))
        //     {
        //         cmd.CommandType = CommandType.StoredProcedure;
        //         cmd.Parameters.AddWithValue("reservation_id", contact_id);

        //         using (var reader = cmd.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 var name = reader[0];
        //                 var type = reader[1];
        //                 var phone = reader[2];

        //                 result = new contactvm
        //                 {
        //                     Name = name.ToString(),
        //                     ContactType = type.ToString(),
        //                     Phone = phone.ToString(),
        //                 };

        //                 tran.Commit();
        //                 conn.Close();
        //             }
        //         }
        //     }
        //     return result.Name;
            
        // }

    }


}