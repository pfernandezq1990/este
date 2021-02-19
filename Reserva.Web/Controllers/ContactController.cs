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

    public class ContactController : ControllerBase {
        private readonly ReservationDbContext _context;

        public ContactController(ReservationDbContext context) {
            _context = context;
        }
        private bool ContactExist(int id)
        {
            return _context.Contacts.Any(c => c.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get() {
            return await _context.Contacts.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> Post([FromBody] Contact contact) {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Get), new {id = contact.Id}, contact);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id){
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> Put(int id, [FromBody] Contact contact) {
            if (id != contact.Id)
            {
                return BadRequest();
            }
    
            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExist(id))
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
        public async Task<ActionResult<Contact>> Delete(int id) {

            var contactToDelete = await _context.Contacts.FindAsync(id);
            if (contactToDelete == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contactToDelete);
            await _context.SaveChangesAsync();

            return contactToDelete;
        }
    }
}