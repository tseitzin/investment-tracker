# Investment Tracker

Investment Tracker is a full-stack portfolio management application for tracking stocks and cryptocurrencies. Users can manage watchlists, record buy/sell transactions, view real-time market data from Polygon.io and the Coinbase API, and explore historical price charts with selectable time ranges. A company research page provides news, details, and related companies for any listed stock. Market movers (top gainers and losers) are surfaced on the dashboard. An admin panel provides user management and an audit log viewer. Authentication uses JWT Bearer tokens with a 15-minute inactivity auto-logout enforced entirely on the client.

---

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Environment Variables](#environment-variables)
- [Running](#running)
- [Testing](#testing)
- [Project Structure](#project-structure)
- [Build](#build)
- [Deployment](#deployment)
- [Skills](#skills)

---

## Features

- Stock and cryptocurrency watchlist management
- Portfolio position tracking — buy/sell with full transaction history
- Real-time market data via Polygon.io (stocks) and Coinbase API (crypto)
- Historical price charts with selectable time ranges (1W, 1M, 3M, 6M, 1Y)
- Company research: details, news feed, and related companies
- Market movers: top gainers and losers
- Admin panel: user management and audit log viewer
- JWT-based authentication with 15-minute inactivity auto-logout

---

## Prerequisites

| Requirement | Version |
|---|---|
| Node.js | 22.3.0 |
| npm | 10.9.0 |
| .NET SDK | 8.0 |
| PostgreSQL | 14+ (local instance) |
| Polygon.io API key | Free tier works |
| Coinbase API key + secret | Coinbase Developer Platform |
| SendGrid API key | For password reset emails |

---

## Installation

```bash
# 1. Clone the repository
git clone <repo-url>
cd investment-tracker

# 2. Install frontend dependencies
npm install

# 3. Configure frontend environment
cp .env.example .env
# Edit .env and fill in VITE_POLYGON_API_KEY and VITE_API_URL

# 4. Configure backend settings
cp api/appsettings.Development.template.json api/appsettings.Development.json
# Edit appsettings.Development.json with your database connection string,
# JWT settings, and API keys

# 5. Restore .NET packages
cd api
dotnet restore

# 6. Apply database migrations
dotnet ef database update
```

---

## Environment Variables

### Frontend (`.env`)

| Variable | Required | Default | Description |
|---|---|---|---|
| `VITE_POLYGON_API_KEY` | No | — | Polygon.io API key. In production this value is served by the backend `SecretsController`; this variable is only used as a Vite build-time definition. |
| `VITE_API_URL` | No | `http://localhost:5000` | Backend URL used by the Vite dev server proxy. |

### Backend (`appsettings.Development.json` or environment variables)

| Variable | Required | Default | Description |
|---|---|---|---|
| `ConnectionStrings__DefaultConnection` | Yes | — | PostgreSQL connection string for local development. |
| `Jwt__Key` | Yes | — | HS256 signing secret. Must be 32+ characters. |
| `Jwt__Issuer` | Yes | — | JWT issuer claim (e.g. `http://localhost:5000`). |
| `Jwt__Audience` | Yes | — | JWT audience claim (e.g. `http://localhost:5173`). |
| `PolygonApiKey` | Yes | — | Polygon.io API key served to the browser via `/api/secrets`. |
| `CoinbaseApiKey` | Yes | — | Coinbase API key served to the browser via `/api/secrets`. |
| `CoinbaseApiSecret` | Yes | — | Coinbase API secret served to the browser via `/api/secrets`. |
| `SendGrid__ApiKey` | Yes | — | SendGrid API key for password reset emails. |
| `SendGrid__FromEmail` | Yes | — | Verified sender email address for SendGrid. |

### Heroku Config Vars (production)

| Variable | Description |
|---|---|
| `DATABASE_URL` | Auto-set by the Heroku Postgres add-on. Parsed from `postgres://` URI format at startup. |
| `JWT__KEY` | 32+ character secret key. |
| `JWT__ISSUER` | Heroku app URL (e.g. `https://investment-tracker-eee77bc323da.herokuapp.com`). |
| `JWT__AUDIENCE` | Heroku app URL. |
| `VITE_POLYGON_API_KEY` | Polygon.io API key (used during the Docker frontend build stage). |
| `COINBASEAPIKEY` | Coinbase API key. |
| `COINBASEAPISECRET` | Coinbase API secret. |
| `SENDGRID__APIKEY` | SendGrid API key. |
| `SENDGRID__FROMEMAIL` | Verified sender email. |
| `ASPNETCORE_ENVIRONMENT` | Set to `Production`. |

---

## Running

Development requires two terminals running simultaneously.

**Terminal 1 — Backend (ASP.NET Core on port 5000)**

```bash
cd api
dotnet run
```

Swagger UI is available at `http://localhost:5000/swagger` in Development mode.

**Terminal 2 — Frontend (Vite dev server on port 5173)**

```bash
npm run dev
```

The Vite dev server proxies all `/api` requests to `http://localhost:5000`. Open `http://localhost:5173` in a browser.

---

## Testing

```bash
# Run tests in watch mode (interactive)
npm run test

# Run tests once (CI)
npm run test:run

# Generate coverage report (output to ./coverage/)
npm run coverage
```

Tests use Vitest 2, Vue Test Utils 2, and happy-dom as the DOM environment. The test setup file is located at `src/tests/setup.ts`.

---

## Project Structure

```
investment-tracker/
├── src/                          # Vue 3 frontend source
│   ├── api/
│   │   └── axios.ts              # Axios instance, request/response interceptors
│   ├── components/               # 27 reusable UI components
│   │   ├── HistoricalPriceChart.vue
│   │   ├── StockPriceChart.vue
│   │   ├── Navbar.vue
│   │   ├── ConfirmationModal.vue
│   │   ├── LoadingSpinner.vue
│   │   └── ...
│   ├── composables/              # 3 Vue composables
│   │   ├── useCollapsibleSection.ts
│   │   ├── useLogger.ts
│   │   └── useStockRemoval.ts
│   ├── router/
│   │   └── index.ts              # Vue Router with auth + admin guards
│   ├── services/                 # 13 service modules (API integrations)
│   │   ├── authService.ts
│   │   ├── coinbaseService.ts
│   │   ├── polygonService.ts
│   │   ├── secretService.ts      # Fetches API keys from backend
│   │   ├── tokenService.ts       # sessionStorage token management
│   │   └── ...
│   ├── stores/                   # 4 Pinia stores
│   │   ├── auth.ts               # Authentication state
│   │   ├── search.ts
│   │   ├── searchHistory.ts      # Stock search history (localStorage)
│   │   └── cryptoSearchHistory.ts # Crypto search history (localStorage)
│   ├── tests/
│   │   ├── Home.spec.ts
│   │   ├── Landing.spec.ts
│   │   └── setup.ts
│   ├── types/                    # TypeScript type definitions
│   ├── utils/                    # 14 utility modules
│   │   ├── activityMonitor.ts    # 15-minute inactivity auto-logout
│   │   ├── chartConfig.ts
│   │   ├── chartPlugins.ts
│   │   ├── chartScales.ts
│   │   ├── chartTypes.ts
│   │   ├── volumeChartConfig.ts
│   │   ├── securityUtils.ts
│   │   └── ...
│   └── views/                    # 19 page components
│       ├── Landing.vue           # Post-login dashboard
│       ├── SearchArea.vue        # Stock search
│       ├── CryptoSearch.vue      # Crypto search
│       ├── StockDashboard.vue    # Stock watchlist
│       ├── CryptoDashboard.vue   # Crypto watchlist
│       ├── ResearchStock.vue     # Company research (/research/:symbol)
│       ├── PortfolioSummary.vue
│       ├── TransactionHistory.vue
│       ├── Users.vue             # Admin: user management
│       ├── AuditLogs.vue         # Admin: audit log viewer
│       └── ...
├── api/                          # ASP.NET Core 8 backend
│   ├── Controllers/              # 14 REST API controllers
│   │   ├── AuthController.cs
│   │   ├── SecretsController.cs  # Serves API keys to authenticated clients
│   │   ├── PortfolioController.cs
│   │   ├── CryptoPortfolioController.cs
│   │   ├── SavedStocksController.cs
│   │   ├── SavedCryptosController.cs
│   │   ├── TransactionController.cs
│   │   ├── StockDataController.cs
│   │   ├── MarketMoversController.cs
│   │   ├── UsersController.cs
│   │   ├── AuditController.cs
│   │   ├── LogsController.cs
│   │   ├── FavoritesController.cs
│   │   └── FallbackController.cs # Returns index.html for SPA routing
│   ├── Data/
│   │   └── AppDbContext.cs       # EF Core DbContext
│   ├── Middleware/
│   │   └── SecurityHeadersMiddleware.cs # CSP, HSTS, X-Frame-Options, etc.
│   ├── Migrations/               # EF Core database migrations
│   ├── Models/                   # C# entity and request/response models
│   ├── Services/                 # Backend service layer
│   │   ├── AuthService.cs
│   │   ├── EmailService.cs       # SendGrid integration
│   │   ├── KeyVaultService.cs    # Reads secrets from environment/config
│   │   └── StockSeederService.cs # Seeds stock reference data on startup
│   └── api.csproj
├── Dockerfile                    # 4-stage multi-stage production build
├── heroku.yml                    # Heroku Docker container stack config
├── vite.config.ts                # Vite build config and dev proxy
├── tailwind.config.js
├── tsconfig.json
└── package.json
```

---

## Build

```bash
# Compile Vue SPA to api/wwwroot (served as static files by ASP.NET Core)
npm run build
```

The build output directory is `./api/wwwroot`. The ASP.NET Core backend serves these files as static assets, and `FallbackController` returns `index.html` for all non-API GET requests to support client-side routing.

---

## Deployment

The application deploys to Heroku using a Docker container stack. Deployment is a single Git push.

### First-time setup

```bash
# Log in to Heroku container registry
heroku login
heroku container:login

# Add the Heroku remote (if not already added)
heroku git:remote -a investment-tracker-eee77bc323da

# Set all required config vars (see Environment Variables table above)
heroku config:set JWT__KEY="your-32-char-secret" --app investment-tracker-eee77bc323da
# ... repeat for all required vars
```

### Deploying

```bash
git push heroku main
```

Heroku reads `heroku.yml`, builds the `Dockerfile`, and deploys the resulting image. On startup, the application automatically:

1. Parses the `DATABASE_URL` environment variable from Heroku Postgres URI format to a Npgsql connection string.
2. Runs all pending EF Core migrations via `context.Database.Migrate()`.
3. Seeds initial stock reference data via `StockSeederService`.

### Dockerfile build stages

| Stage | Base image | What it does |
|---|---|---|
| `frontend-build` | `node:18` | Runs `npm run build`, outputs Vue SPA to `api/wwwroot` |
| `build` | `mcr.microsoft.com/dotnet/sdk:8.0` | Restores packages and builds the .NET project |
| `publish` | (from `build`) | Runs `dotnet publish -c Release` |
| `final` | `mcr.microsoft.com/dotnet/aspnet:8.0` | Copies published backend and `wwwroot` into the runtime image |

The final image contains only the ASP.NET Core runtime — no Node.js or .NET SDK.

---

## Skills

| Category | Technology |
|---|---|
| Primary Languages | TypeScript, C# |
| Frontend Framework | Vue 3 (Composition API, `<script setup>`) |
| Backend Framework | ASP.NET Core 8 |
| Database | PostgreSQL via EF Core 8 + Npgsql |
| State Management | Pinia 2 |
| Styling | Tailwind CSS 3 |
| Charts | Chart.js 4 + vue-chartjs 5 |
| HTTP Client | Axios 1.6 |
| Build Tool | Vite 5 |
| Infrastructure | Heroku (Docker container stack) |
| CI/CD | Heroku Git push deploy (`git push heroku main`) |
| Frontend Testing | Vitest 2, Vue Test Utils 2, happy-dom |
| External APIs | Polygon.io (stocks), Coinbase API (crypto), SendGrid (email) |
