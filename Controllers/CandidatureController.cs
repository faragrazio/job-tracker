using JobTracker.Models;
using JobTracker.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Controllers
{
    /// <summary>
    /// Gestisce tutte le operazioni per le candidature di lavoro.
    /// Segue la Clean Architecture: il Controller fa solo routing e orchestrazione,
    /// la logica di business è nei UseCases, l'accesso ai dati nel Repository.
    /// </summary>
    public class CandidatureController : Controller
    {
        private readonly GetAllCandidatureUseCase _getAll;
        private readonly GetCandidaturaByIdUseCase _getById;
        private readonly CreateCandidaturaUseCase _create;
        private readonly UpdateCandidaturaUseCase _update;
        private readonly DeleteCandidaturaUseCase _delete;
        private readonly GetStatsUseCase _stats;
        private readonly GetTimelineUseCase _timeline;

        /// <summary>
        /// Tutti gli UseCases vengono iniettati tramite Dependency Injection.
        /// Il Controller non sa come funzionano internamente — li usa e basta.
        /// </summary>
        public CandidatureController(
            GetAllCandidatureUseCase getAll,
            GetCandidaturaByIdUseCase getById,
            CreateCandidaturaUseCase create,
            UpdateCandidaturaUseCase update,
            DeleteCandidaturaUseCase delete,
            GetStatsUseCase stats,
            GetTimelineUseCase timeline)
        {
            _getAll = getAll;
            _getById = getById;
            _create = create;
            _update = update;
            _delete = delete;
            _stats = stats;
            _timeline = timeline;
        }

        // ==================== VIEWS ====================

        /// <summary>
        /// Mostra la lista delle candidature con filtri opzionali e contatori per la dashboard.
        /// </summary>
        public async Task<IActionResult> Index(string? stato, string? cerca)
        {
            var candidature = await _getAll.ExecuteAsync(stato, cerca);
            var stats = await _stats.ExecuteAsync();

            ViewBag.Stato = stato;
            ViewBag.Cerca = cerca;
            ViewBag.Totale = stats.Totale;
            ViewBag.Inviate = stats.Inviate;
            ViewBag.Colloqui = stats.Colloqui;
            ViewBag.Rifiutate = stats.Rifiutate;

            return View(candidature);
        }

        /// <summary>
        /// Restituisce il form vuoto per creare una nuova candidatura.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Valida e salva una nuova candidatura.
        /// In caso di successo reindirizza alla lista, altrimenti mostra il form con gli errori.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Candidatura candidatura)
        {
            if (ModelState.IsValid)
            {
                await _create.ExecuteAsync(candidatura);
                TempData["Success"] = "Candidatura aggiunta con successo!";
                return RedirectToAction(nameof(Index));
            }
            return View(candidatura);
        }

        /// <summary>
        /// Restituisce il form precompilato per modificare una candidatura esistente.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var candidatura = await _getById.ExecuteAsync(id);
            if (candidatura == null) return NotFound();
            return View(candidatura);
        }

        /// <summary>
        /// Valida e applica le modifiche a una candidatura esistente.
        /// Controlla che l'ID nella route corrisponda a quello nel model (protezione da manomissione).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Candidatura candidatura)
        {
            if (id != candidatura.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var success = await _update.ExecuteAsync(id, candidatura);
                if (!success) return NotFound();

                TempData["Success"] = "Candidatura aggiornata!";
                return RedirectToAction(nameof(Index));
            }
            return View(candidatura);
        }

        /// <summary>
        /// Elimina definitivamente una candidatura tramite ID.
        /// Usa POST per impedire eliminazioni accidentali via URL.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _delete.ExecuteAsync(id);
            TempData["Success"] = "Candidatura eliminata.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Mostra la vista di dettaglio in sola lettura per una singola candidatura.
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var candidatura = await _getById.ExecuteAsync(id);
            if (candidatura == null) return NotFound();
            return View(candidatura);
        }

        // ==================== API JSON PER HIGHCHARTS ====================

        /// <summary>
        /// API JSON — restituisce i contatori per stato per il grafico donut Highcharts.
        /// Chiamato via fetch() da charts.js.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ApiStats()
        {
            var stats = await _stats.ExecuteAsync();
            return Json(new { perStato = stats.PerStato });
        }

        /// <summary>
        /// API JSON — restituisce le candidature raggruppate per data per il grafico area Highcharts.
        /// Chiamato via fetch() da charts.js.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ApiTimeline()
        {
            var timeline = await _timeline.ExecuteAsync();
            return Json(new { date = timeline.Date, conteggi = timeline.Conteggi });
        }
    }
}