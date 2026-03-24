/// <summary>
/// Gestisce le operazioni AJAX sulla tabella candidature.
/// Funzioni pure chiamate da onclick — nessuna logica nel DOM, solo qui.
/// </summary>

// ==================== ELIMINA CANDIDATURA VIA AJAX ====================
function eliminaCandidatura(id, azienda) {
    if (!confirm('Sei sicuro di voler eliminare la candidatura per ' + azienda + '?')) {
        return;
    }

    // Prende il token CSRF dalla pagina per protezione anti-manomissione
    var token = document.querySelector('input[name="__RequestVerificationToken"]');
    var tokenValue = token ? token.value : '';

    fetch('/Candidature/Delete/' + id, {
        method: 'POST',
        headers: {
            'RequestVerificationToken': tokenValue
        }
    })
    .then(function (response) {
        if (response.ok || response.redirected) {
            // Animazione: la riga sparisce con fade out
            var riga = document.getElementById('row-' + id);
            if (riga) {
                riga.style.transition = 'opacity 0.3s ease';
                riga.style.opacity = '0';
                setTimeout(function () {
                    riga.remove();
                    // Ricarica la pagina per aggiornare contatori e grafici
                    location.reload();
                }, 300);
            }
        } else {
            alert('Errore durante l\'eliminazione. Riprova.');
        }
    })
    .catch(function () {
        alert('Errore di rete. Controlla la connessione e riprova.');
    });
}

// ==================== INIZIALIZZAZIONE PAGINA ====================
document.addEventListener('DOMContentLoaded', function () {
    // Carica i grafici Highcharts se i container esistono nella pagina
    if (document.getElementById('chart-stato')) {
        caricaGraficoStato();
    }
    if (document.getElementById('chart-tempo')) {
        caricaGraficoTempo();
    }
});