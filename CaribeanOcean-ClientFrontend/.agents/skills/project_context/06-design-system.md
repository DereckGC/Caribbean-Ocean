# 06 — Design System

## Color Palette
| Token | HEX | Usage |
|---|---|---|
| `--color-ocean-deep` | `#0C3D52` | Primary — navbar, footer, primary buttons |
| `--color-teal` | `#1D9E75` | Main accent — CTAs, highlights, decorative lines |
| `--color-teal-dark` | `#0F6E56` | Hover state of teal |
| `--color-sand` | `#F5F0E8` | General site background |
| `--color-pearl` | `#FDFCFA` | Cards, light section backgrounds |
| `--color-night` | `#071E28` | Footer background |
| `--color-jungle` | `#2D6E3E` | Secondary accent — icons, badges |
| `--color-coral` | `#D85A30` | Alerts, promotions, offers |
| `--color-wood` | `#8B5E3C` | Decorative details, dividers |
| `--color-gold` | `#C9B878` | Input borders, dividers |
| `--text-primary` | `#1A1A1A` | Main body text |
| `--text-secondary` | `#6B6B6B` | Subtitles, captions |
| `--text-light` | `#FDFCFA` | Text on dark backgrounds |
| `--text-muted-light` | `#A8C4CE` | Secondary text on dark |
| `--text-accent-light` | `#A8DFF0` | Hero accents on dark |

---

## Typography
| Token | Font | Source |
|---|---|---|
| `--font-display` | Cormorant Garamond | Google Fonts |
| `--font-ui` | Montserrat | Google Fonts |
| `--font-body` | Lato | Google Fonts |

### Google Fonts Import
```css
@import url('https://fonts.googleapis.com/css2?family=Cormorant+Garamond:ital,wght@0,400;0,600;0,700;1,400&family=Montserrat:wght@400;500;600&family=Lato:wght@300;400;700&display=swap');
```

### Type Scale
| Element | Font | Size | Weight |
|---|---|---|---|
| Hero title | Cormorant Garamond | 72px | 700 |
| Hero subtitle | Cormorant Garamond Italic | 24px | 400 |
| Section H1 | Cormorant Garamond | 52px | 600 |
| Section H2 | Cormorant Garamond | 38px | 600 |
| Card title | Montserrat | 20px | 600 |
| Body text | Lato | 16px | 400 |
| Caption / label | Montserrat | 11px | 600 |
| Nav links | Montserrat | 13px | 500 |
| Buttons | Montserrat | 13px | 600 |
| Price / stat number | Cormorant Garamond | 32px | 600 |

---

## Spacing Scale
| Token | Value |
|---|---|
| `--space-4` | 16px |
| `--space-5` | 24px |
| `--space-6` | 32px |
| `--space-7` | 48px |
| `--space-8` | 64px |
| `--space-9` | 80px |
| `--space-10` | 100px (section padding desktop) |

---

## Border Radius
| Token | Value | Usage |
|---|---|---|
| `--radius-sm` | 4px | Inputs, small elements |
| `--radius-md` | 8px | Cards, images |
| `--radius-lg` | 12px | Booking widget, modals |
| `--radius-pill` | 30px | Buttons, tags |

---

## Buttons
| Variant | Background | Text | Border |
|---|---|---|---|
| Primary | `#1D9E75` | `#FFFFFF` | none |
| Secondary | transparent | `#0C3D52` | 1px `#0C3D52` |
| Ghost | transparent | `#FFFFFF` | 1px `#FFFFFF` |
| Dark | `#0C3D52` | `#FFFFFF` | none |

All buttons: Montserrat 13px 600 uppercase, padding 16px 36px, border-radius 30px, transition 250ms

---

## Section Backgrounds (alternating pattern)
1. `#FDFCFA` — Pearl White (Welcome/About)
2. `#F5F0E8` — Sand (Rooms, Facilities)
3. `#0C3D52` — Ocean Deep (dark sections)

---

## Layout
- Max content width: 1200px centered
- Navbar height: 72px (fixed)
- Hero height: 100vh
- Padding desktop: 80px lateral, 100px vertical
- Padding tablet: 40px lateral, 80px vertical
- Padding mobile: 20px lateral, 64px vertical

---

## Responsive Breakpoints
| Name | Width |
|---|---|
| Mobile | < 768px |
| Tablet | 768–1024px |
| Desktop | 1024–1280px |
| Wide | > 1280px |

Mobile: Ad area hidden, hamburger menu, single column layouts.

---

## Admin Module Design
- Sidebar background: `#0C3D52`
- Main content background: `#F5F0E8`
- Top bar: `#FDFCFA` with bottom border
- Dark card (dashboard stats): near-black background
- Table header: `#F5F0E8`
- Action buttons: dark rounded pills (Manage / Update / Delete)
