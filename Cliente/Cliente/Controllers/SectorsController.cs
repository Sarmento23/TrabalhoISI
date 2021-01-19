using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente.Models;
using Cliente.Comunication;

namespace Cliente.Controllers
{
    public class SectorsController : Controller
    {
    

        public async Task<IActionResult> Index()
        {
            var sectors = await SectorComm.GetSectors();
            foreach(var item in sectors)
            {
                item.Stadium = await StadiumComm.GetStadium(item.StadiumId);
            }
            return View(sectors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sector = await SectorComm.GetSector(id);
            sector.Stadium = await StadiumComm.GetStadium(sector.StadiumId);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Colunas,Linhas,StadiumId")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                var created = SectorComm.PostSector(sector);
                if(created != null)
                    return RedirectToAction(nameof(Index));
            }
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name", sector.StadiumId);
            return View(sector);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var sector = await SectorComm.GetSector(id);
            if (sector == null)
            {
                return NotFound();
            }
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name", sector.StadiumId);
            return View(sector);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Colunas,Linhas,StadiumId")] Sector sector)
        {
            if (id != sector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await SectorComm.PutSector(id, sector);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (SectorComm.GetSector(id)==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name", sector.StadiumId);
            return View(sector);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sector = await SectorComm.GetSector(id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sector = await SectorComm.DeleteSector(id);
            if(sector == true)
                return RedirectToAction(nameof(Index));
            return View(sector);
        }
    }
}
