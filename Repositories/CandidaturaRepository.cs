using JobTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Repositories
{
    /// <summary>
    /// Implementazione concreta del repository — qui dentro c'è MySQL tramite EF Core.
    /// Se domani vuoi cambiare database, crei un'altra classe che implementa ICandidaturaRepository
    /// e il Controller non cambia di una riga.
    /// </summary>
    public class CandidaturaRepository : ICandidaturaRepository
    {
        private readonly AppDbContext _db;

        // Riceve il DbContext dall'esterno (DI) — stesso principio del Controller
        public CandidaturaRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Candidatura>> GetAllAsync(string? stato, string? cerca)
        {
            // Parte dalla tabella completa
            var query = _db.Candidature.AsQueryable();

            // Filtro per stato (se specificato)
            if (!string.IsNullOrEmpty(stato))
                query = query.Where(c => c.Stato == stato);

            // Ricerca per azienda o ruolo (se specificato)
            if (!string.IsNullOrEmpty(cerca))
                query = query.Where(c =>
                    c.Azienda.Contains(cerca) ||
                    c.Ruolo.Contains(cerca));

            // Ordina per data candidatura decrescente (le più recenti prima)
            return await query
                .OrderByDescending(c => c.DataCandidatura)
                .ToListAsync();
        }

        public async Task<Candidatura?> GetByIdAsync(int id)
        {
            return await _db.Candidature.FindAsync(id);
        }

        public async Task CreateAsync(Candidatura candidatura)
        {
            _db.Candidature.Add(candidatura);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Candidatura candidatura)
        {
            // Cerca se esiste già un'entity tracciata con lo stesso ID
            var tracked = _db.ChangeTracker.Entries<Candidatura>().FirstOrDefault(e => e.Entity.Id == candidatura.Id);
            // Se esiste, la stacca dal tracker per evitare conflitti
            if (tracked != null)
            {
                tracked.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }

            _db.Candidature.Update(candidatura);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var candidatura = await _db.Candidature.FindAsync(id);
            if (candidatura != null)
            {
                _db.Candidature.Remove(candidatura);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync()
        {
            return await _db.Candidature.CountAsync();
        }

        public async Task<int> CountByStatoAsync(string stato)
        {
            return await _db.Candidature.CountAsync(c => c.Stato == stato);
        }
    }
}