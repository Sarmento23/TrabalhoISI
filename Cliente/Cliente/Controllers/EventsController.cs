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
    public class EventsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var events = await EventComm.GetEvents();
            foreach (var item in events)
            {
                item.Stadium = await StadiumComm.GetStadium(item.StadiumId);
                item.Sector = await SectorComm.GetSector(item.SectorId);
            }
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {

            var @event = await EventComm.GetEvent(id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["SectorId"] = new SelectList(await SectorComm.GetSectors(), "Id", "Name");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StadiumId,SectorId,Percentage,AvailableSeats,StartDate,EndDate")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var created = await EventComm.PostEvent(@event);
                if(created != null)
                    return RedirectToAction(nameof(Index));
            }
            ViewData["SectorId"] = new SelectList(await SectorComm.GetSectors(), "Id", "Name");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View(@event);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var @event = await EventComm.GetEvent(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["SectorId"] = new SelectList(await SectorComm.GetSectors(), "Id", "Name");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StadiumId,SectorId,Percentage,AvailableSeats,StartDate,EndDate")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await EventComm.PutEvent(id, @event); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (EventComm.GetEvent(id)==null)
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
            ViewData["SectorId"] = new SelectList(await SectorComm.GetSectors(), "Id", "Name");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View(@event);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var @event = EventComm.GetEvent(id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await EventComm.DeleteEvent(id);
            if(@event == true)
                return RedirectToAction(nameof(Index));
            return View(@event);
        }
    }
}
