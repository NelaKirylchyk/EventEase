# EventEase

A lightweight event discovery and registration app built with Blazor WebAssembly (.NET8). Browse events, view details, and register with built‑in validation. The app demonstrates idiomatic Blazor patterns including dependency injection, state management, and forms, and can be deployed as a static site with optional offline support.

- Tech: Blazor WebAssembly (.NET8), C#12
- Project: Standalone WASM app (no server API required)
- PWA: Includes a publish service worker for offline caching

## Features
- Events list and details pages
- Registration form with validation and user feedback
- Session persistence for draft registration data
- Simple in‑memory attendance tracking
- Responsive layout with navigation menu
- Optional offline support when published

## Getting started
### Prerequisites
- .NET SDK8.0 or later
- Any editor (Visual Studio, VS Code, or JetBrains Rider)

### Build and run (CLI)
1. Restore packages
 - `dotnet restore`
2. Build
 - `dotnet build`
3. Run the development server
 - `dotnet run`
4. Open the URL shown in the console (e.g., `http://localhost:5xxx`)

### Open in Visual Studio
- Open the `EventEase/EventEase.csproj` and press F5.

## Project structure (high‑level)
- `EventEase/Program.cs` — App bootstrapping and dependency injection
- `EventEase/App.razor` — Root component and router
- `EventEase/Layout/MainLayout.razor`, `EventEase/Layout/NavMenu.razor` — Shell and navigation
- `EventEase/Pages/Events.razor` — Events list
- `EventEase/Pages/EventDetails.razor` — Event detail view
- `EventEase/Pages/Register.razor` — Registration form with validation and session persistence
- `EventEase/Services/` — App services and state management
 - `ISessionState`, `SessionState` — Simple browser session storage
 - `IEventsState`, `EventsState` — Events data store
 - `IAttendanceTracker`, `AttendanceTracker` — In‑memory attendance
- `EventEase/Models/` — Domain models (`EventItem`, `AttendanceRecord`)
- `EventEase/wwwroot/` — Static assets and PWA service worker files

Note: Some generated `.ide.g.cs` files may appear during development. Source components are in the corresponding `.razor` files.

## Architecture and key concepts
- Dependency Injection
 - Registered in `Program.cs`:
 - `ISessionState` as scoped
 - `IEventsState` and `IAttendanceTracker` as singletons
- Routing
 - Defined using `@page` directives in components, e.g., `@page "/register/{Name}"`
- Forms & Validation
 - Uses `EditForm`, `DataAnnotationsValidator`, and `ValidationMessage` for client‑side validation
- State & Persistence
 - Minimal session state (name/email) stored via `ISessionState` implementation
- PWA / Offline (publish only)
 - `wwwroot/service-worker.published.js` caches app assets after publish. See Blazor offline considerations to understand trade‑offs.

## Notable components
- `Pages/Register.razor`
 - Prepopulates form fields from session storage per event
 - On submit: persists minimal session data and tracks attendance
 - Displays validation and error/success messages
- `Layout/MainLayout.razor`
 - Provides the application shell, navigation, and content area

## Configuration
- Base address: The app uses the host environment base address for `HttpClient`.
- Session keys: Registration state is namespaced by event (e.g., `reg:{event}:full`, `reg:{event}:email`).

## Deployment
Blazor WASM apps can be hosted as static sites.

- GitHub Pages (static)
1. Publish: `dotnet publish -c Release`
2. Use the contents of `EventEase/bin/Release/net8.0/publish/wwwroot` as the site root
3. Configure Pages to serve from that folder
4. If hosting under a sub‑path, ensure `base` path and service worker scope are correctly configured

- Azure Static Web Apps
 - Create a Static Web App resource and point it to this repo
 - App location: `EventEase`
 - Output location: `EventEase/bin/Release/net8.0/publish/wwwroot`

## Troubleshooting
- Blank page after deployment
 - Check `<base href="/">` in `index.html` and adjust for sub‑path hosting
- Stale content after update
 - Service worker may cache assets; perform a hard refresh or bump the app version and redeploy
- Validation not triggering
 - Ensure `DataAnnotationsValidator` and `ValidationMessage` are present in the `EditForm`