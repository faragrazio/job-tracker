using JobTracker.Models;
using JobTracker.Repositories;

namespace JobTracker.UseCases
{
    /// <summary>
    /// Aggiorna una candidatura esistente con validazioni di business.
    /// Verifica che la candidatura esista prima di tentare l'aggiornamento.
    /// </summary>
    public class UpdateCandidaturaUseCase
    {
        private readonly ICandidaturaRepository _repo;

        public UpdateCandidaturaUseCase(ICandidaturaRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Restituisce true se l'aggiornamento è andato a buon fine, false se l'ID non esiste.
        /// </summary>
        public async Task<bool> ExecuteAsync(int id, Candidatura candidatura)
        {
            var esistente = await _repo.GetByIdAsync(id);
            if (esistente == null) return false;

            await _repo.UpdateAsync(candidatura);
            return true;
        }
    }
}