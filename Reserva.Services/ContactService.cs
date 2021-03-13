using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reserva.Data;
using Reserva.Data.Models;

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
        
    }
}