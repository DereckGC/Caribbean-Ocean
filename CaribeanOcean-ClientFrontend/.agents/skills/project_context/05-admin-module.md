# 05 — Admin Module

## Admin Navbar
**Links (left to right):**
Logo | Manage pages | Manage reservations | Manage Rooms | Today's status | Room availability | Offers and advertising | Username (avatar icon) | Logout (→ icon)

Active section is underlined/highlighted.

---

## Page 1 — Login
**Layout:** Full-screen split (two halves)

**Left half (dark background):**
- H1: "Login"
- Subtitle: "Enter your account details"
- Username input (placeholder text)
- Password input (eye toggle icon for show/hide)
- "Login" button (full width, gray)
- "Don't have an account?" + "Sign up" button

**Right half:**
- Hotel hero image as background
- "Welcome to Caribbean Ocean" large overlay text

---

## Page 2 — Dashboard / Home (after login)
**Content:** "Estadisticas del mes" (Monthly Statistics card — dark card)

**Stats with horizontal progress bars (colored):**
| Stat | Color | Example value |
|---|---|---|
| Total de habitaciones reservadas | Red | 85 |
| Habitaciones premium reservadas | Green | 20 |
| Habitaciones estándares reservadas | Purple | 40 |
| Habitaciones junior reservadas | Blue | 25 |
| Total de ganancia obtenida | Blue | $1000 |

Each row: label + colored progress bar + numeric value on right.

---

## Page 3 — Manage Pages
**View 1 — Page Grid:**
4 cards in 2×2 grid, each showing:
- Thumbnail image
- Page name (Home / About Us / Facilities / How to get there?)
- "Edit page content" button (dark)

**On click "Edit page content" → Modal:**
Fields:
- ① Title
- Description
- Fee
- Image (current image preview)
- "Upload new image" button
- "Save changes" button

---

## Page 4 — Manage Reservations
**View 1 — Reservation Table:**
- "Add reservation" button (top left)
- Full-width table with reservation data rows
- Per row: Manage | Update | Delete buttons (dark)

**On "Update" → Modal:**
Fields:
- ① Start date
- End date
- Client's name
- Image
- "Upload new image" button
- "Save changes" button

*(Same modal used for Add reservation)*

**On "Manage" → New Page:**
- Full page showing reservation info card
- Large "ⓘ" icon centered
- "The client's reservation information will appear here"
- ← Return button + 🖨 Print button at bottom

---

## Page 5 — Manage Rooms
**View 1 — Room Type Cards:**
3 cards with images:
- Junior | Manage + Update buttons
- Standart (typo — Standard) | Manage + Update buttons
- Premium | Manage + Update buttons

**On "Manage" → Room List Table:**
- Full table of individual rooms for that type
- Per row: Manage button
- **Room state widget (right side):** "Room's state" with Active/Inactive radio buttons + "Save changes" button
- On "Manage" per row → toggles active/inactive state

**On "Update" → Modal (Room Type Edit):**
Fields:
- ① Room Title
- Description
- Fee
- Image preview
- "Upload new image" button
- "Save changes" button

---

## Page 6 — Today's Status
**Content:**
- Full-width table with all rooms:
  - Columns: room info + availability status
- **Print button** (bottom center, with printer icon)
- Note: "Cuando se pulse el botón de imprimir se imprimirá todo el contenido de la tabla desplegada en esta página"

---

## Page 7 — Room Availability
**View 1 — Search Form:**
- Start Date (date picker)
- End Date (date picker)
- Room Type (dropdown)
- "Search" button

**View 2 — Results Table:**
Same layout with form at top, results table below showing available rooms.

---

## Page 8 — Offers and Advertising
**View 1 — Category Cards:**
2 cards:
- Advertisement (image + Manage + Update buttons)
- Offers (image + Manage + Update buttons)

Note: "Las opciones 'Manage' llevan a otra página en donde se puede administrar las ofertas y la publicidad de otros sitios"

**On "Manage" → Offer/Ad List Table:**
- Full table of offers or advertisements
- Per row: Update button
- Note: "Las ofertas no tendrán la opción de cambiarlas activas desde el modal, ya que todos las alertas llevan al área de ofertas de la página cliente"

**On "Update" → Modal:**
Fields:
- ① Title
- Destiny link (URL input)
- Image preview
- "Upload new image" button
- "Save changes" button

---

## Admin UX Patterns Summary

| Action | Trigger | Result |
|---|---|---|
| Edit page content | "Edit page content" on page card | Modal with Title, Description, Fee, Image, Upload, Save |
| Add reservation | "Add reservation" button | Same Update modal |
| Edit reservation | "Update" button on row | Modal with Start date, End date, Client's name, Image |
| View reservation | "Manage" button on row | Separate full page with print option |
| Delete reservation | "Delete" button on row | (confirmation then delete) |
| Edit room type | "Update" on room card | Modal with Title, Description, Fee, Image |
| Manage rooms | "Manage" on room card | Room list table with active/inactive toggle |
| Edit offer/ad | "Update" on offer/ad row | Modal with Title, Destiny link, Image |
| View offer/ad list | "Manage" on category card | Full offer/ad table |
| Print today's status | Print button | Print current table |
