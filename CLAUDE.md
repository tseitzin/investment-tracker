# Investment Tracker — Project CLAUDE.md

## Commands

### Frontend

```bash
# Install dependencies
npm install

# Development server (port 5173, proxies /api → localhost:5000)
npm run dev

# Build for production (outputs to api/wwwroot)
npm run build

# Type-check without building
npx vue-tsc --noEmit

# Run tests in watch mode
npm test

# Run tests once (CI)
npm run test:run

# Generate coverage report (output: ./coverage/)
npm run coverage
```

### Backend

```bash
# Run backend (port 5000)
cd api && dotnet run

# Run backend with auto-reload on file changes
cd api && dotnet watch run

# Add a new EF Core migration
cd api && dotnet ef migrations add <MigrationName>

# Apply pending migrations
cd api && dotnet ef database update

# Revert to a previous migration
cd api && dotnet ef database update <PreviousMigrationName>

# List all migrations
cd api && dotnet ef migrations list
```

### Heroku

```bash
# View live logs
heroku logs --tail -a investment-tracker-eee77bc323da

# Set a config var
heroku config:set KEY=value -a investment-tracker-eee77bc323da

# View all config vars
heroku config -a investment-tracker-eee77bc323da

# Deploy
git push heroku main

# Open the app
heroku open -a investment-tracker-eee77bc323da
```

---

## Local Setup Checklist

A new developer needs all of the following before the app will run:

- [ ] Node.js 22.3.0 and npm 10.9.0 (use `nvm` or `volta` to pin version)
- [ ] .NET 8 SDK (`dotnet --version` should show `8.x.x`)
- [ ] PostgreSQL instance reachable at the address in `appsettings.Development.json`
- [ ] `api/appsettings.Development.json` copied from `api/appsettings.Development.template.json` and filled in with real values
- [ ] `.env` copied from `.env.example` and filled in (only `VITE_API_URL` and `VITE_POLYGON_API_KEY` are needed locally)
- [ ] `cd api && dotnet ef database update` run at least once to create the schema

---

## Conventions

### Vue / TypeScript

- All components use `<script setup>` Composition API syntax — no Options API
- Services are **class-based singletons** exported at module level:
  ```ts
  class PolygonService { ... }
  export const polygonService = new PolygonService()
  ```
- All shared types live in `src/types/` and are imported with `import type`
- The `@` path alias maps to `src/` (configured in `vite.config.ts` and `vitest.config.ts`)
- Styling is **Tailwind CSS only** — do not add scoped `<style>` blocks unless absolutely unavoidable
- Toast notifications: use **`vue-toastification` only** — do not add `vue3-toastify` usage (duplicate package, present but unused)
- Session token storage: use **`TokenService`** (reads/writes `sessionStorage`) — do not use `localStorage` directly for auth tokens

### C# / .NET

- PostgreSQL table names follow **snake_case** convention (configured in `AppDbContext.OnModelCreating`)
- Controllers use `[Authorize]` at the class level; public endpoints are marked `[AllowAnonymous]`
- Services are registered with **Scoped** lifetime (not Singleton) to avoid issues with EF Core `DbContext` threading
- Secrets are read via `IKeyVaultService` — never read from `IConfiguration` directly in controllers
- Use `context.Database.Migrate()` in `Program.cs` for automatic migration on startup (already in place)

### Git

- Branch names: use descriptive kebab-case names (e.g., `fix-auth-interceptor`, `add-portfolio-chart`)
- Never commit real credentials to any `appsettings` file
- `api/appsettings.Development.json` is in `.gitignore` — do not commit it
- `api/appsettings.Production.json` is in `.gitignore` — do not commit it

---

## Architecture Constraints

**Do not violate these patterns without team discussion:**

1. **No new globally registered components** — Do not add to the `app.component()` calls in `main.ts`. Import components locally where they are used.

2. **No API secrets in the frontend** — All credentials must be fetched from the backend `/api/secrets` endpoint (or, preferably, moved entirely server-side so the frontend never sees them).

3. **Token storage via `TokenService`** — `src/services/tokenService.ts` is the canonical place to read and write auth tokens (`sessionStorage`). Do not bypass this with direct `localStorage` or `sessionStorage` calls in components.

4. **No `Access-Control-*` headers in axios requests** — These are server response headers. Setting them on client requests is incorrect and has no effect. `src/api/axios.ts` was corrected; do not reintroduce this pattern.

5. **Rate limit awareness** — The backend applies a global rate limit of 100 requests/minute per user or IP (configured in `Program.cs`). Do not add polling loops or auto-refresh intervals that could exceed this on a single client.

---

## Known Issues / Tech Debt

| Issue | Location | Severity | Notes |
|-------|----------|----------|-------|
| API secrets sent to browser | `api/Controllers/SecretsController.cs`, `src/services/coinbaseService.ts`, `src/services/polygonService.ts` | Medium | Polygon + Coinbase keys/secrets are fetched by the browser and cached in service singletons. External API calls should be proxied server-side. |
| Duplicate toast package | `package.json` | Low | `vue3-toastify` is installed but unused. Remove it to reduce bundle size. |
| 5 globally registered components | `src/main.ts` | Low | Bypasses tree-shaking. Convert to local imports. |
| Redundant search history stores | `src/stores/searchHistory.ts`, `src/stores/cryptoSearchHistory.ts` | Low | Structurally identical stores — should be one generic composable. |
| Low test coverage | `src/tests/` | Medium | Only 2 spec files cover a large application. |
| `encryptData` misnamed | `src/utils/securityUtils.ts` | Low | Function performs base64 encoding, not encryption. Name is misleading. |
| "Stock Navigator" naming | test files, Swagger | Low | Legacy name — canonical name is "Investment Tracker". Clean up during test/doc updates. |

---

## Deployment Workflow

```
1. Ensure all changes are committed and pushed to the main branch
2. Verify no credentials are committed (check appsettings files)
3. git push heroku main
4. Heroku builds the Docker image (4-stage: frontend build → .NET build → publish → runtime)
5. The new dyno starts; EF Core auto-applies pending migrations on startup
6. StockSeederService runs and seeds any missing reference data
7. Monitor: heroku logs --tail -a investment-tracker-eee77bc323da
```

The Heroku app URL is: `https://investment-tracker-eee77bc323da.herokuapp.com`
