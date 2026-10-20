USE IF7100_CaribbeanOceanDB;
GO

-- =========================================
-- ADMIN_USER
-- =========================================
INSERT INTO ADMIN_USER (username, password_hash, name, email, last_login, active)
VALUES
('admin1', 'hashed_password_123', 'John Admin', 'john.admin@example.com', SYSDATETIME(), 1),
('admin2', 'hashed_password_456', 'Mary Manager', 'mary.manager@example.com', NULL, 1),
('admin3', 'hashed_password_789', 'Carlos Editor', 'carlos.editor@example.com', NULL, 1),
('admin4', 'hashed_password_abc', 'Lucia Soporte', 'lucia.soporte@example.com', SYSDATETIME(), 1),
('admin5', 'hashed_password_xyz', 'Roberto Auditor', 'roberto.auditor@example.com', NULL, 1);

-- =========================================
-- ROOM_TYPE
-- =========================================
INSERT INTO ROOM_TYPE (name, description, daily_rate, image_url, features, updated_at, active)
VALUES
(
    'Junior Room', 
    'Cozy room ideal for one or two guests.', 
    80.00, 
    'https://res.cloudinary.com/dm9dniopv/image/upload/v1776741483/images_iys2pz.jpg', 
	'["WiFi","Frigde","TV"]',
    SYSDATETIME(), 1
),
(
    'Standard Room', 
    'Comfortable room with basic amenities.', 
    100.00, 
    'https://res.cloudinary.com/dm9dniopv/image/upload/v1776743282/Dorado-Beach-a-Ritz-Carlton-Reserve-3_bjabe9.jpg', 
	'["WiFi","Ocean View","Bar"]',
    SYSDATETIME(), 1
),
(
    'Deluxe Room', 
    'Spacious room with premium features and balcony.', 
    145.00, 
    'https://res.cloudinary.com/dm9dniopv/image/upload/v1776743340/1_oyj882.jpg', 
	'["Queen size bed","Bath","OceanView"]',
    SYSDATETIME(), 1
),
(
    'Family Suite', 
    'Large suite for families with multiple beds.', 
    210.00, 
    'https://res.cloudinary.com/dm9dniopv/image/upload/v1776743408/images_2_zdjlld.jpg',
	'["Queen size bed","Bath","OceanView"]',
    SYSDATETIME(), 1
),
(
    'Ocean View Suite', 
    'Luxury suite with a direct ocean view.', 
    320.00, 
    'https://res.cloudinary.com/dm9dniopv/image/upload/v1776743464/suite_1_-exclusive_xwg01n.jpg', 
	'["Queen size bed","Bath","OceanView"]',
    SYSDATETIME(), 1
);

-- =========================================
-- SEASON
-- =========================================
INSERT INTO SEASON (name, start_date, end_date, discount_amount)
VALUES
('Low Season 2026', '2026-01-10', '2026-04-30', -5.0),
('Mid Season 2026', '2026-05-01', '2026-08-31', -3.0),
('High Season 2026', '2026-09-01', '2026-12-20', 10.0),
('Holiday Season 2026', '2026-12-21', '2027-01-05', 15.0),
('Spring Break 2026', '2026-03-01', '2026-03-15', 20.0);

-- =========================================
-- ROOM
-- =========================================
INSERT INTO ROOM (number, room_type_id, active)
VALUES
(101, 1, 1),
(201, 2, 1),
(301, 3, 1),
(401, 4, 1),
(501, 5, 1);

-- =========================================
-- OFFER
-- =========================================
INSERT INTO OFFER (name, description, discount_percentage, start_time, end_time, room_type_id, updated_at, updated_by, active)
VALUES
(
    'Spring Break Junior Special', 
    'Take a break in our Junior Rooms with a special discount during the spring season.', 
    15.00, '2026-03-01', '2026-05-31', 1, SYSDATETIME(), 1, 1
),
(
    'Early Summer Standard Deal', 
    'Book your standard room early and enjoy a significant price drop for May.', 
    12.00, '2026-05-01', '2026-06-30', 2, SYSDATETIME(), 2, 1
),
(
    'Deluxe Autumn Retreat', 
    'Experience luxury for less. 20% off on all Deluxe Rooms during the high season peak.', 
    20.00, '2026-04-01', '2026-08-15', 3, SYSDATETIME(), 1, 1
),
(
    'Family Suite Flash Sale', 
    'The perfect family getaway. Save big on our spacious Family Suites this July.', 
    25.00, '2026-05-10', '2026-07-31', 4, SYSDATETIME(), 3, 1
),
(
    'Ocean View Serenity', 
    'Wake up to the sound of the waves. Exclusive discount for Ocean View Suites in low season.', 
    10.00, '2026-02-01', '2026-05-20', 5, SYSDATETIME(), 2, 1
),
(
    'Romantic Junior Getaway', 
    'A cozy escape for two. Enjoy special rates on Junior Rooms for the month of February.', 
    18.50, '2026-06-01', '2026-07-15', 1, SYSDATETIME(), 4, 1
),
(
    'End of Year Deluxe Promo', 
    'Say goodbye to 2026 in style with our year-end deluxe room offer.', 
    14.00, '2026-09-01', '2026-12-31', 3, SYSDATETIME(), 5, 1
);
GO

-- =========================================
-- ADVERTISEMENT
-- =========================================
INSERT INTO ADVERTISEMENT (image_url, target_link, updated_at, updated_by, active)
VALUES
('/images/ads/summer-sale.jpg', 'https://example.com/offers/summer-sale', SYSDATETIME(), 1, 1),
('/images/ads/family-special.jpg', 'https://example.com/offers/family-special', SYSDATETIME(), 2, 1),
('/images/ads/ocean-view.jpg', 'https://example.com/rooms/ocean-view-suite', SYSDATETIME(), 3, 0),
('/images/ads/spa-relax.jpg', 'https://example.com/facilities/spa', SYSDATETIME(), 4, 1),
('/images/ads/early-bird.jpg', 'https://example.com/reservations', SYSDATETIME(), 1, 1);

-- =========================================
-- PAGE_CONTENT
-- =========================================
INSERT INTO PAGE_CONTENT ([key], title, subTitle, [description], url_content, updated_at, updated_by, active)
VALUES
('room_type', 'ROOMS', 'Take a look of our rooms', '', '[]', SYSDATETIME(), 1, 1),
(
    'gallery', 
    'Nuestra Galería', 
    'Some of our facilities', 
    'Explora las increíbles vistas y servicios de Caribbean Ocean.', 
    '[{"id": 1, "url": "https://static.hosteltur.com/app/public/uploads/img/articles/2014/09/01/S_5b14f78cdbe07_shutterstock_hotelcaribe.jpg", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."},
      {"id": 2, "url": "https://i.pinimg.com/736x/39/fa/28/39fa28f519c21814324d4053271ad668.jpg", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."},
      {"id": 3, "url": "https://www.thehotelguru.com/_images/d7/a5/d7a52778f45626be485af17b0def2af1/s1654x900.jpg", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."},
      {"id": 4, "url": "https://digital.ihg.com/is/image/ihg/independent-puerto-plata-8182279046-16x5-1?ts=1767990373913&dpr=off", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."},
      {"id": 5, "url": "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/0e/a7/fc/e0/sunsol-isla-caribe.jpg?w=700&h=-1&s=1", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."},
      {"id": 6, "url": "https://i0.wp.com/foodandpleasure.com/wp-content/uploads/2021/04/hoteles-en-playa-del-carmen-magicblue.jpeg?fit=1280%2C855&ssl=1", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."},
      {"id": 7, "url": "https://q-xx.bstatic.com/xdata/images/hotel/max500/543592315.jpg?k=0e14f88a5dcf6a1ecef1cae5e0bd4ff67dc7ea356bceed899176be4fc32ce7cc&o=pg", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."},
      {"id": 8, "url": "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/0f/00/57/e2/pc-bungalow-suite.jpg?w=400&h=-1&s=1", "alt": "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, quod."}]', 
    SYSDATETIME(), 
    1, 
    1
),
('about_us', 'ABOUT US', 'Resort & Spa', 'The Caribbean Ocean Resort & Spa was born from the dream of creating a space where visitors could experience the essence of the Costa Rican Caribbean without sacrificing comfort and luxury. Founded in 2008 by a Costa Rican family passionate about sustainable tourism, we have grown to become one of the most renowned resorts in the Caribbean region. We offer 48 rooms distributed among beachfront villas and bungalows, a world-class spa, three themed restaurants, infinity pools, and direct access to a private beach. All our staff are local, trained to the highest standards of hospitality, and committed to providing personalized attention to each guest. We deeply believe in responsible tourism. Therefore, more than 80% of the products we use in our kitchen and spa come from local and organic suppliers, and we hold ICT Sustainable Tourism certification (Level 4).', '[{"url": "about.jpg"}]', SYSDATETIME(), 1, 1),
('facilities', 'Our Facilities', 'Explore our modern facilities.', '', '[{"url": "fac.jpg"}]', SYSDATETIME(), 1, 1),
('contact_info', 'Get in Touch', 'We are here to help', 'Visit us at our main lobby.', '{"map": "link"}', SYSDATETIME(), 4, 1),
('policy_terms', 'Policies', 'Terms and Conditions', 'Check our cancellation policies.', '{"pdf": "pol.pdf"}', SYSDATETIME(), 5, 1),
('hero', 'Caribbean Ocean', 'Where the jungle meets the sea', 
'Discover paradise where the rainforest embraces the Caribbean Sea. Located on the shores of Puerto Viejo de Talamanca, Costa Rica, the Caribbean Ocean Resort & Spa is the ideal destination for those seeking luxury, nature, and authenticity in perfect harmony.', 
'[
	{"id": 0, "url": "https://res.cloudinary.com/dm9dniopv/image/upload/v1776715333/Hero_Beach_kjtqp7.webp", "alt": "Piscina"},
	{"id": 1, "url": "https://res.cloudinary.com/dm9dniopv/image/upload/v1776715333/Hero_Hotel_View_xvru1k.jpg", "alt": "Hero"},
	{"id": 2, "url": "https://res.cloudinary.com/dm9dniopv/image/upload/v1776715333/Hero_Swiming_ventuy.jpg", "alt": "Hero"},
	{"id": 3, "url": "https://res.cloudinary.com/dm9dniopv/image/upload/v1776715333/Hero_Pool_stesrf.avif", "alt": "Hero"}
]', SYSDATETIME(), 5, 1),
('locations', 'ARRIVES AT CARIBEANOCEAN', 'Embark on a seamless journey to paradise', 'The hotel is located fifty yards from Playa Cocles in the middle of the natural beauty of Puerto Viejo de Talamanca, Limón, Costa Rica.', 
'[
{"id": 0, "url": "https://maps.app.goo.gl/f4LakXLQziZfjidq8"},
{"id": 1, "url": "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1966.716568333218!2d-82.72704476032715!3d9.643981399999994!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8fa64533a1c941d7%3A0x4c26f09334cfd1b2!2sPlaya%20Chasma!5e0!3m2!1ses-419!2scr!4v1774936213017!5m2!1ses-419!2scr"}
]', SYSDATETIME(), 1, 1);

-- =========================================
-- HOTEL_CONTACT
-- =========================================
INSERT INTO HOTEL_CONTACT ([type], contact, active)
VALUES
('calls', '+506 8888-0001', 1),
('calls', '+506 2222-1010', 1),
('email', 'info@caribbean.cr', 1),
('email', 'reservas@caribbean.cr', 1),
('whatsapp', '+506 8888-0002', 1),
('facebook', 'https://www.facebook.com/caribbeanoceantours', 1),
('instagram', 'https://www.instagram.com/caribbean_ocean_tours/', 1),
('tiktok', 'https://www.tiktok.com/@whattodoincuracao', 1);

-- =========================================
-- FACILITY
-- =========================================
INSERT INTO FACILITY (name, description, features, image_url, display_order, updated_at, updated_by, active)
VALUES
(
    'Swimming Pool', 
    'Outdoor pool available.', 
    '["Relax","Ocean View","Infinity pool"]', 
    'https://res.cloudinary.com/dwolx8nhl/image/upload/v1776714075/Hero_Pool_stxiko.avif', 
    1, 
    SYSDATETIME(), 
    1, 
    1
),
(
    'Spa', 
    'Wellness and massage.', 
    '["Relax","Ocean View","Infinity pool"]', 
    'https://res.cloudinary.com/dwolx8nhl/image/upload/v1776714075/Hero_Pool_stxiko.avif', 
    2, 
    SYSDATETIME(), 
    2, 
    1
),
(
    'Restaurant', 
    'Local and international.', 
    '["Relax","Ocean View","Infinity pool"]', 
    'https://res.cloudinary.com/dwolx8nhl/image/upload/v1776714075/Hero_Pool_stxiko.avif', 
    3, 
    SYSDATETIME(), 
    2, 
    1
),
(
    'Gym', 
    'Fitness area.', 
    '["Relax","Ocean View","Infinity pool"]', 
    'https://res.cloudinary.com/dwolx8nhl/image/upload/v1776714075/Hero_Pool_stxiko.avif', 
    4, 
    SYSDATETIME(), 
    3, 
    1
),
(
    'Surf Lessons', 
    'Guided surf lessons.', 
    '["Relax","Ocean View","Infinity pool"]', 
    'https://res.cloudinary.com/dwolx8nhl/image/upload/v1776714075/Hero_Pool_stxiko.avif', 
    5, 
    SYSDATETIME(), 
    1, 
    0
);

-- =========================================
-- RESERVATION
-- =========================================
INSERT INTO RESERVATION (
    code, 
    room_id, 
    client_name, 
    client_lastname, 
    masked_card, 
    email, 
    created_at, 
    check_in, 
    check_out, 
    total_amount, 
    active
)
VALUES
('RSV-1001', 1,  'Ian', 'Ramirez', '**** 1234', 'ian.ramirez@example.com', SYSDATETIME(), '2026-02-10', '2026-02-14', 320.00, 1),
('RSV-1002', 2,  'Laura', 'Gomez', '**** 5678', 'laura.gomez@example.com', SYSDATETIME(), '2026-06-05', '2026-06-10', 500.00, 1),
('RSV-1003', 3,  'Carlos', 'Mendez', '**** 4321', 'carlos.mendez@example.com', SYSDATETIME(), '2026-07-20', '2026-07-25', 725.00, 1),
('RSV-1004', 4,  'Sofia', 'Lopez', '**** 8765', 'sofia.lopez@example.com', SYSDATETIME(), '2026-09-12', '2026-09-16', 840.00, 1),
('RSV-1005', 5,  'Daniel', 'Castro', '**** 2468', 'daniel.castro@example.com', SYSDATETIME(), '2026-12-23', '2026-12-28', 1600.00, 1);


-- =========================================
-- ACTION CODES
-- =========================================

INSERT INTO [dbo].[ACTION_CODE] (code, name, category, description) VALUES
 
-- Authentication
(101, 'LOGIN',    'Authentication', 'User logged into the system'),
(102, 'LOGOUT',   'Authentication', 'User logged out of the system'),
(103, 'REGISTER', 'Authentication', 'User register into the system'),
 
-- Room Types
(201, 'ROOM_TYPE_VIEW',   'Rooms', 'Viewed room types'),
(202, 'ROOM_TYPE_CREATE', 'Rooms', 'Created a room type'),
(203, 'ROOM_TYPE_UPDATE', 'Rooms', 'Updated a room type'),
(204, 'ROOM_TYPE_DELETE', 'Rooms', 'Deleted a room type'),
 
-- Rooms
(211, 'ROOM_VIEW',   'Rooms', 'Viewed rooms'),
(212, 'ROOM_CREATE', 'Rooms', 'Created a room'),
(213, 'ROOM_UPDATE', 'Rooms', 'Updated a room'),
(214, 'ROOM_DELETE', 'Rooms', 'Deleted a room'),
(215, 'ROOM_STATUS', 'Rooms', 'Generated status of rooms'),
 
-- Gallery
(301, 'GALLERY_VIEW',   'Gallery', 'Viewed gallery images'),
(302, 'GALLERY_CREATE', 'Gallery', 'Added a gallery image'),
(303, 'GALLERY_UPDATE', 'Gallery', 'Updated a gallery image'),
(304, 'GALLERY_DELETE', 'Gallery', 'Deleted a gallery image'),
 
-- Reservations
(401, 'RESERVATION_VIEW',   'Reservations', 'Viewed reservations'),
(402, 'RESERVATION_CREATE', 'Reservations', 'Created a reservation'),
(403, 'RESERVATION_UPDATE', 'Reservations', 'Updated a reservation'),
(404, 'RESERVATION_DELETE', 'Reservations', 'Deleted a reservation'),
 
-- Status
(501, 'STATUS_VIEW', 'Status', 'Viewed room status for today'),
(502, 'STATUS_PDF',  'Status', 'Generated room status PDF'),
 
-- Offers
(601, 'OFFER_VIEW',   'Offers', 'Viewed offers'),
(602, 'OFFER_CREATE', 'Offers', 'Created an offer'),
(603, 'OFFER_UPDATE', 'Offers', 'Updated an offer'),
(604, 'OFFER_DELETE', 'Offers', 'Deleted an offer'),
 
-- About Us
(701, 'ABOUT_VIEW',   'About Us', 'User viewed about us'),
(702, 'ABOUT_UPDATE', 'About Us', 'User updated about us'),
 
-- Advertising
(801, 'ADD_VIEW',   'Advertising', 'Viewed advertisements'),
(802, 'ADD_CREATE', 'Advertising', 'Created an advertisement'),
(803, 'ADD_UPDATE', 'Advertising', 'Updated an advertisement'),
(804, 'ADD_DELETE', 'Advertising', 'Deleted an advertisement'),
 
-- Facility
(901, 'FACILITY_VIEW',   'Facility', 'Viewed facilities page'),
(902, 'FACILITY_CREATE', 'Facility', 'Created a facility'),
(903, 'FACILITY_UPDATE', 'Facility', 'Updated a facility'),
(904, 'FACILITY_DELETE', 'Facility', 'Deleted a facility'),
 
-- Hero
(1001, 'HERO_VIEW',         'Hero', 'Viewed hero section'),
(1002, 'HERO_TEXT_UPDATE',  'Hero', 'Updated hero text'),
(1003, 'HERO_IMAGE_CREATE', 'Hero', 'Added a hero image'),
(1004, 'HERO_IMAGE_UPDATE', 'Hero', 'Updated a hero image'),
(1005, 'HERO_IMAGE_DELETE', 'Hero', 'Deleted a hero image'),
 
-- Locations
(1101, 'LOCATION_VIEW',   'Locations', 'Viewed locations page'),
(1102, 'LOCATION_UPDATE', 'Locations', 'Updated locations page'),
 
-- Seasons
(1201, 'SEASON_VIEW',   'Seasons', 'Viewed seasons'),
(1202, 'SEASON_CREATE', 'Seasons', 'Created a season'),
(1203, 'SEASON_UPDATE', 'Seasons', 'Updated a season'),
(1204, 'SEASON_DELETE', 'Seasons', 'Deleted a season'),
 
-- Hotel Contact
(1301, 'HOTEL_CONTACT_VIEW',   'Hotel Contact', 'Viewed hotel contacts'),
(1302, 'HOTEL_CONTACT_CREATE', 'Hotel Contact', 'Created a hotel contact'),
(1303, 'HOTEL_CONTACT_UPDATE', 'Hotel Contact', 'Updated a hotel contact'),
(1304, 'HOTEL_CONTACT_DELETE', 'Hotel Contact', 'Deleted a hotel contact')
GO
 