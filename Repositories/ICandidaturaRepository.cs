using JobTracker.Models;

namespace JobTracker.Repositories
{
    /// <summary>
    /// Contratto per l'accesso ai dati delle candidature.
    /// Il Controller parla con questa interfaccia — non sa se sotto c'è MySQL, MongoDB o altro.
    /// Principio D di SOLID: dipendi dall'astrazione, non dall'implementazione concreta.
    /// </summary>
    public interface ICandidaturaRepository
    {
        // Restituisce tutte le candidature (con filtri opzionali)
        Task<List<Candidatura>> GetAllAsync(string? stato, string? cerca);

        // Restituisce una singola candidatura per ID
        Task<Candidatura?> GetByIdAsync(int id);

        // Aggiunge una nuova candidatura
        Task CreateAsync(Candidatura candidatura);

        // Aggiorna una candidatura esistente
        Task UpdateAsync(Candidatura candidatura);

        // Elimina una candidatura per ID
        Task DeleteAsync(int id);

        // Contatori per la dashboard
        Task<int> CountAsync();
        Task<int> CountByStatoAsync(string stato);
    }
}