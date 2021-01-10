using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StadiumsController : ControllerBase
    {
        private readonly DBManagement _context;

        public StadiumsController(DBManagement context)
        {
            _context = context;
        }

        // GET: api/Stadia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stadium>>> Getstadiums()
        {
            int count = 0;
            var stadiums = await _context.stadiums.Include(c => c.Sectors).ToListAsync();
            foreach(var item in stadiums)
            {
               foreach(var sector in item.Sectors)
                {
                    count += sector.Linhas * sector.Colunas;
                }
                item.Capacidade = count;
                count = 0;
            }
            return stadiums;
        }

        // GET: api/Stadia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stadium>> GetStadium(int id)
        {
            try
            {
                int count = 0;
                var stadium = await _context.stadiums.Include(c=>c.Sectors).FirstOrDefaultAsync(c => c.ID.Equals(id));

                if (stadium == null)
                {
                    return NotFound();
                }
                foreach (Sector sector in stadium.Sectors)
                {
                    count += sector.Linhas * sector.Colunas;
                }
                stadium.Capacidade = count;
                return stadium;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // PUT: api/Stadia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStadium(int id, Stadium stadium)
        {
            if (id != stadium.ID)
            {
                return BadRequest();
            }

            _context.Entry(stadium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StadiumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(stadium);
        }

        // POST: api/Stadia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stadium>> PostStadium(Stadium stadium)
        {
            var exist = await _context.stadiums.AnyAsync(c => c.Name.ToLower().Equals(stadium.Name.ToLower()));

            if (exist == true)
                return BadRequest();


            _context.stadiums.Add(stadium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStadium", new { id = stadium.ID }, stadium);
        }

        // DELETE: api/Stadia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStadium(int id)
        {
            var stadium = await _context.stadiums.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }

            _context.stadiums.Remove(stadium);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool StadiumExists(int id)
        {
            return _context.events.Any(e => e.ID == id);
        }
    }
}
