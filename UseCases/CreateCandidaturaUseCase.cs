using JobTracker.Models;
using JobTracker.Repositories;

namespace JobTracker.UseCases
{
    /// <summary>
    /// Crea una nuova candidatura dopo aver applicato le regole di business.
    /// Qui puoi aggiungere validazioni extra che non riguardano il database
    /// (es. impedire duplicati per la stessa azienda e ruolo).
    /// </summary>
    public class CreateCandidaturaUseCase
    {
        private readonly ICandidaturaRepository _repo;

        public CreateCandidaturaUseCase(ICandidaturaRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Candidatura candidatura)
        {
            // Regola di business: se lo stato non è specificato, imposta "Inviata" come default
            if (string.IsNullOrEmpty(candidatura.Stato))
            {
                candidatura.Stato = "Inviata";
            }

            await _repo.CreateAsync(candidatura);
        }
    }
}