namespace JobTracker.DTOs
{
    /// <summary>
    /// DTO che contiene i dati raggruppati per data per il grafico timeline Highcharts.
    /// Usato da GetTimelineUseCase per separare la struttura dati dalla logica di business.
    /// </summary>
    public class TimelineResult
    {
        public List<string> Date { get; set; } = new();
        public List<int> Conteggi { get; set; } = new();
    }
}