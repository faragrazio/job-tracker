/// <summary>
/// Gestisce le operazioni AJAX sulla tabella candidature.
/// Usa un modal Bootstrap per la conferma di eliminazione al posto del confirm() nativo.
/// </summary>

// ID della candidatura da eliminare (impostato quando si apre il modal)
var candidaturaIdDaEliminare = null;

// ==================== APRI MODAL DI CONFERMA ====================
function eliminaCandidatura(id, azienda) {
    // Salva l'ID e mostra il nome dell'azienda nel modal
    candidaturaIdDaEliminare = id;
    document.getElementById('nomeAziendaModal').textContent = azienda;

    // Apre il modal Bootstrap
    var modal = new bootstrap.Modal(document.getElementById('modalElimina'));
    modal.show();
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

    // Gestisce il click sul pulsante "Elimina" nel modal
    var btnConferma = document.getElementById('btnConfermaElimina');
    if (btnConferma) {
        btnConferma.addEventListener('click', function () {
            if (!candidaturaIdDaEliminare) return;

            // Prende il token CSRF dalla pagina
            var token = document.querySelector('input[name="__RequestVerificationToken"]');
            var tokenValue = token ? token.value : '';

            fetch('/Candidature/Delete/' + candidaturaIdDaEliminare, {
                method: 'POST',
                headers: {
                    'RequestVerificationToken': tokenValue
                }
            })
            .then(function (response) {
                if (response.ok || response.redirected) {
                    // Chiude il modal
                    var modal = bootstrap.Modal.getInstance(document.getElementById('modalElimina'));
                    if (modal) modal.hide();

                    // Animazione: la riga sparisce con fade out (solo nella Index)
                    var riga = document.getElementById('row-' + candidaturaIdDaEliminare);
                    if (riga) {
                        riga.style.transition = 'opacity 0.3s ease';
                        riga.style.opacity = '0';
                        setTimeout(function () {
                            riga.remove();
                            window.location.href = '/Candidature';
                        }, 300);
                    } else {
                        // Se non c'è la riga (es. siamo nella pagina Details), redirect diretto
                        window.location.href = '/Candidature';
                    }
                } else {
                    alert('Errore durante l\'eliminazione. Riprova.');
                }
            })
            .catch(function () {
                alert('Errore di rete. Controlla la connessione e riprova.');
            });
        });
    }
});