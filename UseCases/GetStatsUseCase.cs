using JobTracker.DTOs;
using JobTracker.Repositories;

namespace JobTracker.UseCases
{
    /// <summary>
    /// Genera le statistiche aggregate per la dashboard e il grafico donut Highcharts.
    /// Restituisce i contatori per ogni stato (Inviata, Visualizzata, Colloquio, Rifiutata, Offerta).
    /// </summary>
    public class GetStatsUseCase
    {
        private readonly ICandidaturaRepository _repo;

        public GetStatsUseCase(ICandidaturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<StatsResult> ExecuteAsync()
        {
            var stati = new[] { "Inviata", "Visualizzata", "Colloquio", "Rifiutata", "Offerta" };
            var perStato = new List<object>();

            foreach (var stato in stati)
            {
                var count = await _repo.CountByStatoAsync(stato);
                if (count > 0)
                {
                    perStato.Add(new { name = stato, y = count });
                }
            }

            return new StatsResult
            {
                Totale = await _repo.CountAsync(),
                Inviate = await _repo.CountByStatoAsync("Inviata"),
                Colloqui = await _repo.CountByStatoAsync("Colloquio"),
                Rifiutate = await _repo.CountByStatoAsync("Rifiutata"),
                PerStato = perStato
            };
        }
    }
}