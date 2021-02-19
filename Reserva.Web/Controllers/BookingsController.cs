using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Reserva.Data;
using Reserva.Data.Models;

namespace Reserva.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookingsController : ControllerBase {
        private readonly ReservationDbContext _context;

        public BookingsController(ReservationDbContext context) {
            _context =context;
        }

        private bool ReservationsExist(int id) {
            return _context.Bookings.Any(b => b.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> Get() {
            return await _context.Bookings.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> Post([FromBody] Reservation reservation) {
            _context.Bookings.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new {id = reservation.Id}, reservation);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetAction(int id) {
            var reservation = await _context.Bookings.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Reservation>> Put(int id, [FromBody] Reservation reservation) {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationsExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;    
                }
                
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> Delete(int id) {
            var reservationToDelete = await _context.Bookings.FindAsync(id);
            if (reservationToDelete == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(reservationToDelete);
            await _context.SaveChangesAsync();

            return reservationToDelete;
        }
    }
}