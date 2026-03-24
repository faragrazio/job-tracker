/// <summary>
/// Gestisce i grafici Highcharts della dashboard.
/// Palette colori allineata alle CSS variables del tema SaaS.
/// </summary>

// Colori coerenti con le classi badge-stato nel CSS
var COLORI_STATO = ['#3b82f6', '#8b5cf6', '#f59e0b', '#ef4444', '#10b981'];

// ==================== GRAFICO DONUT: DISTRIBUZIONE PER STATO ====================
function caricaGraficoStato() {
    fetch('/Candidature/ApiStats')
        .then(function (response) {
            if (!response.ok) throw new Error('Errore API');
            return response.json();
        })
        .then(function (data) {
            if (!data.perStato || data.perStato.length === 0) {
                document.getElementById('chart-stato').innerHTML =
                    '<p style="color: #94a3b8; text-align: center; margin-top: 80px;">Nessun dato disponibile</p>';
                return;
            }

            Highcharts.chart('chart-stato', {
                chart: {
                    type: 'pie',
                    backgroundColor: 'transparent',
                    style: { fontFamily: 'DM Sans, sans-serif' }
                },
                title: { text: null },
                plotOptions: {
                    pie: {
                        innerSize: '60%',
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.y}',
                            style: {
                                fontSize: '11px',
                                fontWeight: '600',
                                color: '#64748b',
                                textOutline: 'none'
                            },
                            distance: 15
                        },
                        states: {
                            hover: {
                                brightness: 0.05,
                                halo: { size: 8 }
                            }
                        }
                    }
                },
                colors: COLORI_STATO,
                series: [{
                    name: 'Candidature',
                    data: data.perStato
                }],
                tooltip: {
                    style: { fontSize: '13px' },
                    backgroundColor: '#1e1e2e',
                    borderColor: 'transparent',
                    borderRadius: 8,
                    style: { color: '#ffffff', fontSize: '12px' }
                },
                credits: { enabled: false }
            });
        })
        .catch(function (error) {
            console.error('Errore caricamento grafico stato:', error);
            document.getElementById('chart-stato').innerHTML =
                '<p style="color: #ef4444; text-align: center; margin-top: 80px;">Errore nel caricamento</p>';
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
                    '<p style="color: #94a3b8; text-align: center; margin-top: 80px;">Nessun dato disponibile</p>';
                return;
            }

            Highcharts.chart('chart-tempo', {
                chart: {
                    type: 'area',
                    backgroundColor: 'transparent',
                    style: { fontFamily: 'DM Sans, sans-serif' }
                },
                title: { text: null },
                xAxis: {
                    categories: data.date,
                    labels: {
                        style: { fontSize: '11px', color: '#94a3b8' }
                    },
                    lineColor: '#e2e8f0',
                    tickColor: 'transparent'
                },
                yAxis: {
                    title: { text: null },
                    allowDecimals: false,
                    gridLineColor: '#f0f2f5',
                    labels: {
                        style: { fontSize: '11px', color: '#94a3b8' }
                    }
                },
                series: [{
                    name: 'Candidature',
                    data: data.conteggi,
                    color: '#6366f1',
                    lineWidth: 2.5,
                    marker: {
                        radius: 5,
                        fillColor: '#6366f1',
                        lineWidth: 2,
                        lineColor: '#ffffff'
                    },
                    fillColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, 'rgba(99, 102, 241, 0.25)'],
                            [1, 'rgba(99, 102, 241, 0.01)']
                        ]
                    }
                }],
                tooltip: {
                    backgroundColor: '#1e1e2e',
                    borderColor: 'transparent',
                    borderRadius: 8,
                    style: { color: '#ffffff', fontSize: '12px' }
                },
                credits: { enabled: false },
                legend: { enabled: false }
            });
        })
        .catch(function (error) {
            console.error('Errore caricamento grafico tempo:', error);
            document.getElementById('chart-tempo').innerHTML =
                '<p style="color: #ef4444; text-align: center; margin-top: 80px;">Errore nel caricamento</p>';
        });
}