using JobTracker.DTOs;
using JobTracker.Repositories;

namespace JobTracker.UseCases
{
    /// <summary>
    /// Genera i dati per il grafico Highcharts "Candidature nel tempo".
    /// Raggruppa le candidature per data e restituisce date + conteggi per il grafico area.
    /// </summary>
    public class GetTimelineUseCase
    {
        private readonly ICandidaturaRepository _repo;

        public GetTimelineUseCase(ICandidaturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<TimelineResult> ExecuteAsync()
        {
            var candidature = await _repo.GetAllAsync(null, null);

            var grouped = candidature
                .GroupBy(c => c.DataCandidatura.ToString("dd/MM"))
                .OrderBy(g => g.First().DataCandidatura)
                .Select(g => new { data = g.Key, count = g.Count() })
                .ToList();

            return new TimelineResult
            {
                Date = grouped.Select(g => g.data).ToList(),
                Conteggi = grouped.Select(g => g.count).ToList()
            };
        }
    }
}