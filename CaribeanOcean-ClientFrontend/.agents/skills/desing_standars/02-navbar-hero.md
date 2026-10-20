# Navbar & Hero — Caribbean Ocean Resort & Spa

---

## Navbar

### Layout
| Property | Value |
|---|---|
| Height | 72px |
| Max width | 1200px centered |
| Position | Fixed, top 0, full width |
| Z-index | 100 |
| Padding horizontal | 80px desktop / 40px tablet / 20px mobile |

### States

#### Default (over hero)
| Property | Value |
|---|---|
| Background | Transparent |
| Transition | background 300ms ease |
| Logo color | `#FFFFFF` |
| Links color | `#FFFFFF` |
| CTA border | 1px solid `#FFFFFF` |
| CTA text | `#FFFFFF` |

#### On scroll (after 80px)
| Property | Value |
|---|---|
| Background | `#0C3D52` at 95% opacity |
| Logo color | `#FFFFFF` |
| Links color | `#FFFFFF` |
| CTA background | `#1D9E75` |
| CTA text | `#FFFFFF` |

### Typography
| Element | Font | Size | Weight | Color | Letter Spacing |
|---|---|---|---|---|---|
| Logo name | Cormorant Garamond | 22px | 600 | `#FFFFFF` | 0.5px |
| Logo tagline | Cormorant Garamond Italic | 11px | 400 | `#A8DFF0` | 1px |
| Nav links | Montserrat | 13px | 500 | `#FFFFFF` | 0.5px |
| CTA button | Montserrat | 13px | 600 | `#FFFFFF` | 1px |

### Nav Links
| Property | Value |
|---|---|
| Links separator | `·` in `#1D9E75` |
| Link hover | Color `#1D9E75`, transition 150ms |
| Active link | Color `#1D9E75`, bottom border 1px `#1D9E75` |
| Gap between links | 32px |

### CTA Button
| Property | Value |
|---|---|
| Padding | 10px 24px |
| Border radius | 30px |
| Border | 1px solid `#FFFFFF` (default) / none (on scroll) |
| Background | Transparent (default) / `#1D9E75` (on scroll) |
| Hover background | `#1D9E75` |
| Hover border color | `#1D9E75` |
| Transition | 250ms ease |

### Mobile (< 768px)
| Property | Value |
|---|---|
| Height | 60px |
| Logo size | 18px |
| Nav links | Hidden — hamburger menu |
| Hamburger icon | 24px, `#FFFFFF` |
| Mobile menu background | `#0C3D52` full screen |
| Mobile links | Cormorant Garamond 32px, centered |

---

## Hero

### Layout
| Property | Value |
|---|---|
| Height | 100vh (full screen) |
| Min height | 600px |
| Background | Full-width photograph |
| Overlay | `#0C3D52` at 50% opacity |
| Content position | Vertically centered, left aligned |
| Content padding left | 8% desktop / 5% tablet / 20px mobile |
| Content max width | 680px |

### Background Image
| Property | Value |
|---|---|
| Object fit | cover |
| Object position | center center |
| Loading | eager (above the fold) |

### Text Content
| Element | Font | Size | Weight | Color | Letter Spacing |
|---|---|---|---|---|---|
| Eyebrow label | Montserrat | 13px | 400 | `#A8DFF0` | 4px |
| Main title | Cormorant Garamond | 72px | 700 | `#FFFFFF` | -1px |
| Subtitle | Cormorant Garamond Italic | 24px | 400 | `#A8DFF0` | 0 |
| Tagline | Cormorant Garamond Italic | 18px | 400 | `#A8DFF0` | 1px |

### Eyebrow Label
| Property | Value |
|---|---|
| Text | `PUERTO VIEJO DE TALAMANCA · COSTA RICA` |
| Text transform | uppercase |
| Margin bottom | 16px |

### Decorative Line
| Property | Value |
|---|---|
| Width | 80px |
| Height | 2px |
| Color | `#1D9E75` |
| Margin | 20px 0 |

### Buttons
| Element | Background | Border | Text | Padding | Border Radius |
|---|---|---|---|---|---|
| Primary CTA | `#1D9E75` | none | `#FFFFFF` Montserrat 13px 600 | 16px 36px | 30px |
| Secondary CTA | transparent | 1px `#FFFFFF` | `#FFFFFF` Montserrat 13px 600 | 16px 36px | 30px |
| Gap between buttons | 16px | | | | |
| Primary hover | `#0F6E56` | | | | |
| Secondary hover | `#FFFFFF` 10% opacity bg | | | | |

### Scroll Indicator
| Property | Value |
|---|---|
| Icon | Chevron down arrow |
| Size | 24px |
| Color | `#FFFFFF` at 60% opacity |
| Position | Bottom center, 32px from bottom |
| Animation | Bounce up-down, 1.5s infinite |

### Responsive
| Breakpoint | Title Size | Subtitle Size | Content Padding |
|---|---|---|---|
| Mobile < 768px | 42px | 18px | 20px |
| Tablet 768–1024px | 56px | 21px | 5% |
| Desktop > 1024px | 72px | 24px | 8% |
