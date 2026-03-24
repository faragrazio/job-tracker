using System.ComponentModel.DataAnnotations;

namespace JobTracker.Models
{
    public class Candidatura
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "L'azienda è obbligatoria")]
        [Display(Name = "Azienda")]
        public string Azienda { get; set; } = string.Empty;

        [Required(ErrorMessage = "Il ruolo è obbligatorio")]
        [Display(Name = "Ruolo")]
        public string Ruolo { get; set; } = string.Empty;

        [Display(Name = "Città")]
        public string? Citta { get; set; }

        [Display(Name = "Modalità")]
        public string? Modalita { get; set; }

        [Display(Name = "Fonte")]
        public string? Fonte { get; set; }

        [Required]
        [Display(Name = "Data Candidatura")]
        [DataType(DataType.Date)]
        public DateTime DataCandidatura { get; set; } = DateTime.Today;

        [Display(Name = "Stato")]
        public string Stato { get; set; } = "Inviata";

        [Display(Name = "RAL Indicata")]
        public string? RALIndicata { get; set; }

        [Display(Name = "Stack Richiesto")]
        public string? StackRichiesto { get; set; }

        [Display(Name = "Note")]
        public string? Note { get; set; }

        [Display(Name = "Link Offerta")]
        public string? LinkOfferta { get; set; }

        [Display(Name = "Data Risposta")]
        [DataType(DataType.Date)]
        public DateTime? DataRisposta { get; set; }

        [Display(Name = "Data Colloquio")]
        [DataType(DataType.Date)]
        public DateTime? DataColloquio { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}