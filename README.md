# Caribbean-Ocean
Full-stack hotel management platform for a luxury eco-resort in Costa Rica. Built with React 19, ASP.NET Core 10 (Clean Architecture), SQL Server, FastAPI + Python. Features online booking, dinamic content, admin dashboard and an AI-powered reservation prediction model.


# 🌊 Caribbean Ocean Resort & Spa

> Full-stack hotel management platform for a luxury eco-resort in Costa Rica. Features online booking, dynamic CMS, admin dashboard and an AI-powered reservation prediction model.

<img width="1906" height="959" alt="image" src="https://github.com/user-attachments/assets/107d9495-3ec9-44aa-ad71-937e88937838" />
<img width="1700" height="856" alt="image" src="https://github.com/user-attachments/assets/cc2d2aef-0463-49a4-aa3a-ed48f8e01440" />
<img width="1639" height="868" alt="image" src="https://github.com/user-attachments/assets/16d9be2b-d7a4-412b-94bf-b8e255cc9018" />

---

## What it does

The system is split into three independent modules:

- **Public website** — guest-facing React SPA with hero slider, room catalog, 3-step online booking flow, offers carousel, facilities, maps and contact info.
- **Admin portal** — protected management dashboard for reservations (CRUD), room management, dynamic CMS, gallery, hero banners, offers, seasons, advertisement banners, hotel contact info and a revenue dashboard with charts.
- **Analytics service** — async Python REST API that trains a predictive model on reservation data to forecast hotel occupancy.

---

## Tech Stack

| Area | Stack |
|---|---|
| Frontend | React 19 · TypeScript · Vite · React Router v7 · Axios · Recharts |
| Backend | C# · ASP.NET Core 10 · Clean Architecture (Onion) · JWT Auth |
| Database | SQL Server · Stored Procedures |
| Image storage | Cloudinary |
| Analytics | Python · FastAPI · SQLAlchemy (async) · Celery · Redis · scikit-learn |
| Logging | MongoDB |
| DevOps | Docker · Docker Compose · GitHub Actions |

---

## Architecture

The backend follows **Clean Architecture** with strict inward dependency rules:

```
API  →  Application  →  Domain  ←  Infrastructure
```

Domain has zero external dependencies. Interfaces live in Domain, implementations in Infrastructure.

The frontend follows a **Feature-Based (Vertical Slice)** structure — each domain (booking, rooms, dashboard, etc.) owns its components, hooks, services and TypeScript interfaces in isolation.

---
