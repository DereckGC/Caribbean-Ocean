# 07 — User Stories & Acceptance Criteria

## Actors
- **Hotel client** (public site user)
- **Hotel admin** (authenticated admin module user)

---

## Section 1 — Public Site

### US-01: View hotel home page
**As** a hotel client  
**I want** to have a general overview of the hotel on the home page with a brief description of its services  
**So that** I can determine if the hotel is appropriate for me

**Acceptance criteria:**
- Home page loads with hotel hero image and description
- About Us section visible with text and gallery
- Navigation links are visible and functional

---

### US-02: View facilities
**As** a hotel client  
**I want** to see the installations and their attractions on the facilities page  
**So that** I know what I can do at the hotel

**Acceptance criteria:**
- Facilities page shows list of facilities with images and descriptions
- Alternating layout (image left/right)
- Only gallery images shown (no full facility descriptions)

---

### US-03: View rates / fees
**As** a hotel client  
**I want** to see the room rates  
**So that** I can plan my budget

**Acceptance criteria:**
- Rates page shows each room type with price, description and features
- Images of room types are displayed
- Characteristic 1, Characteristic 2 features listed per type

---

### US-04: View how to get there
**As** a hotel client  
**I want** to see how to get to the hotel with well-known reference points  
**So that** I can plan my itinerary

**Acceptance criteria:**
- Page shows location description
- Google Map is included
- Drive and fly access options shown

---

### US-05: View About Us + gallery
**As** a hotel client  
**I want** to see photos of the main facilities and surrounding areas in the About Us gallery  
**So that** I can decide if the hotel fits my needs

**Acceptance criteria:**
- Gallery shows images in a grid (4×4 as per mockup)
- Main large image displayed alongside thumbnails

---

### US-06: Check room availability
**As** a hotel client  
**I want** to check room availability for a specific date range  
**So that** I can plan my reservation

**Acceptance criteria:**
- Search form has start date, end date, room type
- Start date must be before end date
- Available rooms shown in results
- If no availability → show "Sorry" message + alternative dates

---

### US-07: Make online reservation
**As** a hotel client  
**I want** to reserve a room specifying dates and room type and receive immediate feedback  
**So that** I can secure my stay

**Acceptance criteria:**
- All fields are mandatory (Name, Last Name, Email, Credit Card)
- On success: show reservation code + thank you message
- Email confirmation sent to client's address
- Simulated payment (PayPal-style, certified)
- On no availability: redirect to alternatives page

---

### US-08: View contact information
**As** a hotel client  
**I want** to see the hotel's contact information (phone numbers, email)  
**So that** I can resolve doubts

**Acceptance criteria:**
- Phone numbers displayed
- Email address displayed
- Location/address shown

---

## Section 2 — Admin: Manage Pages

### US-09: Admin login
**As** an admin  
**I want** to enter my credentials on a login page  
**So that** I can access all admin options

**Acceptance criteria:**
- Login form requires username and password
- Only authenticated admins can access the admin module
- Incorrect credentials show error
- Redirects to admin dashboard on success
- Logout redirects to login page

---

### US-10: Modify page text content
**As** an admin  
**I want** to change the text of a page  
**So that** I can renew the site's appearance periodically

**Acceptance criteria:**
- Admin can select which page to modify (Home, About Us, Facilities, How to get there)
- Text field is editable
- Changes are saved and reflected on public site
- Entry to public site confirms text was modified

---

### US-11: Modify page image
**As** an admin  
**I want** to update the image shown to clients  
**So that** I can keep the site visually fresh

**Acceptance criteria:**
- Current image is displayed
- Upload new image button functional
- Image format validated (JPG/GIF/PNG)
- No dimension validation required
- Not mandatory to upload image
- On save: redirect to admin home showing success or error message
- Change persists on public site

---

## Section 3 — Admin: Manage Rooms

### US-12: Load room description and rate
**As** an admin  
**I want** to load the description, rate and photo of a Standard or Junior room  
**So that** clients can see current information

**Acceptance criteria:**
- Admin can edit room type description
- Admin can set daily rate
- Admin can upload a new image

---

### US-13: View room info
**As** an admin  
**I want** to verify current room information  
**So that** I can confirm it's up to date

**Acceptance criteria:**
- Room list shows all rooms per type
- Each room shows its current status

---

### US-14: Activate/deactivate rooms
**As** an admin  
**I want** to change the active status of a room (active or inactive)  
**So that** I can manage availability

**Acceptance criteria:**
- Toggle per room between Active and Inactive
- Changes reflected immediately in availability

---

## Section 4 — Admin: Reservations

### US-15: View reservation list
**As** an admin  
**I want** to see a list of all reservations  
**So that** I can manage them

**Acceptance criteria:**
- Table shows all reservations
- Manage / Update / Delete actions per row

---

### US-16: View reservation detail + print
**As** an admin  
**I want** to view full reservation details and print  
**So that** I can have physical records

**Acceptance criteria:**
- Manage button opens full reservation detail page
- Print button prints the reservation info
- Return button goes back to reservation list

---

### US-17: Update reservation
**As** an admin  
**I want** to update reservation data  
**So that** I can correct errors

**Acceptance criteria:**
- Update button opens modal with editable fields
- Changes saved successfully

---

### US-18: Delete reservation
**As** an admin  
**I want** to delete a reservation  
**So that** I can remove cancelled bookings

**Acceptance criteria:**
- Delete button removes the reservation
- (Confirmation dialog implied)

---

### US-19: Add reservation manually
**As** an admin  
**I want** to add a reservation manually  
**So that** I can register phone or in-person bookings

**Acceptance criteria:**
- "Add reservation" button opens same Update modal
- All reservation fields available

---

## Section 5 — Admin: Hotel Status

### US-20: View today's hotel status
**As** an admin  
**I want** to see the status of all rooms today  
**So that** I can consult daily availability

**Acceptance criteria:**
- Table shows all rooms with their status (AVAILABLE / OCCUPIED / RESERVED)
- Status computed from reservations (not stored)
- Print button prints the entire table

---

## Section 6 — Admin: Room Availability

### US-21: Consult room availability by date range
**As** an admin  
**I want** to determine the number of available rooms in a date range  
**So that** I can advise clients

**Acceptance criteria:**
- Search form: start date, end date, room type
- Start date must be before end date
- Results table shows available rooms

---

## Section 7 — Admin: Offers & Advertising

### US-22: Manage offers
**As** an admin  
**I want** to manage special offers  
**So that** I can attract more clients

**Acceptance criteria:**
- Offer list with Update button per offer
- Update modal: Title, Destiny link, Image
- Offers visible in client-facing offers page

---

### US-23: Manage advertisements
**As** an admin  
**I want** to manage the publicity/advertisement banner  
**So that** I can show relevant promotions on every page

**Acceptance criteria:**
- Ad image uploadable
- Destination link configurable
- Ad area visible on all public pages

---

## Section 8 — Admin: Statistics

### US-24: View monthly statistics dashboard
**As** an admin  
**I want** to see a dashboard with hotel statistics on the admin home page  
**So that** I can track performance

**Acceptance criteria:**
- Dashboard shows: total reservations, premium reserved, standard reserved, junior reserved, total revenue
- Displayed with colored progress bars per category
- Data reflects current month

---

## General Acceptance Notes (from XLS file)
- Reservation code format: alphanumeric string (e.g. WIRNDAYU4817FD)
- Room status rules:
  - OCCUPIED = first day of reservation (check_in = today)
  - RESERVED = future reservation exists
  - AVAILABLE = no reservation or inactive room
- Simulated payment (PayPal-style)
- Image uploads: JPG/GIF/PNG format, no dimension validation
- All admin operations require authentication
