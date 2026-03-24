/// <summary>
/// Gestisce i grafici Highcharts della dashboard.
/// Chiama le API JSON del Controller e renderizza i grafici donut e area.
/// </summary>

// ==================== GRAFICO DONUT: DISTRIBUZIONE PER STATO ====================
function caricaGraficoStato() {
    fetch('/Candidature/ApiStats')
        .then(function (response) {
            if (!response.ok) throw new Error('Errore API');
            return response.json();
        })
        .then(function (data) {
            // Se non ci sono dati, mostra un messaggio nel container
            if (!data.perStato || data.perStato.length === 0) {
                document.getElementById('chart-stato').innerHTML =
                    '<p class="text-muted text-center mt-5">Nessun dato disponibile</p>';
                return;
            }

            Highcharts.chart('chart-stato', {
                chart: {
                    type: 'pie',
                    backgroundColor: 'transparent'
                },
                title: { text: null },
                plotOptions: {
                    pie: {
                        innerSize: '55%',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.y}',
                            style: { fontSize: '12px' }
                        }
                    }
                },
                // Colori coerenti con i badge Bootstrap usati nella tabella
                colors: ['#0dcaf0', '#6c757d', '#ffc107', '#dc3545', '#198754'],
                series: [{
                    name: 'Candidature',
                    data: data.perStato
                }],
                credits: { enabled: false }
            });
        })
        .catch(function (error) {
            console.error('Errore caricamento grafico stato:', error);
            document.getElementById('chart-stato').innerHTML =
                '<p class="text-danger text-center mt-5">Errore nel caricamento del grafico</p>';
        });
}

// ==================== GRAFICO AREA: CANDIDATURE NEL TEMPO ====================
function caricaGraficoTempo() {
    fetch('/Candidature/ApiTimeline')
        .then(function (response) {
            if (!response.ok) throw new Error('Errore API');
            return response.json();
        })
        .then(function (data) {
            if (!data.date || data.date.length === 0) {
                document.getElementById('chart-tempo').innerHTML =
                    '<p class="text-muted text-center mt-5">Nessun dato disponibile</p>';
                return;
            }

            Highcharts.chart('chart-tempo', {
                chart: {
                    type: 'area',
                    backgroundColor: 'transparent'
                },
                title: { text: null },
                xAxis: {
                    categories: data.date,
                    labels: { style: { fontSize: '11px' } }
                },
                yAxis: {
                    title: { text: 'Candidature' },
                    allowDecimals: false
                },
                series: [{
                    name: 'Candidature inviate',
                    data: data.conteggi,
                    color: '#4e73df',
                    fillColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, 'rgba(78, 115, 223, 0.3)'],
                            [1, 'rgba(78, 115, 223, 0.02)']
                        ]
                    }
                }],
                credits: { enabled: false },
                legend: { enabled: false }
            });
        })
        .catch(function (error) {
            console.error('Errore caricamento grafico tempo:', error);
            document.getElementById('chart-tempo').innerHTML =
                '<p class="text-danger text-center mt-5">Errore nel caricamento del grafico</p>';
        });
}