﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Objects;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DBManagement _context;

        public EventsController(DBManagement context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Getevents()
        {
            return await _context.events.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.ID)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            int count = 0;
            try
            {
                var exist = await _context.events.AnyAsync(c => c.StadiumID.Equals(@event.StadiumID) && @event.StartDate > c.StartDate && @event.StartDate < c.EndDate && @event.EndDate > c.StartDate && @event.EndDate < c.EndDate);
                if (exist == true)
                    return BadRequest();

                var stadiumExist = await _context.stadiums.FirstOrDefaultAsync(i => i.ID.Equals(@event.StadiumID));
                if (stadiumExist == null)
                    return BadRequest();
                var sectors = _context.sectors.Where(x => x.StadiumID.Equals(@event.StadiumID));

                foreach(var item in sectors)
                {
                    count += item.Capacidade;
                }
                stadiumExist.Capacidade = count;
                @event.Stadium = stadiumExist;
                int capacityEvent = @event.Percentage * @event.Stadium.Capacidade / 100;

                var sector = _context.sectors.Where(x => x.StadiumID.Equals(@event.StadiumID) && x.Capacidade / 2 < capacityEvent).OrderByDescending(x => x.Capacidade).First();
                if (sector == null)
                    return BadRequest("Não é possivel realizar evento");
                @event.AvailableSeats = sector.Capacidade;
                @event.Sector = sector;
                _context.events.Add(@event);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEvent", new { id = @event.ID }, @event);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);               
            }
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _context.events.Any(e => e.ID == id);
        }
    }
}
