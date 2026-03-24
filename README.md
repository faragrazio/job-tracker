# 💼 JobTracker

**Dashboard per il tracking delle candidature di lavoro** — applicazione web full stack sviluppata con ASP.NET MVC, MySQL e Bootstrap 5.

![ASP.NET](https://img.shields.io/badge/ASP.NET_MVC-.NET_8-512BD4?style=flat&logo=dotnet)
![MySQL](https://img.shields.io/badge/Database-MySQL_8-4479A1?style=flat&logo=mysql&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Frontend-Bootstrap_5-7952B3?style=flat&logo=bootstrap&logoColor=white)
![Highcharts](https://img.shields.io/badge/Charts-Highcharts-058DC7?style=flat)
![License](https://img.shields.io/badge/License-MIT-green?style=flat)

---

## 📸 Screenshot

### Dashboard principale
> Card riassuntive con contatori, grafici Highcharts (donut per stato + area nel tempo), filtri e tabella interattiva.

### Form di creazione
> Form responsive a 2 colonne con validazione, select per modalità/fonte/stato e campi data.

### Dettaglio candidatura
> Vista in sola lettura con badge stato colorato, link cliccabile e pulsanti azione rapida.

---

## ✨ Funzionalità

- **Dashboard** con 4 card riassuntive (totali, inviate, colloqui, rifiutate)
- **Grafici Highcharts** — donut distribuzione per stato + area candidature nel tempo
- **CRUD completo** — crea, visualizza, modifica, elimina candidature
- **Filtri** per stato e ricerca testuale per azienda/ruolo
- **Eliminazione AJAX** con animazione fade-out (senza page reload)
- **Design responsive** — ottimizzato per desktop e mobile
- **Validazione** server-side con Data Annotations

---

## 🏗️ Architettura

Il progetto segue la **Clean Architecture** con separazione netta delle responsabilità:
```
Request HTTP
    │
    ▼
Controller          → routing e orchestrazione
    │
    ▼
UseCase             → logica di business
    │
    ▼
Repository          → accesso ai dati (via interfaccia)
    │
    ▼
AppDbContext         → Entity Framework Core → MySQL
```

### Struttura del progetto
```
JobTracker/
├── Controllers/
│   └── CandidatureController.cs      # Routing + API JSON per Highcharts
├── Models/
│   ├── Candidatura.cs                 # Entity principale
│   └── AppDbContext.cs                # DbContext Entity Framework
├── DTOs/
│   ├── StatsResult.cs                 # DTO statistiche dashboard
│   └── TimelineResult.cs             # DTO dati grafico timeline
├── Repositories/
│   ├── ICandidaturaRepository.cs      # Interfaccia (Dependency Inversion)
│   └── CandidaturaRepository.cs       # Implementazione MySQL
├── UseCases/
│   ├── GetAllCandidatureUseCase.cs    # Lista con filtri
│   ├── GetCandidaturaByIdUseCase.cs   # Dettaglio singolo
│   ├── CreateCandidaturaUseCase.cs    # Creazione + regole business
│   ├── UpdateCandidaturaUseCase.cs    # Aggiornamento con verifica esistenza
│   ├── DeleteCandidaturaUseCase.cs    # Eliminazione
│   ├── GetStatsUseCase.cs            # Contatori per dashboard
│   └── GetTimelineUseCase.cs          # Dati raggruppati per grafico
├── Views/
│   ├── Candidature/
│   │   ├── Index.cshtml               # Dashboard + tabella + grafici
│   │   ├── Create.cshtml              # Form creazione
│   │   ├── Edit.cshtml                # Form modifica
│   │   └── Details.cshtml             # Vista dettaglio
│   └── Shared/
│       └── _Layout.cshtml             # Layout con navbar e footer
├── wwwroot/
│   ├── css/
│   │   └── site.css                   # Tema SaaS con CSS variables
│   └── js/
│       ├── charts.js                  # Grafici Highcharts (donut + area)
│       └── candidature.js             # AJAX delete + inizializzazione
├── Program.cs                         # DI container + configurazione
├── appsettings.Example.json           # Template configurazione (senza password)
└── README.md
```

---

## 🛠️ Stack tecnologico

| Layer | Tecnologia |
|-------|-----------|
| **Backend** | C#, ASP.NET MVC, .NET 8 |
| **ORM** | Entity Framework Core 8 + Pomelo MySQL |
| **Database** | MySQL 8.0 |
| **Frontend** | HTML5, CSS3, JavaScript, Bootstrap 5 |
| **Grafici** | Highcharts |
| **Icone** | Bootstrap Icons |
| **Font** | DM Sans (Google Fonts) |
| **Version Control** | Git + GitHub |

---

## 🚀 Installazione e setup

### Prerequisiti

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL 8.0](https://dev.mysql.com/downloads/)
- [Git](https://git-scm.com/)

### 1. Clona il repository
```bash
git clone https://github.com/faragrazio/job-tracker.git
cd job-tracker
```

### 2. Crea il database
```sql
CREATE DATABASE jobtracker;
USE jobtracker;

CREATE TABLE candidature (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Azienda VARCHAR(100) NOT NULL,
    Ruolo VARCHAR(100) NOT NULL,
    Citta VARCHAR(100),
    Modalita VARCHAR(50),
    Fonte VARCHAR(50),
    DataCandidatura DATE NOT NULL,
    Stato VARCHAR(50) NOT NULL DEFAULT 'Inviata',
    RALIndicata VARCHAR(50),
    StackRichiesto VARCHAR(255),
    Note TEXT,
    LinkOfferta VARCHAR(500),
    DataRisposta DATE,
    DataColloquio DATE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);
```

### 3. Configura la connessione

Copia il file di esempio e inserisci la tua password MySQL:
```bash
cp appsettings.Example.json appsettings.json
```

Modifica `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=jobtracker;User=root;Password=LA_TUA_PASSWORD;"
  }
}
```

### 4. Avvia l'applicazione
```bash
dotnet run
```

Apri il browser su `http://localhost:5017`

---

## 📐 Pattern e principi applicati

- **Clean Architecture** — separazione Controller / UseCase / Repository
- **Repository Pattern** — accesso ai dati tramite interfaccia
- **Dependency Injection** — tutte le dipendenze registrate in `Program.cs`
- **SOLID Principles** — Single Responsibility, Open/Closed, Dependency Inversion
- **DTO Pattern** — oggetti dedicati per il trasferimento dati tra layer
- **Async/Await** — operazioni database non bloccanti

---

## 👤 Autore

**Graziano Faraone** — Full Stack Developer

- LinkedIn: [linkedin.com/in/graziano-faraone-26a071218](https://www.linkedin.com/in/graziano-faraone-26a071218)
- GitHub: [github.com/faragrazio](https://github.com/faragrazio)
- Email: grazianofaraone@gmail.com

---

## 📄 Licenza

Questo progetto è distribuito sotto licenza MIT. Vedi il file `LICENSE` per i dettagli.
