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
    public class TicketsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var iSITableContext = await TicketsComm.GetTickets();
            return View(iSITableContext);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await TicketsComm.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["EventId"] = new SelectList(await EventComm.GetEvents(), "Id", "Id");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdeptName,AdeptContact,StadiumId,EventId,SectorName,Local")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var ticketCreated = await TicketsComm.PostTicket(ticket);
                if(ticketCreated != null)
                    return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(await EventComm.GetEvents(), "Id", "Id");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await TicketsComm.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(await EventComm.GetEvents(), "Id", "Id");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdeptName,AdeptContact,StadiumId,EventId,SectorName,Local")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existe = TicketsComm.GetTicket(id);
                    if (existe != null)
                        await TicketsComm.PutTicket(id,ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (TicketsComm.GetTicket(id) == null)
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
            ViewData["EventId"] = new SelectList(await EventComm.GetEvents(), "Id", "Id");
            ViewData["StadiumId"] = new SelectList(await StadiumComm.GetStadiums(), "Id", "Name");
            return View(ticket);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await TicketsComm.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await TicketsComm.DeleteTicket(id);
            if(ticket == true)
                return RedirectToAction(nameof(Index));
            return View(ticket);
        }
    }
}
