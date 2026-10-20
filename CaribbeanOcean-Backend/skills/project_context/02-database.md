# 02 — Database Model

## Naming Convention
- **Style:** Real-world snake_case — simple descriptive names, NO table prefix repetition
- **Columns:** `id`, `name`, `check_in`, `daily_rate` (NOT `room_id`, `room_name`, `reservation_check_in`)
- **FK exception:** FK columns DO reference their target table: `room_type_id`, `season_id`, `updated_by`
- **Actual DB model:** Based on `Modelo_Base_de_Datos.png` — uses `limages: JSON` on ROOM (images array)

---

## ADMIN_USER
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| username | VARCHAR | Login credential |
| password_hash | VARCHAR | Encrypted |
| name | VARCHAR | Real full name |
| email | VARCHAR | |
| last_login | DATETIME | |
| active | BOOLEAN | Account enabled |

---

## ROOM_TYPE
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| name | VARCHAR | "Standard", "Junior", "Premium" |
| description | VARCHAR | Editable via admin |
| daily_rate | FLOAT | Price per night |
| image_url | VARCHAR | Type representative image |
| updated_at | DATETIME | |

---

## ROOM
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| room_type_id | INT FK → ROOM_TYPE | |
| number | INT | Physical room number |
| limages | JSON | Array of image URLs |
| active | BOOLEAN | Shown in reservations |

---

## RESERVATION
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| code | VARCHAR | Unique visible code e.g. WIRNDAYU4817FD |
| room_id | INT FK → ROOM | |
| season_id | INT FK → SEASON | |
| offer_id | INT FK → OFFER | Nullable — not all reservations use an offer |
| client_name | VARCHAR | |
| client_lastname | VARCHAR | |
| masked_card | VARCHAR | Stored masked (last 4 digits only) |
| transaction_id | VARCHAR | Payment gateway ID — NOT in DB model per image |
| created_at | DATETIME | When reservation was made |
| check_in | DATETIME | Arrival date |
| check_out | DATETIME | Departure date |
| total_amount | FLOAT | Final charged amount |

---

## OFFER
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| name | VARCHAR | Offer name |
| description | VARCHAR | Visible description |
| discount_percentage | FLOAT | e.g. 20.0 |
| start_date | DATETIME | Offer validity start |
| end_date | DATETIME | Offer validity end |
| room_type_id | INT FK → ROOM_TYPE | Applies to this room type |
| season_id | INT FK → SEASON | Applies during this season |
| active | BOOLEAN | |
| updated_at | DATETIME | |
| updated_by | INT FK → ADMIN_USER | |

---

## SEASON
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| name | VARCHAR | e.g. "High Season 2024" |
| star_date | DATETIME | Note: typo in diagram — intended `start_date` |
| end_date | DATETIME | |

---

## PAGE_CONTENT
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| key | VARCHAR | Page identifier: `home`, `about_us`, `facilities`, `how_to_get_there` |
| title | VARCHAR | |
| text_content | VARCHAR | HTML content from WYSIWYG (renamed from `content` in latest model) |
| url_content | VARCHAR | Image URL (renamed from `image_url` in latest model) |
| update_at | DATETIME | Note: column name in diagram |
| updated_by | INT FK → ADMIN_USER | |

---

## FACILITY
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| name | VARCHAR | |
| description | VARCHAR | |
| image_url | VARCHAR | |
| display_order | INT | Sort order on public page |
| updated_at | DATETIME | |
| updated_by | INT FK → ADMIN_USER | |
| active | BOOLEAN | |

---

## ADVERTISEMENT
| Column | Type | Notes |
|---|---|---|
| id | INT PK | |
| image_url | VARCHAR | Banner image |
| target_link | VARCHAR | Destination URL |
| url_image | VARCHAR | Additional image URL (per DB diagram) |
| updated_at | DATETIME | |
| updated_by | INT FK → ADMIN_USER | |
| active | BOOLEAN | |

---

## Relationships
| From | To | Type | Label |
|---|---|---|---|
| ROOM_TYPE | ROOM | 1 → many | has |
| ROOM | RESERVATION | 1 → many | generates |
| SEASON | RESERVATION | 1 → many | groups |
| SEASON | OFFER | 1 → many | frames |
| ROOM_TYPE | OFFER | 1 → many | applies to |
| OFFER | RESERVATION | 1 → many | discounts |
| ADMIN_USER | PAGE_CONTENT | 1 → many | edits |
| ADMIN_USER | FACILITY | 1 → many | edits |
| ADMIN_USER | ADVERTISEMENT | 1 → many | manages |
| ADMIN_USER | SEASON | 1 → many | manages |
| ADMIN_USER | OFFER | 1 → many | manages |

---

## Room Status Logic (computed, not stored)
- **OCCUPIED:** `check_in = today` (first day of reservation)
- **RESERVED:** `check_in > today` (future reservation exists)
- **AVAILABLE:** No reservation for today or room is inactive
