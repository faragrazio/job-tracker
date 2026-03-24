using JobTracker.Models;
using JobTracker.Repositories;

namespace JobTracker.UseCases
{
    /// <summary>
    /// Recupera una singola candidatura per ID.
    /// Restituisce null se l'ID non esiste — il Controller decide come gestire il 404.
    /// </summary>
    public class GetCandidaturaByIdUseCase
    {
        private readonly ICandidaturaRepository _repo;

        public GetCandidaturaByIdUseCase(ICandidaturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<Candidatura?> ExecuteAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
    }
}