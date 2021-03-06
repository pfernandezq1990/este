using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reserva.Data.Models;
using Reserva.Data.VM;

namespace Reserva.Services
{
    public interface IBookingsService
    {
        Task<IEnumerable<Reservation>> List();

        Task<int> Create(Reservation reservation);

        Task<Reservation> GetById(int id);

        Task Update(Reservation reservation);

        Task<int> Delete(int? id);        

        // Task<string> GetContact(int contact_id);

    }
}