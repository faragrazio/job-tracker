using JobTracker.Models;
using JobTracker.Repositories;

namespace JobTracker.UseCases
{
    /// <summary>
    /// Recupera la lista delle candidature applicando filtri opzionali per stato e testo di ricerca.
    /// Separato dal Controller perché la logica di filtraggio è responsabilità del dominio, non del layer HTTP.
    /// </summary>
    public class GetAllCandidatureUseCase
    {
        private readonly ICandidaturaRepository _repo;

        public GetAllCandidatureUseCase(ICandidaturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Candidatura>> ExecuteAsync(string? stato, string? cerca)
        {
            return await _repo.GetAllAsync(stato, cerca);
        }
    }
}