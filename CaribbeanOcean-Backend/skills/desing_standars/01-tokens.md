# Design Tokens — Caribbean Ocean Resort & Spa

## Color Palette

### Primary Colors
| Token | HEX | Usage |
|---|---|---|
| `--color-ocean-deep` | `#0C3D52` | Primary — navbar, footer, primary buttons |
| `--color-caribbean-teal` | `#1D9E75` | Main accent — CTAs, highlights, decorative lines |
| `--color-sand-white` | `#F5F0E8` | General site background |
| `--color-pearl-white` | `#FDFCFA` | Cards and light section backgrounds |
| `--color-deep-night` | `#071E28` | Footer background |

### Accent Colors
| Token | HEX | Usage |
|---|---|---|
| `--color-jungle-green` | `#2D6E3E` | Secondary accent — icons, badges, details |
| `--color-coral-sunset` | `#D85A30` | Alerts, promotions, offers |
| `--color-warm-wood` | `#8B5E3C` | Decorative details, dividers |
| `--color-golden-sand` | `#C9B878` | Input borders, decorative elements |

### Text Colors
| Token | HEX | Usage |
|---|---|---|
| `--color-text-primary` | `#1A1A1A` | Main body text |
| `--color-text-secondary` | `#6B6B6B` | Subtitles, captions, secondary text |
| `--color-text-light` | `#FDFCFA` | Text on dark backgrounds |
| `--color-text-muted-light` | `#A8C4CE` | Secondary text on dark backgrounds |
| `--color-text-accent-light` | `#A8DFF0` | Hero subtitle, accents on dark |
| `--color-text-footer` | `#6B8A94` | Copyright and footer secondary |

---

## Typography

### Font Families
| Token | Font | Source | Usage |
|---|---|---|---|
| `--font-display` | Cormorant Garamond | Google Fonts | Titles, display text |
| `--font-display-italic` | Cormorant Garamond Italic | Google Fonts | Taglines, accent phrases |
| `--font-ui` | Montserrat | Google Fonts | Navigation, labels, buttons, subtitles |
| `--font-body` | Lato | Google Fonts | Body text, paragraphs, descriptions |

### Google Fonts Import
```css
@import url('https://fonts.googleapis.com/css2?family=Cormorant+Garamond:ital,wght@0,400;0,600;0,700;1,400;1,600&family=Montserrat:wght@400;500;600&family=Lato:wght@300;400;700&display=swap');
```

### Type Scale
| Token | Font | Size | Weight | Line Height | Usage |
|---|---|---|---|---|---|
| `--text-hero-title` | Cormorant Garamond | 72px | 700 | 1.1 | Hero main title |
| `--text-hero-subtitle` | Cormorant Garamond Italic | 24px | 400 | 1.4 | Hero subtitle |
| `--text-hero-label` | Montserrat | 13px | 400 | 1.4 | Hero eyebrow label |
| `--text-h1` | Cormorant Garamond | 52px | 600 | 1.2 | Section main titles |
| `--text-h2` | Cormorant Garamond | 38px | 600 | 1.3 | Section sub-titles |
| `--text-h3` | Montserrat | 20px | 600 | 1.4 | Card titles |
| `--text-h4` | Montserrat | 16px | 600 | 1.4 | Route titles, small headings |
| `--text-body-lg` | Lato | 16px | 400 | 1.8 | Main paragraphs |
| `--text-body` | Lato | 15px | 400 | 1.7 | Standard paragraphs |
| `--text-body-sm` | Lato | 14px | 400 | 1.6 | Card descriptions |
| `--text-caption` | Lato | 13px | 400 | 1.5 | Captions, footer links |
| `--text-label` | Montserrat | 11px | 600 | 1.4 | Section labels, tags |
| `--text-copyright` | Lato | 11px | 400 | 1.4 | Copyright text |
| `--text-nav` | Montserrat | 13px | 500 | 1.4 | Navbar links |
| `--text-btn` | Montserrat | 13px | 600 | 1 | All buttons |
| `--text-price` | Cormorant Garamond | 32px | 600 | 1.2 | Room rates |
| `--text-stat-number` | Cormorant Garamond | 42px | 600 | 1 | Stats / badges numbers |
| `--text-stat-label` | Montserrat | 11px | 400 | 1.4 | Stats / badges labels |

---

## Spacing Scale

| Token | Value | Usage |
|---|---|---|
| `--space-1` | 4px | Micro spacing |
| `--space-2` | 8px | Inner padding small |
| `--space-3` | 12px | Gap between small elements |
| `--space-4` | 16px | Standard gap |
| `--space-5` | 24px | Medium gap |
| `--space-6` | 32px | Grid gutter |
| `--space-7` | 48px | Section inner spacing |
| `--space-8` | 64px | Section padding mobile |
| `--space-9` | 80px | Section padding tablet |
| `--space-10` | 100px | Section padding desktop |

---

## Border Radius

| Token | Value | Usage |
|---|---|---|
| `--radius-sm` | 4px | Inputs, small elements |
| `--radius-md` | 8px | Cards, images |
| `--radius-lg` | 12px | Booking widget, modals |
| `--radius-pill` | 30px | Buttons, tags |
| `--radius-circle` | 50% | Avatars, stat badges |

---

## Transitions

| Token | Value | Usage |
|---|---|---|
| `--transition-fast` | 150ms ease | Hover states, small interactions |
| `--transition-base` | 250ms ease | Standard transitions |
| `--transition-slow` | 300ms ease | Navbar scroll, overlays |
| `--transition-enter` | 400ms ease-out | Section entrance animations |

---

## Shadows

The design is intentionally flat. No box-shadows on cards or containers. The only exception:

| Token | Value | Usage |
|---|---|---|
| `--shadow-none` | none | Cards, containers (flat design) |
| `--shadow-focus` | `0 0 0 3px rgba(29,158,117,0.3)` | Focus ring on inputs and buttons |
