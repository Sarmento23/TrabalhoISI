using System;
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
    public class SectorsController : ControllerBase
    {
        private readonly DBManagement _context;

        public SectorsController(DBManagement context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve lista de todos os sectores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sector>>> Getsectors()
        {
            return await _context.sectors.ToListAsync();
        }

        /// <summary>
        /// Devolve Sector pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Sector>> GetSector(int id)
        {
            var sector = await _context.sectors.Include(c => c.Stadium).FirstOrDefaultAsync(c=>c.ID.Equals(id));
            
            if (sector == null)
            {
                return NotFound();
            }
            
            return sector;
        }

        
        /// <summary>
        /// Procurar sectores pertencentes a um estádio
        /// </summary>
        /// <param name="idStadium"></param>
        /// <returns></returns>
        [HttpGet("stadium/{idStadium}")]
        public async Task<ActionResult<IEnumerable<Sector>>> GetSectorsByStadiumID(int idStadium)
        {
            var sector = await _context.sectors.Where(c=>c.StadiumID.Equals(idStadium)).ToListAsync();

            if (sector == null)
            {
                return NotFound();
            }
            return sector;
        }

        /// <summary>
        /// Alterar dados de um sector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sector"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSector(int id, Sector sector)
        {
            if (id != sector.ID)
            {
                return BadRequest();
            }

            _context.Entry(sector).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectorExists(id))
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

        /// <summary>
        /// Introduzir novo sector
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Sector>> PostSector(Sector sector)
        {
            int count = 0;
            try
            {
                var existe = await _context.sectors.AnyAsync(c => c.Name.Equals(sector.Name) && c.StadiumID == sector.StadiumID);

                if (existe == true)
                    return BadRequest("Sector já existe");

                var stadiumExist = await _context.stadiums.FindAsync(sector.StadiumID);

                if (stadiumExist == null)
                    return BadRequest("Estádio não existe");

                sector.Capacidade = sector.Linhas*sector.Colunas;
                stadiumExist.Sectors.Add(sector);
                sector.Stadium = stadiumExist;
                _context.sectors.Add(sector);
                foreach(var setor in stadiumExist.Sectors)
                {
                    stadiumExist.Capacidade += count;
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSector", new { id = sector.ID }, sector);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Apaga o sector pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSector(int id)
        {
            var sector = await _context.sectors.FindAsync(id);
            if (sector == null)
            {
                return NotFound();
            }

            _context.sectors.Remove(sector);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se o sector existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool SectorExists(int id)
        {
            return _context.sectors.Any(e => e.ID == id);
        }
    }
}
