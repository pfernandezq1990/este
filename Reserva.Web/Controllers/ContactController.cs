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

    public class ContactController : ControllerBase {

        private readonly IContactService service;
        public ContactController(IContactService _service) {
            service = _service;
        }
        // private bool ContactExist(int id)
        // {
        //     return _service.Exist(id);
        // }


        // Get all Contacts
        [HttpGet]
        public async Task<IActionResult> Get() {
            try
            {
                var contacts = await service.List();
                if (contacts == null)
                {
                    return NotFound();
                }

                return Ok(contacts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        //Create a Contact
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact) {
            if (ModelState.IsValid)
            {
                try
                {
                    var contactId = await service.Create(contact);
                    if (contactId > 0)
                    {
                        return Ok(contactId);
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

        //Find Contact by Id

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var contact = await service.GetById(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            else
            {
                return BadRequest();
            }
        }

        // Update a contact

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Contact contact) {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.Update(contact);

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

        // Delete a contact
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

        // [HttpGet]
        // [Route("booking/{id}")]
        // public async Task<IActionResult> GetBookingsContac(int contact_id) {
        //     try
        //     {
        //         var bookings = service.GetReservations(contact_id);
        //         if (bookings == null)
        //         {
        //             return NotFound();
        //         }
        //         return Ok(bookings);
        //     }
        //     catch (Exception)
        //     {                
        //         return BadRequest();
        //     }
        // }
    }
}