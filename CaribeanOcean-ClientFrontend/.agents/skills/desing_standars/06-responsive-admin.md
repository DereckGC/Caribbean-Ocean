# Responsive & Admin Module — Caribbean Ocean Resort & Spa

---

## Responsive Design

### Breakpoints
| Name | Range | Target device |
|---|---|---|
| Mobile | < 768px | Phones |
| Tablet | 768px – 1024px | Tablets, small laptops |
| Desktop | 1024px – 1280px | Laptops |
| Wide | > 1280px | Large screens |

### Grid System
| Breakpoint | Columns | Gutter | Padding lateral |
|---|---|---|---|
| Mobile | 4 | 16px | 20px |
| Tablet | 8 | 24px | 40px |
| Desktop | 12 | 32px | 80px |
| Wide | 12 | 32px | Auto (max 1200px centered) |

### Section Padding Vertical
| Breakpoint | Large sections | Medium sections |
|---|---|---|
| Mobile | 64px | 48px |
| Tablet | 80px | 64px |
| Desktop | 100px | 80px |

### Typography Responsive Scale
| Element | Mobile | Tablet | Desktop |
|---|---|---|---|
| Hero title | 42px | 56px | 72px |
| Hero subtitle | 18px | 21px | 24px |
| H1 section | 36px | 44px | 52px |
| H2 section | 28px | 34px | 38px |
| H3 card | 18px | 19px | 20px |
| Body text | 15px | 16px | 16px |

### Component Adjustments by Breakpoint

#### Navbar
| Breakpoint | Behavior |
|---|---|
| Desktop | Full horizontal nav, all links visible |
| Tablet | Logo + CTA button + hamburger icon |
| Mobile | Logo centered + hamburger icon |

#### Hero
| Breakpoint | Behavior |
|---|---|
| Desktop | Text left-aligned, 50% width |
| Tablet | Text left-aligned, 70% width |
| Mobile | Text centered, full width, reduced overlay opacity to 60% |

#### Rooms Grid
| Breakpoint | Columns |
|---|---|
| Desktop | 2 columns |
| Tablet | 2 columns |
| Mobile | 1 column |

#### Facilities Grid
| Breakpoint | Columns |
|---|---|
| Desktop | 4 columns |
| Tablet | 2 columns |
| Mobile | 1 column |

#### Offers Grid
| Breakpoint | Columns |
|---|---|
| Desktop | 3 columns |
| Tablet | 2 columns |
| Mobile | 1 column |

#### Booking Widget
| Breakpoint | Layout |
|---|---|
| Desktop | 4 fields in a row + button |
| Tablet | 2 fields per row, button full width |
| Mobile | 1 field per row, button full width |

#### Footer
| Breakpoint | Columns |
|---|---|
| Desktop | 4 columns |
| Tablet | 2 columns |
| Mobile | 1 column, centered |

---

## Admin Module

### General Layout
| Property | Value |
|---|---|
| Sidebar width | 260px |
| Sidebar background | `#0C3D52` |
| Main content background | `#F5F0E8` |
| Top bar height | 64px |
| Top bar background | `#FDFCFA` |
| Top bar border bottom | 1px solid `#C9B878` 40% |
| Content padding | 32px |
| Max content width | 1100px |

### Sidebar
| Element | Font | Size | Weight | Color |
|---|---|---|---|---|
| Logo | Cormorant Garamond | 18px | 600 | `#FDFCFA` |
| Logo tagline | Cormorant Garamond Italic | 11px | 400 | `#A8C4CE` |
| Section divider label | Montserrat | 10px | 600 | `#A8C4CE` 60% | uppercase, letter-spacing 2px |
| Nav link | Lato | 14px | 400 | `#A8C4CE` |
| Nav link icon | 18px | — | `#A8C4CE` | |
| Nav link hover | background `#071E28`, text `#FDFCFA` |
| Nav link active | background `#1D9E75` 20%, text `#1D9E75`, left border 3px `#1D9E75` |
| Nav link padding | 12px 20px |

### Top Bar
| Element | Font | Size | Color |
|---|---|---|---|
| Page title | Cormorant Garamond | 24px 600 | `#0C3D52` |
| Username | Lato | 14px 400 | `#6B6B6B` |
| User avatar | 36px circle, initials Montserrat 14px `#FDFCFA`, background `#1D9E75` |
| Logout link | Lato | 13px 400 | `#D85A30` |

### Admin Tables
| Property | Value |
|---|---|
| Background | `#FDFCFA` |
| Border radius | 8px |
| Border | 1px solid `#F5F0E8` |
| Header background | `#F5F0E8` |
| Header font | Montserrat 12px 600 `#0C3D52` uppercase letter-spacing 1px |
| Header padding | 14px 20px |
| Row padding | 14px 20px |
| Row border bottom | 1px solid `#F5F0E8` |
| Row hover background | `#F5F0E8` |
| Row font | Lato 14px 400 `#1A1A1A` |
| Row secondary | Lato 13px 400 `#6B6B6B` |

### Admin Table Actions
| Element | Style |
|---|---|
| View link | Lato 13px 400 `#1D9E75`, hover underline |
| Edit link | Lato 13px 400 `#0C3D52`, hover underline |
| Delete link | Lato 13px 400 `#D85A30`, hover underline |
| Separator between actions | `·` `#C9B878` |

### Status Badges (tables)
| Status | Background | Text |
|---|---|---|
| Available / Active | `#E1F5EE` | Montserrat 11px 600 `#0F6E56` |
| Reserved | `#E6F1FB` | Montserrat 11px 600 `#185FA5` |
| Occupied | `#FAEEDA` | Montserrat 11px 600 `#854F0B` |
| Cancelled / Inactive | `#FAECE7` | Montserrat 11px 600 `#993C1D` |

### Admin Forms
| Property | Value |
|---|---|
| Form background | `#FDFCFA` |
| Form border radius | 8px |
| Form padding | 32px |
| Section title | Montserrat 14px 600 `#0C3D52` border-bottom 1px `#F5F0E8` pb 12px mb 24px |
| Field layout | 2 columns desktop / 1 column mobile |
| Field gap | 24px |
| All input styles | Same as public site form inputs |
| Form actions bar | background `#F5F0E8` padding 20px 32px border-top 1px `#C9B878` 40% |
| Submit button | Primary button style |
| Cancel button | Secondary button style |

### WYSIWYG Editor (TinyMCE)
| Property | Value |
|---|---|
| Toolbar background | `#F5F0E8` |
| Toolbar border | 1px solid `#C9B878` 40% |
| Editor background | `#FDFCFA` |
| Editor border | 1px solid `#C9B878` 40% |
| Editor border radius | 0 0 4px 4px |
| Editor font | Lato 16px 400 `#1A1A1A` |
| Editor min height | 300px |
| Editor focus border | `#1D9E75` |

### Admin Login Page
| Property | Value |
|---|---|
| Background | `#0C3D52` |
| Card background | `#FDFCFA` |
| Card border radius | 12px |
| Card padding | 48px |
| Card max width | 420px centered |
| Logo | Cormorant Garamond 28px 600 `#0C3D52` centered |
| Subtitle | Lato 14px 400 `#6B6B6B` centered mb 32px |
| Field spacing | 20px between fields |
| Submit button | Full width, primary style |
| Error message | `#D85A30` Lato 13px, below affected field |
| General error | Error toast style, above form |
