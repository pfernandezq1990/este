using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reserva.Data.Models;
using Reserva.Data.VM;

namespace Reserva.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> List();

        Task<int> Create(Contact contact);

        Task<Contact> GetById(int id);

        Task Update(Contact contact);

        Task<int> Delete(int? id);

        // Task<IEnumerable<reservation>> GetReservations(int contact_id);

        // Task GetBookings(int contact_id);



    }
}