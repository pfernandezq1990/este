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
using Reserva.Services;

namespace Reserva.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookingsController : ControllerBase {
        // private readonly ReservationDbContext _context;
        private readonly IBookingsService service;
        public BookingsController(IBookingsService _service) {
            service = _service;
        }

        // private bool ReservationsExist(int id) {
        //     return _context.Bookings.Any(b => b.Id == id);
        // }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Reservation>>> Get() {
        //     return await _context.Bookings.ToListAsync();
        // }

        // [HttpPost]
        // public async Task<ActionResult<Reservation>> Post([FromBody] Reservation reservation) {
        //     _context.Bookings.Add(reservation);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(Get), new {id = reservation.Id}, reservation);
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Reservation>> GetAction(int id) {
        //     var reservation = await _context.Bookings.FindAsync(id);
        //     if (reservation == null)
        //     {
        //         return NotFound();
        //     }

        //     return reservation;
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult<Reservation>> Put(int id, [FromBody] Reservation reservation) {
        //     if (id != reservation.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(reservation).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ReservationsExist(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;    
        //         }
                
        //     }

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Reservation>> Delete(int id) {
        //     var reservationToDelete = await _context.Bookings.FindAsync(id);
        //     if (reservationToDelete == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Bookings.Remove(reservationToDelete);
        //     await _context.SaveChangesAsync();

        //     return reservationToDelete;
        // }

        //Get Bookings

        [HttpGet]
        public async Task<IActionResult> Get() {
            try
            {
                var bookings = await service.List();
                if (bookings == null)
                {
                    return NotFound();
                }
                return Ok(bookings);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Create a reservation
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reservation reservation) {
            if (ModelState.IsValid)
            {
                try
                {
                    var bookingsId = await service.Create(reservation);
                    if (bookingsId > 0)
                    {
                        return Ok(bookingsId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        //Find reservations by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var reservation = await service.GetById(id);
            if (reservation != null)
            {
                return Ok(reservation);
            }
            else
            {
                return BadRequest();
            }
        }

        //Update a reservation

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] Reservation reservation) {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.Update(reservation);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")                    
                    {
                        return NotFound();
                    }
                    return BadRequest();                   
                }
            }
            return BadRequest();
        }

        //Delete reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id) {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await service.Delete(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}