using JobTracker.Repositories;

namespace JobTracker.UseCases
{
    /// <summary>
    /// Elimina una candidatura per ID.
    /// Il Repository gestisce internamente il caso in cui l'ID non esiste.
    /// </summary>
    public class DeleteCandidaturaUseCase
    {
        private readonly ICandidaturaRepository _repo;

        public DeleteCandidaturaUseCase(ICandidaturaRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}