---
name: caribbean-ocean-context
description: >
  Master context skill for the Caribbean Ocean Resort & Spa software engineering
  project. Use this skill ALWAYS when the user asks ANYTHING related to this project:
  hotel website, admin module, user stories, sprints, database design, frontend mockups,
  backend architecture, package diagrams, design system, commit conventions, UML diagrams,
  or any technical or planning decision. Trigger on any mention of Caribbean Ocean, hotel
  project, reservations system, room management, admin module, facilities, offers, seasons,
  frontend pages, backend layers, React modules, or any component of this project.
  Also trigger for questions about architecture (Clean Architecture, n-layers), frontend
  package structure (React modules), backend packages (API, Application, Domain,
  Infrastructure), database model, ERD, or UML class diagrams related to this project.
---

# Caribbean Ocean Resort & Spa — Master Project Context

## Reference Index

| File | Contents |
|---|---|
| `references/01-project-overview.md` | Course info, SCRUM rules, sprint schedule, deliverables, evaluation |
| `references/02-database.md` | All 9 entities, attributes, relationships, naming conventions |
| `references/03-architecture.md` | Backend layers, frontend packages, Clean Architecture breakdown |
| `references/04-frontend-pages.md` | All 11 public pages + mockup inventory |
| `references/05-admin-module.md` | All 8 admin sections, flows, modals |
| `references/06-design-system.md` | Colors, fonts, tokens, spacing, components |
| `references/07-user-stories.md` | User stories by section with acceptance criteria |
| `references/08-commits.md` | Conventional commits guide with project-specific examples |

---

## At a Glance

**Project:** Caribbean Ocean Resort & Spa  
**Tagline:** Where the jungle meets the sea.  
**Location:** Playa Cocles, Puerto Viejo de Talamanca, Limón, Costa Rica  
**Course:** Ingeniería de Software 2026 — 35% of final grade  
**Period:** March 17 – June 27, 2026 | Sprint 0 → Sprint 5  
**Methodology:** SCRUM with Trello  
**Stack:** React (frontend) + n-layer backend + REST API  

---

## Core Entities (quick ref)

| Table | PK | Key FKs |
|---|---|---|
| `ADMIN_USER` | id | — |
| `ROOM_TYPE` | id | — |
| `ROOM` | id | room_type_id |
| `RESERVATION` | id | room_id, season_id, offer_id |
| `OFFER` | id | room_type_id, season_id, updated_by |
| `SEASON` | id | — |
| `PAGE_CONTENT` | id | updated_by |
| `FACILITY` | id | updated_by |
| `ADVERTISEMENT` | id | updated_by |

> Full schema with all columns → `references/02-database.md`

---

## Architecture (quick ref)

### Backend (Clean Architecture — 4 layers)
```
API/           → Controllers
Application/   → DTOs, Mappers, Services, UseCases
Domain/        → Entities, Exceptions, Interfaces
Infrastructure/ → Persistance, Data, ExternalServices
```

### Frontend (React — feature-based modules)

**Public site modules:** App (Router, Layout) → Home, RoomsAndPrices, Booking → Shared  
**Admin modules:** App (Router, Layout) → Loggin, Home, ModifyPages, Reservations, RoomAdministration, HotelState, RoomAvailability, Adds, Estadistics → Shared  

> Full diagram breakdown → `references/03-architecture.md`

---

## Sprint Schedule

| Date | Sprint |
|---|---|
| 21 MAR 2026 | Sprint 0 — Planning + architecture |
| 07 APR 2026 | Sprint 1 |
| 21 APR 2026 | Sprint 2 |
| 09 MAY 2026 | Sprint 3 — Retrospective |
| 02 JUN 2026 | Sprint 4 |
| 27 JUN 2026 | Sprint 5 — Final delivery |

---

## Design System (quick ref)

```css
--color-ocean-deep: #0C3D52   /* primary */
--color-teal:       #1D9E75   /* accent / CTAs */
--color-sand:       #F5F0E8   /* background */
--color-pearl:      #FDFCFA   /* cards */
--color-night:      #071E28   /* footer */
--color-coral:      #D85A30   /* offers / alerts */

--font-display: 'Cormorant Garamond'
--font-ui:      'Montserrat'
--font-body:    'Lato'
```

> Full tokens, spacing, components → `references/06-design-system.md`

---

## Commit Convention (quick ref)

```
feat(scope):     new feature
fix(scope):      bug fix
refactor(scope): code restructure
style(scope):    formatting only
docs(scope):     documentation
test(scope):     tests
build(scope):    deps / build system
chore:           maintenance
```

Key scopes: `navbar`, `hero`, `reservation`, `offers`, `fees`, `facilities`, `admin`, `auth`, `db`, `api`, `tokens`, `components`, `footer`

> Full guide with 40+ examples → `references/08-commits.md`
