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
    public class StadiaController : Controller
    {
        private readonly ISITableContext _context;

        public StadiaController(ISITableContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await StadiumComm.GetStadiums());
        }

        public async Task<IActionResult> Details(int id)
        {
            var stadium = await StadiumComm.GetStadium(id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                var stadiumCreated = await StadiumComm.PostStadium(stadium);
                if(stadiumCreated != null)
                return RedirectToAction(nameof(Index));
            }
            return View(stadium);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var stadium = await StadiumComm.GetStadium(id);
            if (stadium == null)
            {
                return NotFound();
            }
            return View(stadium);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Stadium stadium)
        {
            if (id != stadium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await StadiumComm.PutStadium(id,stadium);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (StadiumComm.GetStadium(stadium.Id)==null)
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
            return View(stadium);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var stadium = await StadiumComm.GetStadium(id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadium = await StadiumComm.DeleteStadium(id);
            if(stadium == true)
                return RedirectToAction(nameof(Index));
            return View(stadium);
        }
    }
}
