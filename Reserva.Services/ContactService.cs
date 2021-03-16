using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reserva.Data;
using Reserva.Data.Models;
using Npgsql;
using System.Data;
using Reserva.Data.VM;

namespace Reserva.Services
{
    public class ContactService : IContactService
    {

        private readonly ReservationDbContext _context;

        public ContactService(ReservationDbContext context)
        {
            _context = context;
        }

        private bool Exist(int id)
        {
            return _context.Contacts.Any(c => c.Id == id);
        }

        public async Task<IEnumerable<Contact>> List()
        {            
            return await _context.Contacts.ToListAsync();
        }

        public async Task<int> Create(Contact contact)
        {
            if (_context != null)
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return contact.Id;
            }

            return 0;
        }

        public async Task<Contact> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                return contact;                
            }
            else
            {
                return null;
            }
        }

        public async Task Update(Contact contact)
        {
           _context.Contacts.Update(contact);
           await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int? id)
        {
            int result = 0;
            
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id );

            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                result =  await _context.SaveChangesAsync();

            }
            return result;
        }

        // public async Task<IEnumerable<reservation>> GetReservations(int contact_id)
        // {
        //     var tem = _context.Bookings.FromSqlRaw("Execute dbo.getbooking {0}", contact_id).ToListAsync();
        //     var result = new List<reservation>();
        //     foreach (var item in tem)
        //     {
                
        //     }
        // }



        // public async Task<IEnumerable<Reservation>> GetReservations(int contact_id) {                      

        //     var result = new List<Reservation>();
        //     NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=reserva;");
        //     conn.Open();            
        //     NpgsqlTransaction tran = conn.BeginTransaction();

        //     using (var cmd = new NpgsqlCommand("getbooking", conn))
        //     {
        //         cmd.CommandType = CommandType.StoredProcedure;
        //         cmd.Parameters.AddWithValue("contact_id", contact_id);

        //         using (var reader = cmd.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 var id = (int)reader[0];
        //                 var title = (string)reader[1];
        //                 var date = (DateTime)reader[2];
        //                 var contactId = (int)reader[3];
        //                 var favorite = (bool)reader[4];

        //                 var reserva = new Reservation {
        //                     Id = id,
        //                     Title = title,
        //                     Date = date,
        //                     ContactId = contactId,
        //                     Favorite = favorite                            
        //                 };
        //             //    var title = (String)reader[0];
        //             //    var date = (DateTime)reader[1];

        //             //     bookings temp = new bookings() {
        //             //         Title = title,
        //             //         Date = date
        //             //     };


        //                 result.Add(reserva);

        //             }
        //         }
        //     }
        //     return result;
        // }

    }
}