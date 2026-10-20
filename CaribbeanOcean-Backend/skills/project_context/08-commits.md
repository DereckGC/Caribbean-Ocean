# 08 — Conventional Commits Guide

## Structure
```
git commit -m "<type>(<scope>): <description>"
```

**Rules:**
- Type and scope always lowercase
- Description lowercase, no period at end
- Use imperative present tense: `add`, `fix`, `update` (NOT `added`, `fixed`)
- One concern per commit — never bundle multiple changes

---

## Types

| Type | When to use | Semver |
|---|---|---|
| `feat` | New feature visible to user | MINOR |
| `fix` | Bug fix visible to user | PATCH |
| `refactor` | Code restructure, no behavior change | — |
| `perf` | Performance improvement | PATCH |
| `style` | Formatting, spacing, no logic | — |
| `test` | Add or fix tests | — |
| `docs` | Documentation only | — |
| `build` | Build system, dependencies | — |
| `ops` | Infrastructure, deployment | — |
| `chore` | Maintenance, .gitignore, initial commit | — |

Add `!` after type/scope for **BREAKING CHANGE** → MAJOR

---

## Project Scopes

### Frontend (public site)
`navbar`, `hero`, `about`, `offers`, `fees`, `reservation`, `facilities`, `how-to`, `contact`, `footer`, `ads`

### Frontend (admin)
`admin`, `auth`, `dashboard`, `manage-pages`, `manage-rooms`, `manage-reservations`, `hotel-state`, `room-availability`, `offers-admin`

### Shared frontend
`tokens`, `components`, `responsive`, `animations`, `layout`

### Backend
`api`, `db`, `auth-api`, `rooms-api`, `reservations-api`, `offers-api`, `seasons-api`, `facilities-api`, `ads-api`, `pages-api`

### Project
`deps`, `ci`, `docs`, `release`

---

## Examples by Category

### New features
```bash
feat(reservation): add 3-step booking flow with availability check
feat(reservation): add success modal with auto-generated reservation code
feat(reservation): add unavailability page with nearby date suggestions
feat(offers): add offer cards carousel with prev/next navigation
feat(admin): add room management table with active/inactive toggle
feat(admin): add reservation list with manage, update and delete actions
feat(auth): add admin login page with split-screen layout
feat(dashboard): add monthly stats with colored progress bars
feat(db): add offer entity with discount_percentage and season relation
feat(db): add season entity with start_date and end_date
feat(api): add room availability endpoint by date range
feat(manage-pages): add edit modal with title, description, fee and image fields
feat(hotel-state): add today status table with print functionality
feat(room-availability): add availability search form and results table
```

### Bug fixes
```bash
fix(navbar): scrolled class not applying when page loads mid-scroll
fix(reservation): allow past dates to be selected in check-in input
fix(reservation): total amount not recalculating on room type change
fix(fees): room images not loading on mobile due to wrong asset path
fix(footer): grid layout overflows at 768px breakpoint
fix(ads): ad area visible on mobile, should be hidden below 768px
fix(auth): login not redirecting to dashboard on success
fix(admin): reservation delete confirmation dialog not showing
```

### Refactors
```bash
refactor(components): extract btn classes to reusable component
refactor(tokens): split color and typography tokens into separate sections
refactor(reservation): simplify form validation using constraint API
refactor(db): rename all entity attributes to real-world snake_case
refactor(api): separate controllers from business logic into use cases
```

### Style / design
```bash
style(hero): align cta buttons to left on mobile breakpoint
style(tokens): update section-label letter-spacing from 2px to 3px
style(offers): increase offer-card title from 18px to 20px
style(about): adjust padding and gap between text and gallery columns
```

### Tests
```bash
test(reservation): add form validation unit tests for date range check
test(api): add availability endpoint integration tests
test(auth): add admin login tests for invalid credentials
test(db): add room status logic unit tests
```

### Documentation
```bash
docs: add project README with setup and run instructions
docs(tokens): add inline comments to all CSS custom properties
docs: add frontend blueprint for one-shot generation guide
docs(release): create v1.0.0 changelog entry
```

### Build / infrastructure
```bash
build: initial project setup with folder structure and base files
build(deps): add axios for API client
build(deps): add react-router-dom v6 for routing
ops: add deploy script for production environment
chore: add .gitignore for node_modules, dist and .env files
chore: initial commit
```

### Breaking changes
```bash
feat(api)!: change reservation endpoint response to include season_id
refactor(db)!: rename RESERVACION table to RESERVATION
```

---

## Workflow by Context

### New feature (step by step)
```bash
# 1. Setup
git commit -m "chore(reservation): create reservation page files"

# 2. Build
git commit -m "feat(reservation): add search form with dates and room type"
git commit -m "feat(reservation): add results list with available rooms"
git commit -m "feat(reservation): add guest form with payment fields"
git commit -m "feat(reservation): add success modal with reservation code"

# 3. Fix issues
git commit -m "fix(reservation): date validation not blocking same-day check-in"

# 4. Polish
git commit -m "style(reservation): adjust booking widget padding to 36px"
```

### Database work
```bash
git commit -m "feat(db): create initial schema with all 9 entities"
git commit -m "feat(db): add offer and season entities with foreign keys"
git commit -m "refactor(db): apply real-world snake_case naming convention"
```

### Admin module
```bash
git commit -m "feat(auth): add login page with split-screen layout"
git commit -m "feat(dashboard): add monthly statistics with progress bars"
git commit -m "feat(manage-pages): add page grid with edit modal"
git commit -m "feat(manage-reservations): add table with manage update delete"
git commit -m "feat(manage-rooms): add room type cards with manage and update"
git commit -m "feat(hotel-state): add today status table with print button"
```

---

## What NEVER to do
```bash
# ❌ No type
git commit -m "fixed the reservation bug"

# ❌ Too vague
git commit -m "fix: fixed stuff"

# ❌ Multiple concerns
git commit -m "feat: add reservation page, fix navbar, update footer"

# ❌ Uppercase description
git commit -m "feat(hero): Add hero slider"

# ❌ Period at end
git commit -m "feat(hero): add hero slider."

# ❌ Past tense
git commit -m "feat(hero): added hero slider"
```
