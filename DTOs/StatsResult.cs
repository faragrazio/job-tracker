namespace JobTracker.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) che contiene i risultati delle statistiche aggregate.
    /// Usato da GetStatsUseCase per passare i dati al Controller senza esporre il Repository.
    /// </summary>
    public class StatsResult
    {
        public int Totale { get; set; }
        public int Inviate { get; set; }
        public int Colloqui { get; set; }
        public int Rifiutate { get; set; }
        public List<object> PerStato { get; set; } = new();
    }
}