# 03 — Architecture

## Backend — Clean Architecture (4 Layers)

### Layer Overview
```
Backend/
├── API/
│   └── Controllers/          ← HTTP endpoints, request/response handling
│
├── Application/
│   ├── DTOs/                 ← Data Transfer Objects (input/output shapes)
│   ├── Mappers/              ← Entity ↔ DTO conversion
│   ├── Services/             ← Orchestration, calls UseCases
│   └── UseCases/             ← Business logic per operation
│
├── Domain/
│   ├── Entities/             ← Core business objects (mirrors DB tables)
│   ├── Exceptions/           ← Domain-specific exceptions
│   └── Interfaces/           ← Repository contracts (implemented by Infrastructure)
│
└── Infrastructure/
    ├── Persistance/          ← Repository implementations, ORM context
    ├── Data/                 ← DB config, migrations, seeds
    └── ExternalServices/     ← Payment gateway, email, external APIs
```

### Dependency Flow (from diagram)
```
Controllers → Services → UseCases
UseCases → DTOs (via Mappers)
UseCases → Interfaces (Domain contracts)
Persistance → Interfaces (implements them)
Persistance → Data (reads/writes)
ExternalServices → Interfaces (also implements some)
Persistance → Exceptions (can throw domain exceptions)
ExternalServices → Interfaces (implements)
```

### Key Rules
- **Domain** has no dependencies on other layers — it's the center
- **Infrastructure** implements `Domain.Interfaces` — dependency inversion
- **Application** depends on Domain, never on Infrastructure directly
- **API** depends only on Application (calls Services/DTOs)
- All arrows are dashed in the diagram → dependency/usage relationships

---

## Frontend — React Feature-Based Package Structure

The frontend is split into two React apps sharing a `Shared` module:

### Public Site App
```
App/
├── Router/                   ← React Router configuration
├── App/                      ← Root component
└── Layout/                   ← Navbar + Footer wrapper

Feature modules (each contains: Components, Hooks, Interfaces, Service):
├── Home/                     ← Hero + About Us + Gallery
├── RoomsAndPrices/           ← Room type cards + rates
└── Booking/                  ← Full reservation flow (3 steps + unavailability)

Shared/                       ← Cross-module resources
├── UI Components/            ← Reusable UI (Button, Card, Modal, etc.)
├── Utilities/                ← Helper functions
├── Assets/                   ← Images, icons, fonts
└── Service/                  ← API client, axios config, base requests
```

### Admin App
```
App/
├── Router/                   ← Admin routing (protected routes)
├── App/                      ← Root admin component
└── Layout/                   ← Admin navbar wrapper

Feature modules (each contains: Components, Hooks, Interfaces, Service):
├── Loggin/                   ← Login page (note: typo in diagram)
├── Home/                     ← Dashboard with monthly stats
├── ModifyPages/              ← Edit page content (Home, About Us, Facilities, How to)
├── Reservations/             ← Manage reservations table + modals
├── RoomAdministration/       ← Manage room types + individual rooms
├── HotelState/               ← Today's room status table + print
├── RoomAvailability/         ← Search availability by date range
├── Adds/                     ← Advertisement management
└── Estadistics/              ← Monthly stats dashboard

Shared/                       ← Same shared module as public site
├── UI Components/
├── Utilities/
├── Assets/
└── Service/
```

### Each Feature Module Pattern
Every feature module (e.g. `Booking/`, `Reservations/`) follows this structure:
```
FeatureName/
├── Components/    ← React components specific to this feature
├── Hooks/         ← Custom React hooks (useReservation, useRooms, etc.)
├── Interfaces/    ← TypeScript types/interfaces for this feature
└── Service/       ← API calls specific to this feature
```

---

## N-Layer Summary (Course requirement)

The project follows **N-layer architecture** as required by the professor:

| Layer | Backend | Frontend |
|---|---|---|
| Presentation | API/Controllers | React Components |
| Business Logic | Application/Services + UseCases | Hooks + feature Services |
| Data Access | Infrastructure/Persistance + Data | Shared/Service (API client) |
| Domain | Domain/Entities + Interfaces | Interfaces (TypeScript types) |

---

## Tech Stack

| Area | Technology |
|---|---|
| Frontend framework | React (feature-based modules) |
| Frontend language | TypeScript |
| Backend | (to be defined by team — Java Spring Boot / .NET / Node.js) |
| Database | Relational DB (MySQL / PostgreSQL) |
| ORM | (matches backend choice) |
| Version control | Git + GitHub/GitLab |
| Project management | Trello |
| CI/CD | DevOps pipeline |
| Design tool | draw.io (diagrams confirmed by uploaded files) |
