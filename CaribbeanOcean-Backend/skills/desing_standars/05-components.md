# UI Components — Caribbean Ocean Resort & Spa

---

## Buttons

### Primary Button
| Property | Value |
|---|---|
| Background | `#1D9E75` |
| Text | Montserrat 13px 600 `#FFFFFF` uppercase letter-spacing 1px |
| Padding | 16px 36px |
| Border radius | 30px |
| Border | none |
| Min width | 160px |
| Hover background | `#0F6E56` |
| Active scale | 0.98 |
| Transition | 250ms ease |
| Focus shadow | `0 0 0 3px rgba(29,158,117,0.3)` |
| Disabled background | `#A8C4CE` |
| Disabled cursor | not-allowed |

### Secondary Button
| Property | Value |
|---|---|
| Background | transparent |
| Text | Montserrat 13px 600 `#0C3D52` uppercase letter-spacing 1px |
| Padding | 15px 35px |
| Border radius | 30px |
| Border | 1px solid `#0C3D52` |
| Hover background | `#0C3D52` |
| Hover text | `#FFFFFF` |
| Transition | 250ms ease |

### Ghost Button (on dark backgrounds)
| Property | Value |
|---|---|
| Background | transparent |
| Text | Montserrat 13px 600 `#FFFFFF` uppercase letter-spacing 1px |
| Padding | 15px 35px |
| Border radius | 30px |
| Border | 1px solid `#FFFFFF` |
| Hover background | `#FFFFFF` 10% opacity |
| Transition | 250ms ease |

### Text Link Button
| Property | Value |
|---|---|
| Text | Montserrat 13px 600 `#1D9E75` |
| Decoration | none (default) |
| Hover decoration | underline |
| Arrow | → appended, shifts right 4px on hover |
| Transition | 150ms ease |

### Icon Button
| Property | Value |
|---|---|
| Size | 44px × 44px |
| Shape | Circle |
| Background | `#FDFCFA` |
| Border | 1px solid `#C9B878` |
| Icon size | 20px `#0C3D52` |
| Hover background | `#1D9E75` |
| Hover icon | `#FFFFFF` |
| Hover border | `#1D9E75` |
| Transition | 250ms ease |

---

## Form Inputs

### Text Input
| Property | Value |
|---|---|
| Height | 48px |
| Background | `#F5F0E8` |
| Border | 1px solid `#C9B878` |
| Border radius | 4px |
| Padding | 0 16px |
| Font | Lato 14px 400 `#1A1A1A` |
| Placeholder | Lato 14px 400 `#6B6B6B` |
| Focus border | `#1D9E75` |
| Focus shadow | `0 0 0 3px rgba(29,158,117,0.2)` |
| Error border | `#D85A30` |
| Error message | Lato 12px 400 `#D85A30` |
| Disabled background | `#E8E4DC` |
| Disabled cursor | not-allowed |

### Select / Dropdown
| Property | Value |
|---|---|
| Same as text input | all input styles apply |
| Custom arrow | chevron-down 16px `#0C3D52` |
| Appearance | none (custom styled) |
| Options background | `#FDFCFA` |
| Option hover | background `#F5F0E8` |
| Option selected | color `#1D9E75` |

### Date Picker
| Property | Value |
|---|---|
| Same as text input | all input styles apply |
| Calendar icon | 16px `#1D9E75`, right side |
| Calendar popup background | `#FDFCFA` |
| Selected date | background `#1D9E75` text `#FFFFFF` |
| Today | border 1px `#1D9E75` |
| Range highlight | background `#E1F5EE` |
| Disabled dates | color `#C9B878` cursor not-allowed |

### Field Label
| Property | Value |
|---|---|
| Font | Montserrat 11px 600 `#0C3D52` |
| Text transform | uppercase |
| Letter spacing | 1px |
| Margin bottom | 6px |

---

## Cards

### Room Card
Documented in `03-sections.md` — Rooms / Rates Section.

### Facility Card
Documented in `03-sections.md` — Facilities Section.

### Offer Card
Documented in `03-sections.md` — Offers Section.

### Info Card (generic)
| Property | Value |
|---|---|
| Background | `#FDFCFA` |
| Border radius | 8px |
| Border | 1px solid `#F5F0E8` |
| Padding | 28px |
| Box shadow | none |
| Hover border | 1px solid `#C9B878` |
| Transition | 250ms ease |

---

## Badges & Tags

### Section Label
| Property | Value |
|---|---|
| Font | Montserrat 11px 600 `#1D9E75` |
| Text transform | uppercase |
| Letter spacing | 3px |
| Usage | Above every section title |

### Room Type Tag
| Property | Value |
|---|---|
| Font | Montserrat 11px 600 `#1D9E75` |
| Text transform | uppercase |
| Letter spacing | 1px |
| Usage | Above room card title |

### Offer Badge
| Property | Value |
|---|---|
| Background | `#D85A30` |
| Font | Montserrat 11px 600 `#FFFFFF` uppercase |
| Padding | 4px 12px |
| Border radius | 4px |
| Position | Absolute, top-left of card image |

### Most Popular Badge
| Property | Value |
|---|---|
| Background | `#1D9E75` |
| Font | Montserrat 10px 600 `#FFFFFF` uppercase |
| Padding | 4px 16px |
| Border radius | 0 0 4px 4px |
| Position | Absolute top center of card |

### Season Badge
| Property | Value |
|---|---|
| Background | `#F5F0E8` |
| Font | Montserrat 11px 600 `#0C3D52` |
| Border | 1px solid `#C9B878` |
| Padding | 4px 12px |
| Border radius | 30px |

### Sustainability Badge
| Property | Value |
|---|---|
| Background | transparent |
| Border | 1px solid `#1D9E75` 40% |
| Font | Montserrat 10px 600 `#1D9E75` uppercase letter-spacing 1px |
| Padding | 4px 12px |
| Border radius | 4px |
| Icon | 12px leaf before text |

---

## Decorative Elements

### Section Decorative Line
| Property | Value |
|---|---|
| Width | 60px |
| Height | 2px |
| Color | `#1D9E75` |
| Placement | Below section label, above H2 |
| Margin | 12px 0 |

### Hero Decorative Line
| Property | Value |
|---|---|
| Width | 80px |
| Height | 2px |
| Color | `#1D9E75` |
| Placement | Below hero subtitle |
| Margin | 20px 0 |

### Divider
| Property | Value |
|---|---|
| Height | 1px |
| Color | `#C9B878` at 40% opacity |
| Usage | Between content sections when background is same color |

### Quote Mark (testimonials)
| Property | Value |
|---|---|
| Character | `"` |
| Font | Cormorant Garamond 80px 400 |
| Color | `#1D9E75` at 20% opacity |
| Position | Absolute top-left of card |

---

## Loading & Feedback States

### Page Loading
| Property | Value |
|---|---|
| Overlay background | `#0C3D52` |
| Logo | Centered, white |
| Loader | Thin ring 2px `#1D9E75`, spinning 800ms linear |

### Skeleton Loading (cards)
| Property | Value |
|---|---|
| Background | `#F5F0E8` |
| Animation | Shimmer left to right, 1.5s ease infinite |
| Border radius | Same as element |

### Success Toast
| Property | Value |
|---|---|
| Background | `#E1F5EE` |
| Border left | 3px solid `#1D9E75` |
| Icon | checkmark 20px `#1D9E75` |
| Font | Lato 14px 400 `#085041` |
| Border radius | 4px |
| Position | Bottom right, 24px from edges |
| Duration | 4000ms auto dismiss |

### Error Toast
| Property | Value |
|---|---|
| Background | `#FAECE7` |
| Border left | 3px solid `#D85A30` |
| Icon | x-circle 20px `#D85A30` |
| Font | Lato 14px 400 `#712B13` |
| Border radius | 4px |
| Position | Bottom right, 24px from edges |
| Duration | 5000ms auto dismiss |
