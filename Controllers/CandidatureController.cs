using JobTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Controllers
{
    public class CandidatureController : Controller
    {
        private readonly AppDbContext _db;

        // Il DbContext viene iniettato automaticamente da ASP.NET (principio D - SOLID)
        public CandidatureController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Candidature
        public async Task<IActionResult> Index(string? stato, string? cerca)
        {
            var query = _db.Candidature.AsQueryable();

            // Filtro per stato
            if (!string.IsNullOrEmpty(stato))
                query = query.Where(c => c.Stato == stato);

            // Ricerca per azienda o ruolo
            if (!string.IsNullOrEmpty(cerca))
                query = query.Where(c => 
                    c.Azienda.Contains(cerca) || 
                    c.Ruolo.Contains(cerca));

            var candidature = await query
                .OrderByDescending(c => c.DataCandidatura)
                .ToListAsync();

            ViewBag.Stato = stato;
            ViewBag.Cerca = cerca;
            ViewBag.Totale = await _db.Candidature.CountAsync();
            ViewBag.Inviate = await _db.Candidature.CountAsync(c => c.Stato == "Inviata");
            ViewBag.Colloqui = await _db.Candidature.CountAsync(c => c.Stato == "Colloquio");
            ViewBag.Rifiutate = await _db.Candidature.CountAsync(c => c.Stato == "Rifiutata");

            return View(candidature);
        }

        // GET: /Candidature/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Candidature/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Candidatura candidatura)
        {
            if (ModelState.IsValid)
            {
                _db.Candidature.Add(candidatura);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Candidatura aggiunta con successo!";
                return RedirectToAction(nameof(Index));
            }
            return View(candidatura);
        }

        // GET: /Candidature/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var candidatura = await _db.Candidature.FindAsync(id);
            if (candidatura == null) return NotFound();
            return View(candidatura);
        }

        // POST: /Candidature/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Candidatura candidatura)
        {
            if (id != candidatura.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _db.Candidature.Update(candidatura);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Candidatura aggiornata!";
                return RedirectToAction(nameof(Index));
            }
            return View(candidatura);
        }

        // POST: /Candidature/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var candidatura = await _db.Candidature.FindAsync(id);
            if (candidatura != null)
            {
                _db.Candidature.Remove(candidatura);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Candidatura eliminata.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Candidature/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var candidatura = await _db.Candidature.FindAsync(id);
            if (candidatura == null) return NotFound();
            return View(candidatura);
        }
    }
}