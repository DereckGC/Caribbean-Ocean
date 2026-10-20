# 04 — Frontend Pages & Mockup Inventory

## Navbar (present on all pages)
- **Links:** Offers | Fees | Online Reservation | Facilities | How to get there | About Us | Contact Us
- **Layout:** Logo left, links center, (no explicit CTA in mockup)
- **Behavior:** Transparent default → solid on scroll
- **Active:** Underlined link for current page
- **Mobile:** Hamburger menu

---

## Page 1 — Home / Index (`index.html`)
**Sections:**
1. Navbar
2. **Hero** — Full-screen photo slider with prev/next arrows (`<` `>`), description text overlay:
   > "Discover paradise where the rainforest embraces the Caribbean Sea. Located on the shores of Puerto Viejo de Talamanca, Costa Rica, the Caribbean Ocean Resort & Spa is the ideal destination for those seeking luxury, nature, and authenticity in perfect harmony."
3. **About Us** — title + 2 paragraphs (in Spanish per mockup) + main photo + 4×4 thumbnail gallery
4. Ad area (left sidebar)
5. Footer

**About Us text (from mockup):**
- "Nuestro resort ofrece una experiencia única rodeada de arrecifes de coral, playas de arena blanca y la exuberante biodiversidad del Caribe costarricense. Desde el momento en que llega, el sonido de las olas, el aroma de la selva y la calidez de nuestro equipo lo harán sentir en casa."
- "Ya sea que venga a descansar, a explorar o simplemente a desconectarse del mundo, en Caribbean Ocean Resort & Spa encontrará todo lo que necesita para vivir una estadía inolvidable."

---

## Page 2 — Offers (`offers.html`)
**Sections:**
1. Navbar (Offers link active + underlined)
2. **H1:** "Especial Offerts!" (typo from mockup — keep as-is or fix to "Special Offers!")
3. **Subtitle:** "Reservation with 20% discount"
4. **3-card carousel** with prev (`<`) / next (`>`) arrows
5. Ad area (left sidebar)
6. Footer

**Card structure:**
- Title
- Price: `$50 /mo` (placeholder — use actual room prices)
- Feature list (5-6 bullet points)
- "Button" → Book Now CTA
- Center card: dark background (featured/highlighted style)

---

## Page 3 — Fees / Rooms (`fees.html`)
*(Not explicitly mocked — referenced in navbar. Should show room types with rates.)*

**Expected content:** Room type cards (Standard, Junior, Premium) with:
- Image
- Name + description
- Daily rate
- Features list
- Book button

---

## Page 4 — Online Reservation Step 1 (`reservation.html`)
**Sections:**
1. Navbar (Online Reservation active)
2. **Booking widget card** — centered, white card:
   - H1: "Reservation"
   - Start Date input (date picker)
   - End Date input (date picker)
   - Room Type select dropdown
   - "Reserve" button (dark)
3. Ad area (left sidebar)
4. Footer

---

## Page 5 — Reservation Step 2: Results (`reservation-results.html`)
**Sections:**
1. Navbar
2. H1: "Reservation"
3. **List of available room cards** (horizontal card layout):
   - Thumbnail image (left)
   - Title (right)
   - Body description text
   - "Button" → select this room
4. Ad area (left sidebar)
5. Footer

---

## Page 6 — Reservation Step 3: Guest Form (`reservation-form.html`)
**Sections:**
1. Navbar
2. **Booking widget card:**
   - H1: "Reservation"
   - "Room" label + "Total Amount: $" display
   - Room image (large)
   - Form fields (2-column grid):
     - Name / Last Name
     - Email / Credit Card
   - Cancel + Accept buttons
3. Footer

**Success Modal (after Accept):**
- Close button (×) top right
- H2: "Reservation made successfully"
- Large checkmark circle icon
- "Thank you [username], your booking was successfully completed."
- "Reserve code: WIRNDAYU4817FD" (auto-generated)
- "Thank you for choosing us"

---

## Page 7 — No Availability (`reservation-unavailable.html`)
**Sections:**
1. Navbar
2. **H2:** "Sorry, we don't have any room available in those dates"
3. **H2:** "But we can offer other rooms on dates around"
4. List of alternative room cards (same as results, with `Title / DD,MM,YY` format)
5. **Back button** (bottom right, dark)
6. Ad area (left sidebar)
7. Footer

---

## Page 8 — Facilities (`facilities.html`)
**Sections:**
1. Navbar (Facilities active)
2. H1: "Facilities"
3. Subtitle: "Nuestro resort ofrece una gran variedad de facilidades"
4. **Alternating facility items:**
   - Odd: image left (with name below) + description text right
   - Even: description text left + image right (with name below)
5. Ad area (left sidebar)
6. Footer

---

## Page 9 — How to Get There (`how-to-get-there.html`)
**Sections:**
1. Navbar
2. 📍 **"Acces To Caribbean Ocean"** (pin icon + bold title)
3. Intro: "The Santa Teresa area is quite easy to access. You can either drive or fly to the destination."
4. **Two map panels side by side** (images of maps — drive route + fly route)
5. Ad area (left sidebar)
6. Footer

---

## Page 10 — About Us (`about-us.html`)
*(Referenced in navbar — full About Us page beyond the home section)*

---

## Page 11 — Contact Us (`contact.html`)
**Footer layout (4 columns):**
- Col 1: Logo + "Hotel Ocean Caribbean" name + social icons
- Col 2: **Menu** (italic title) — all nav links as bullet list
- Col 3: **Location** (italic title) — paragraph text + pin icon
  - "The hotel is located fifty yards from Playa Carmen in the middle of the natural beauty and excitement of Santa Teresa, Cóbano, Puntarenas."
- Col 4: **Contact Us** (italic title)
  - 📞 +1 (305) 863 8700
  - 📞 +(506) 2840 0404
  - WhatsApp +(506) 8550 0404
  - ✉ reservations@hotelcaribbean.com
  - Follow Us + social icons
  - Privacy Policy link

---

## Ad Area Specification
- **Position:** Left sidebar, absolute, all pages
- **Visible:** Desktop only (hidden mobile/tablet)
- **Label:** "Area de publicidad"
- **Border:** Left border line
- **Content:** Managed from admin module (Advertisement entity)
