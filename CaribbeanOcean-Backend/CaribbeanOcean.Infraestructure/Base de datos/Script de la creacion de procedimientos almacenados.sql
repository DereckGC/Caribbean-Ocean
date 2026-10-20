USE IF7100_CaribbeanOceanDB;
GO


-- =============================================
-- sp_GetAdminUserByUsername
-- =============================================
IF OBJECT_ID('dbo.sp_GetAdminUserByUsername', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAdminUserByUsername;
GO
CREATE PROCEDURE dbo.sp_GetAdminUserByUsername
    @username NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        username,
        password_hash,
        name,
        email,
        last_login
    FROM dbo.ADMIN_USER
    WHERE username = @username;
END;
GO

-- =============================================
-- sp_CreateAdminUser
-- =============================================
IF OBJECT_ID('dbo.sp_CreateAdminUser', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CreateAdminUser;
GO
CREATE PROCEDURE dbo.sp_CreateAdminUser
    @username      NVARCHAR(50),
    @password_hash NVARCHAR(255),
    @name          NVARCHAR(255),
    @email         NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.ADMIN_USER (username, password_hash, name, email, last_login)
    VALUES (@username, @password_hash, @name, @email, NULL);

    SELECT SCOPE_IDENTITY();
END;
GO

-- =============================================
-- sp_UpdateAdminUserLastLogin
-- =============================================
IF OBJECT_ID('dbo.sp_UpdateAdminUserLastLogin', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateAdminUserLastLogin;
GO
CREATE PROCEDURE dbo.sp_UpdateAdminUserLastLogin
    @username  NVARCHAR(50),
    @last_login DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.ADMIN_USER
    SET last_login = @last_login
    WHERE username = @username;
END;
GO


/*sp_GetReservationById*/


CREATE OR ALTER PROCEDURE sp_GetReservationById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
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
    FROM RESERVATION
    WHERE id = @Id
      AND active = 1;
END;
GO


/*sp_GetAllReservations*/

CREATE OR ALTER PROCEDURE sp_GetAllReservations
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
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
    FROM RESERVATION
    WHERE active = 1
    ORDER BY id DESC;
END;
GO


/*sp_CreateReservation*/

CREATE OR ALTER PROCEDURE sp_CreateReservation
    @code NVARCHAR(50),
    @room_id INT,
    @client_name NVARCHAR(100),
    @client_lastname NVARCHAR(100),
    @email NVARCHAR(255),
    @check_in DATETIME2,
    @check_out DATETIME2,
    @masked_card NVARCHAR(25),
    @total_amount DECIMAL(10,2),
    @pricing_used_offer BIT,
    @selected_offer_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @check_out <= @check_in
    BEGIN
        RAISERROR('Check-out must be later than check-in.', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM ROOM
        WHERE id = @room_id
          AND active = 1
    )
    BEGIN
        RAISERROR('The selected room does not exist or is inactive.', 16, 1);
        RETURN;
    END;

   

    IF EXISTS (
        SELECT 1
        FROM RESERVATION
        WHERE room_id = @room_id
          AND active = 1
          AND @check_in < check_out
          AND @check_out > check_in
    )
    BEGIN
        RAISERROR('The room is already reserved for the selected dates.', 16, 1);
        RETURN;
    END;

    INSERT INTO RESERVATION
    (
        code,
        room_id,
        client_name,
        client_lastname,
        email,
        created_at,
        check_in,
        check_out,
        masked_card,
        pricing_used_offer,
        selected_offer_id,
        total_amount,
        active
    )
    VALUES
    (
        @code,
        @room_id,
        @client_name,
        @client_lastname,
        @email,
        SYSDATETIME(),
        @check_in,
        @check_out,
        @masked_card,
        @pricing_used_offer,
        @selected_offer_id,
        @total_amount,
        1
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END;
GO

/*sp_UpdateReservation*/
CREATE OR ALTER PROCEDURE sp_UpdateReservation
    @id INT,
    @code NVARCHAR(50),
    @room_id INT,
    @client_name NVARCHAR(100),
    @client_lastname NVARCHAR(100),
    
    @email NVARCHAR(255),
    @check_in DATETIME2,
    @check_out DATETIME2,
    @total_amount DECIMAL(10,2)
AS
BEGIN

    IF @check_out <= @check_in
    BEGIN
        RAISERROR('Check-out must be later than check-in.', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM RESERVATION
        WHERE id = @id
          AND active = 1
    )
    BEGIN
        RAISERROR('Reservation not found or inactive.', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM ROOM
        WHERE id = @room_id
          AND active = 1
    )
    BEGIN
        RAISERROR('The selected room does not exist or is inactive.', 16, 1);
        RETURN;
    END;


    IF EXISTS (
        SELECT 1
        FROM RESERVATION
        WHERE room_id = @room_id
          AND active = 1
          AND id <> @id
          AND @check_in < check_out
          AND @check_out > check_in
    )
    BEGIN
        RAISERROR('The room is already reserved for the selected dates.', 16, 1);
        RETURN;
    END;

    UPDATE RESERVATION
    SET
        code = @code,
        room_id = @room_id,
        client_name = @client_name,
        client_lastname = @client_lastname,
         
        email = @email,
        check_in = @check_in,
        check_out = @check_out,
        total_amount = @total_amount
    WHERE id = @id
      AND active = 1;
END;
GO


/*sp_HasAvailableRoomByType
ESTE ES EL METODO QUE SE LLAMA EN EL FRONTEND PARA CONOCER SI EXISTE ALGUNA HABITACION DISPONIBLE DEL TIPO QUE EL USUARIO QUIERE

*/

CREATE OR ALTER PROCEDURE sp_HasAvailableRoomByType
    @room_type_id INT,
    @check_in DATETIME2,
    @check_out DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    IF @check_out <= @check_in
    BEGIN
        RAISERROR('Check-out must be later than check-in.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM ROOM rm
        WHERE rm.room_type_id = @room_type_id
          AND rm.active = 1
          AND NOT EXISTS (
              SELECT 1
              FROM RESERVATION r
              WHERE r.room_id = rm.id
                AND r.active = 1
                AND @check_in < r.check_out
                AND @check_out > r.check_in
          )
    )
        SELECT CAST(1 AS BIT) AS has_available_room;
    ELSE
        SELECT CAST(0 AS BIT) AS has_available_room;
END;
GO

/*sp_GetAvailableRoomIdByTyp
Este es el procedimiento que se usa al final para de verdad crear la reservacion*/

CREATE OR ALTER PROCEDURE sp_GetAvailableRoomIdByType
    @room_type_id INT,
    @check_in DATETIME2,
    @check_out DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    IF @check_out <= @check_in
    BEGIN
        RAISERROR('Check-out must be later than check-in.', 16, 1);
        RETURN;
    END;

    SELECT TOP 1
        rm.id AS room_id
    FROM ROOM rm
    WHERE rm.room_type_id = @room_type_id
      AND rm.active = 1
      AND NOT EXISTS (
          SELECT 1
          FROM RESERVATION r
          WHERE r.room_id = rm.id
            AND r.active = 1
            AND @check_in < r.check_out
            AND @check_out > r.check_in
      )
    ORDER BY rm.id;
END;
GO

/*Stored procedure for reservation details*/
CREATE OR ALTER PROCEDURE sp_GetReservationDetailsByReservationId
    @reservation_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rm.room_type_id,
        rt.name AS room_type_name
    FROM RESERVATION r
    INNER JOIN ROOM rm ON r.room_id = rm.id
    INNER JOIN ROOM_TYPE rt ON rm.room_type_id = rt.id
    WHERE r.id = @reservation_id
      AND r.active = 1;
END;
GO

/*This returns the ROOM_TYPE data only if there is at least one available room of that type.*/
CREATE OR ALTER PROCEDURE sp_GetAvailableRoomTypeById
    @room_type_id INT,
    @check_in DATETIME2,
    @check_out DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    IF @check_out <= @check_in
    BEGIN
        RAISERROR('Check-out must be later than check-in.', 16, 1);
        RETURN;
    END;

    SELECT TOP 1
        rt.id,
        rt.name,
        rt.description,
        rt.daily_rate,
        rt.image_url,
        rt.updated_at,
        rt.features,
        rt.active
    FROM ROOM_TYPE rt
    WHERE rt.id = @room_type_id
      AND rt.active = 1
      AND EXISTS
      (
          SELECT 1
          FROM ROOM rm
          WHERE rm.room_type_id = rt.id
            AND rm.active = 1
            AND NOT EXISTS
            (
                SELECT 1
                FROM RESERVATION r
                WHERE r.room_id = rm.id
                  AND r.active = 1
                  AND @check_in < r.check_out
                  AND @check_out > r.check_in
            )
      );
END;
GO

/*This returns all room types that have at least one available room.*/
CREATE OR ALTER PROCEDURE sp_GetAllAvailableRoomTypes
    @check_in DATETIME2,
    @check_out DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    IF @check_out <= @check_in
    BEGIN
        RAISERROR('Check-out must be later than check-in.', 16, 1);
        RETURN;
    END;

    SELECT
        rt.id,
        rt.name,
        rt.description,
        rt.daily_rate,
        rt.image_url,
        rt.updated_at,
		rt.features,
        rt.active
    FROM ROOM_TYPE rt
    WHERE rt.active = 1
      AND EXISTS
      (
          SELECT 1
          FROM ROOM rm
          WHERE rm.room_type_id = rt.id
            AND rm.active = 1
            AND NOT EXISTS
            (
                SELECT 1
                FROM RESERVATION r
                WHERE r.room_id = rm.id
                  AND r.active = 1
                  AND @check_in < r.check_out
                  AND @check_out > r.check_in
            )
      )
    ORDER BY rt.id;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetRoomTypeById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        name,
        description,
        daily_rate,
        image_url,
        updated_at,
		features,
        active
    FROM ROOM_TYPE
    WHERE id = @id
      AND active = 1;
END;
GO

/*This is the SQL version that returns the count:*/
CREATE OR ALTER PROCEDURE sp_GetAllAvailableRoomTypesWithCount
    @check_in DATETIME2,
    @check_out DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    IF @check_out <= @check_in
    BEGIN
        RAISERROR('Check-out must be later than check-in.', 16, 1);
        RETURN;
    END;

    SELECT
        rt.id,
        rt.name,
        rt.description,
        rt.daily_rate,
        rt.image_url,
        rt.updated_at,
		rt.features,
        rt.active,
        COUNT(rm.id) AS available_rooms
    FROM ROOM_TYPE rt
    INNER JOIN ROOM rm
        ON rm.room_type_id = rt.id
       AND rm.active = 1
    WHERE rt.active = 1
      AND NOT EXISTS
      (
          SELECT 1
          FROM RESERVATION r
          WHERE r.room_id = rm.id
            AND r.active = 1
            AND @check_in < r.check_out
            AND @check_out > r.check_in
      )
    GROUP BY
        rt.id,
        rt.name,
        rt.description,
        rt.daily_rate,
        rt.image_url,
        rt.updated_at,
		rt.features,
        rt.active
    ORDER BY rt.id;
END;
GO



CREATE OR ALTER PROCEDURE sp_DeleteReservation
    @id INT
AS
BEGIN
    

    IF NOT EXISTS (
        SELECT 1
        FROM RESERVATION
        WHERE id = @id
          AND active = 1
    )
    BEGIN
        RAISERROR('Reservation not found or already inactive.', 16, 1);
        RETURN;
    END;

    UPDATE RESERVATION
    SET active = 0
    WHERE id = @id
      AND active = 1;
END;
GO

-- =============================================
-- sp_GetPageContentByKey
-- =============================================
IF OBJECT_ID('dbo.sp_GetPageContentByKey', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetPageContentByKey;
GO
CREATE PROCEDURE dbo.sp_GetPageContentByKey
    @key NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        [key],
        title,
        subtitle,
        [description],
        url_content,
        updated_at,
        updated_by,
        active
    FROM dbo.PAGE_CONTENT
    WHERE active = 1
      AND [key]  = @key;
END;
GO

-- =============================================
-- sp_GetAllHotelContacts
-- =============================================
IF OBJECT_ID('dbo.sp_GetAllHotelContacts', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllHotelContacts;
GO
CREATE PROCEDURE dbo.sp_GetAllHotelContacts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        [type],
        contact
    FROM dbo.HOTEL_CONTACT
    WHERE active = 1;
END;
GO

-- =============================================
-- sp_GetAllRoomTypes
-- =============================================
IF OBJECT_ID('dbo.sp_GetAllRoomTypes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllRoomTypes;
GO
CREATE PROCEDURE dbo.sp_GetAllRoomTypes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        name,
        description,
        daily_rate,
        image_url,
		features,
        updated_at,
        active
    FROM dbo.ROOM_TYPE
    WHERE active = 1
    ORDER BY id ASC;
END;
GO

-- =============================================
-- sp_GetRoomTypeById
-- =============================================
IF OBJECT_ID('dbo.sp_GetRoomTypeById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetRoomTypeById;
GO
CREATE PROCEDURE dbo.sp_GetRoomTypeById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        name,
        description,
        daily_rate,
        image_url,
        features,
        updated_at,
        active
    FROM dbo.ROOM_TYPE
    WHERE id     = @id
      AND active = 1;
END;
GO

-- =============================================
-- sp_UpdateRoomType
-- =============================================
CREATE OR ALTER PROCEDURE sp_UpdateRoomType
    @id          INT,
    @name        NVARCHAR(100),
    @description NVARCHAR(500) = NULL,
    @daily_rate  DECIMAL(10,2),
    @image_url   NVARCHAR(255) = NULL,
    @features    NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM ROOM_TYPE
        WHERE id = @id AND active = 1
    )
    BEGIN
        RAISERROR('Room type not found or inactive.', 16, 1);
        RETURN;
    END;

    UPDATE ROOM_TYPE
    SET
        name        = @name,
        description = @description,
        daily_rate  = @daily_rate,
        image_url   = @image_url,
        features    = @features,
        updated_at  = SYSDATETIME()
    WHERE id = @id AND active = 1;
END;
GO

-- =============================================
-- sp_CreateOffer
-- =============================================
IF OBJECT_ID('dbo.sp_CreateOffer', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CreateOffer;
GO
CREATE PROCEDURE dbo.sp_CreateOffer
    @name                NVARCHAR(100),
    @description         NVARCHAR(500),
    @discount_percentage DECIMAL(5,2),
    @start_time          DATETIME2,
    @end_time            DATETIME2,
    @room_type_id        INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.OFFER
        (name, description, discount_percentage, start_time, end_time, room_type_id, updated_at, active)
    VALUES
        (@name, @description, @discount_percentage, @start_time, @end_time, @room_type_id, SYSDATETIME(), 1);

    SELECT SCOPE_IDENTITY();
END;
GO

-- =============================================
-- sp_DeleteOffer
-- =============================================
IF OBJECT_ID('dbo.sp_DeleteOffer', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DeleteOffer;
GO
CREATE PROCEDURE dbo.sp_DeleteOffer
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.OFFER
    SET active     = 0,
        updated_at = SYSDATETIME()
    WHERE id = @id;

    SELECT @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_GetAllOffer
-- =============================================
IF OBJECT_ID('dbo.sp_GetAllOffer', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllOffer;
GO
CREATE PROCEDURE dbo.sp_GetAllOffer
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        O.id,
        O.name,
        O.description,
        O.discount_percentage,
        O.start_time,
        O.end_time,
        O.room_type_id,
        O.updated_at,
        O.updated_by,
        RT.name      AS room_type,
        RT.image_url AS url_room_type_image,
        O.active
    FROM dbo.OFFER AS O
    JOIN dbo.ROOM_TYPE AS RT ON O.room_type_id = RT.id
    WHERE O.active = 1;
END;
GO

-- =============================================
-- sp_GetClientOffers
-- =============================================
IF OBJECT_ID('dbo.sp_GetClientOffers', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetClientOffers;
GO
CREATE PROCEDURE dbo.sp_GetClientOffers
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        O.id,
        O.name,
        O.description,
        O.discount_percentage,
        O.start_time,
        O.end_time,
        O.room_type_id,
        O.updated_at,
        O.updated_by,
        RT.name      AS room_type,
        RT.image_url AS url_room_type_image,
        O.active
    FROM dbo.OFFER AS O
    JOIN dbo.ROOM_TYPE AS RT ON O.room_type_id = RT.id
    WHERE O.active = 1 AND O.start_time <= SYSDATETIME() AND O.end_time >= SYSDATETIME();
END;
GO

-- =============================================
-- sp_GetOfferById
-- =============================================
IF OBJECT_ID('dbo.sp_GetOfferById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetOfferById;
GO
CREATE PROCEDURE dbo.sp_GetOfferById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        O.id,
        O.name,
        O.description,
        O.discount_percentage,
        O.start_time,
        O.end_time,
        O.room_type_id,
        O.updated_at,
        O.updated_by,
        RT.name      AS room_type,
        RT.image_url AS url_room_type_image,
        O.active
    FROM dbo.OFFER AS O
    JOIN dbo.ROOM_TYPE AS RT ON O.room_type_id = RT.id
    WHERE O.id = @id;
END;
GO

-- =============================================
-- sp_UpdateOffer
-- =============================================
IF OBJECT_ID('dbo.sp_UpdateOffer', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateOffer;
GO
CREATE PROCEDURE dbo.sp_UpdateOffer
    @id                  INT,
    @name                NVARCHAR(100),
    @description         NVARCHAR(500),
    @discount_percentage DECIMAL(5,2),
    @start_time          DATETIME2,
    @end_time            DATETIME2,
    @room_type_id        INT,
    @updated_by          INT,
    @updated_at          DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.OFFER
    SET
        name                = @name,
        description         = @description,
        discount_percentage = @discount_percentage,
        start_time          = @start_time,
        end_time            = @end_time,
        room_type_id        = @room_type_id,
        updated_by          = @updated_by,
        updated_at          = @updated_at
    WHERE id = @id AND active = 1;

    SELECT @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_Facilities
-- =============================================
IF OBJECT_ID('dbo.sp_GetAllFacilities', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllFacilities;
GO
CREATE PROCEDURE dbo.sp_GetAllFacilities AS BEGIN
    SET NOCOUNT ON;

    SELECT
        f.id,
        f.name,
        f.description,
        f.image_url,
        f.features,
		f.updated_at,
		f.updated_by,
        F.display_order,
		f.active
    FROM dbo.FACILITY f
    WHERE f.active = 1
    ORDER BY display_order;

	EXEC sp_GetPageContentByKey 'facilities'
END;
GO

CREATE OR ALTER PROCEDURE sp_GetFacilityById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        name,
        description,
        features,
        image_url,
        display_order,
        updated_at,
        updated_by,
        active
    FROM FACILITY
    WHERE id = @id;
END;


CREATE OR ALTER PROCEDURE sp_CreateFacility
    @name NVARCHAR(100),
    @description NVARCHAR(500) = NULL,
    @features NVARCHAR(MAX) = '[]',
    @image_url NVARCHAR(255) = NULL,
    @display_order INT = 0,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @name IS NULL OR LTRIM(RTRIM(@name)) = ''
    BEGIN
        RAISERROR('Facility name is required.', 16, 1);
        RETURN;
    END;

    IF @display_order < 0
    BEGIN
        RAISERROR('Display order cannot be negative.', 16, 1);
        RETURN;
    END;

    IF @features IS NULL OR ISJSON(@features) <> 1
    BEGIN
        RAISERROR('Features must be a valid JSON array.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM FACILITY
        WHERE LOWER(name) = LOWER(@name)
          AND active = 1
    )
    BEGIN
        RAISERROR('A facility with this name already exists.', 16, 1);
        RETURN;
    END;

    INSERT INTO FACILITY (
        name,
        description,
        features,
        image_url,
        display_order,
        updated_at,
        updated_by,
        active
    )
    VALUES (
        LTRIM(RTRIM(@name)),
        @description,
        @features,
        @image_url,
        @display_order,
        SYSDATETIME(),
        @updated_by,
        1
    );

    SELECT SCOPE_IDENTITY() AS id;
END;

CREATE OR ALTER PROCEDURE sp_UpdateFacility
    @id INT,
    @name NVARCHAR(100),
    @description NVARCHAR(500) = NULL,
    @features NVARCHAR(MAX) = '[]',
    @image_url NVARCHAR(255) = NULL,
    @display_order INT = 0,
    @updated_by INT = NULL,
    @active BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM FACILITY
        WHERE id = @id
    )
    BEGIN
        RAISERROR('Facility not found.', 16, 1);
        RETURN;
    END;

    IF @name IS NULL OR LTRIM(RTRIM(@name)) = ''
    BEGIN
        RAISERROR('Facility name is required.', 16, 1);
        RETURN;
    END;

    IF @display_order < 0
    BEGIN
        RAISERROR('Display order cannot be negative.', 16, 1);
        RETURN;
    END;

    IF @features IS NULL OR ISJSON(@features) <> 1
    BEGIN
        RAISERROR('Features must be a valid JSON array.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM FACILITY
        WHERE LOWER(name) = LOWER(@name)
          AND id <> @id
          AND active = 1
    )
    BEGIN
        RAISERROR('Another active facility with this name already exists.', 16, 1);
        RETURN;
    END;

    UPDATE FACILITY
    SET
        name = LTRIM(RTRIM(@name)),
        description = @description,
        features = @features,
        image_url = @image_url,
        display_order = @display_order,
        updated_at = SYSDATETIME(),
        updated_by = @updated_by,
        active = @active
    WHERE id = @id;

    SELECT @@ROWCOUNT AS rows_affected;
END;


CREATE OR ALTER PROCEDURE sp_DeleteFacility
    @id INT,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM FACILITY
        WHERE id = @id
    )
    BEGIN
        RAISERROR('Facility not found.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM FACILITY
        WHERE id = @id
          AND active = 0
    )
    BEGIN
        RAISERROR('Facility is already inactive.', 16, 1);
        RETURN;
    END;

    UPDATE FACILITY
    SET
        active = 0,
        updated_at = SYSDATETIME(),
        updated_by = @updated_by
    WHERE id = @id;

    SELECT @@ROWCOUNT AS rows_affected;
END;


/*Stored procedure for today's status*/
IF OBJECT_ID('dbo.sp_GetRoomStatusToday', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetRoomStatusToday;
GO
CREATE PROCEDURE dbo.sp_GetRoomStatusToday
    @roomTypeId INT = NULL -- Optional Filter 
AS
BEGIN
    DECLARE @today DATE = CAST(GETDATE() AS DATE);

    SELECT 
        r.id,
        r.number,
        rt.name AS room_type,
        r.active,
        CASE
            -- If room is INACTIVE
            WHEN r.active = 0 THEN 'INACTIVE'

            -- If there's a reservation TODAY but it's not the last day
            WHEN EXISTS (
                SELECT 1 
                FROM Reservation res
                WHERE res.room_id = r.id
                AND @today >= res.check_in
                AND @today < res.check_out
            ) THEN 'OCCUPIED'

            -- If today's the last reservation day (check_out)
            WHEN EXISTS (
                SELECT 1 
                FROM Reservation res
                WHERE res.room_id = r.id
                AND @today = res.check_out
            ) THEN 'AVAILABLE'

            -- If there's no reservartions today regarding the room
            ELSE 'AVAILABLE'
        END AS status

    FROM Room r
    INNER JOIN Room_Type rt ON r.room_type_id = rt.id

    WHERE (@roomTypeId IS NULL OR r.room_type_id = @roomTypeId);
END;
GO

IF OBJECT_ID('dbo.sp_GetAllSeasons', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllSeasons;
GO
CREATE PROCEDURE sp_GetAllSeasons
    AS
BEGIN
    SELECT 
       *
    FROM Season;
END;
GO


IF OBJECT_ID('dbo.sp_GetSeasonById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetSeasonById;
GO
CREATE PROCEDURE sp_GetSeasonById
    @Id INT
AS
BEGIN
    SELECT 
       *
    FROM Season
    WHERE Id = @Id;
END;
GO


IF OBJECT_ID('dbo.sp_CreateSeason', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CreateSeason;
GO
CREATE OR ALTER PROCEDURE sp_CreateSeason
    @Name NVARCHAR(100),
    @StartDate DATE,
    @EndDate DATE,
    @Discount DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        --  Validar nombre
        IF (@Name IS NULL OR LTRIM(RTRIM(@Name)) = '')
            THROW 50001, 'El nombre de la season es obligatorio.', 1;

        --  Validar fechas
        IF (@StartDate IS NULL OR @EndDate IS NULL)
            THROW 50002, 'Las fechas son obligatorias.', 1;

        IF (@StartDate > @EndDate)
            THROW 50003, 'La fecha de inicio no puede ser mayor que la fecha final.', 1;

        --  Validar descuento
        IF (@Discount < 0 OR @Discount > 100)
            THROW 50004, 'El descuento debe estar entre 0 y 100.', 1;

        --  Evitar duplicados por nombre
        IF EXISTS (SELECT 1 FROM Season WHERE Name = @Name)
            THROW 50005, 'Ya existe una season con ese nombre.', 1;


        --  Insertar Season
        INSERT INTO Season (Name, start_date, end_date, active, discount_amount)
        VALUES (@Name, @StartDate, @EndDate,1, @Discount); 

    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO

IF OBJECT_ID('dbo.sp_DeleteSeason', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DeleteSeason;
GO
CREATE PROCEDURE sp_DeleteSeason
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS (SELECT 1 FROM Season WHERE id = @Id)
            THROW 50010, 'Season no encontrada.', 1;

        UPDATE Season
        SET active = 0
        WHERE id = @Id;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO




    UPDATE PAGE_CONTENT
    SET
        title = @title,
        subtitle = @subtitle,
        [description] = @description,
        url_content = @url_content,
        updated_at = SYSDATETIME(),
        updated_by = @updated_by
    WHERE id = @id AND active = 1;
END;
GO

-- =============================================
-- sp_CreateAdd
-- =============================================
IF OBJECT_ID('dbo.sp_CreateAdd', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CreateAdd;
GO
CREATE PROCEDURE dbo.sp_CreateAdd
    @image_url                NVARCHAR(500),
    @target_link         NVARCHAR(500),
	@updated_by			 INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.ADVERTISEMENT
        (image_url, target_link, updated_at, updated_by, active)
    VALUES
        (@image_url, @target_link, SYSDATETIME(), @updated_by, 1);

    SELECT SCOPE_IDENTITY();
END;
GO

-- =============================================
-- sp_DeleteOffer
-- =============================================
IF OBJECT_ID('dbo.sp_DeleteAdd', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DeleteAdd;
GO
CREATE PROCEDURE dbo.sp_DeleteAdd
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.ADVERTISEMENT
    SET active     = 0,
        updated_at = SYSDATETIME()
    WHERE id = @id;

    SELECT @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_GetAllOffer
-- =============================================
IF OBJECT_ID('dbo.sp_GetAllAdds', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllAdds;
GO
CREATE PROCEDURE dbo.sp_GetAllAdds
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        A.id,
		A.image_url,
		A.target_link,
        A.updated_at,
        A.updated_by,
        A.active
    FROM dbo.ADVERTISEMENT AS A
    WHERE A.active = 1;
END;
GO

-- =============================================
-- sp_UpdateOffer
-- =============================================
IF OBJECT_ID('dbo.sp_UpdateAdd', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateAdd;
GO
CREATE PROCEDURE dbo.sp_UpdateAdd
    @id INT,
	@image_url NVARCHAR(500),
    @target_link NVARCHAR(500),
	@updated_by INT
AS
BEGIN

    UPDATE dbo.ADVERTISEMENT
    SET
        image_url = @image_url,
        target_link = @target_link,
        updated_by = @updated_by

    WHERE id = @id AND active = 1;

    SELECT @@ROWCOUNT;
END;
GO


-- =============================================
-- sp_GetOfferById
-- =============================================
IF OBJECT_ID('dbo.sp_GetAddById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAddById;
GO
CREATE PROCEDURE dbo.sp_GetAddById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        A.id,
		A.image_url,
		A.target_link,
        A.updated_at,
        A.updated_by,
        A.active
    FROM dbo.ADVERTISEMENT AS A
    WHERE A.id = @id AND A.active = 1;
END;
GO
CREATE OR ALTER PROCEDURE sp_CreateRoomType
    @name        NVARCHAR(100),
    @description NVARCHAR(500) = NULL,
    @daily_rate  DECIMAL(10,2),
    @image_url   NVARCHAR(255) = NULL,
    @features    NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ROOM_TYPE (name, description, daily_rate, image_url, features, updated_at, active)
    VALUES (@name, @description, @daily_rate, @image_url, @features, SYSDATETIME(), 1);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END;
GO

CREATE OR ALTER PROCEDURE sp_DeleteRoomType
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM ROOM_TYPE
        WHERE id = @id AND active = 1
    )
    BEGIN
        RAISERROR('Room type not found or inactive.', 16, 1);
        RETURN;
    END;

    UPDATE ROOM_TYPE
    SET active = 0
    WHERE id = @id;
END;
GO


-- =============================================
-- Consultas del dashborda
-- =============================================

-- 1. Total Revenue
CREATE OR ALTER PROCEDURE sp_GetTotalRevenue
    @date_from DATE,
    @date_to DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(SUM(total_amount), 0) AS total
    FROM RESERVATION
    WHERE active = 1
      AND check_in >= @date_from
      AND check_in <= @date_to;
END
GO

-- 2. Average Night Price
CREATE OR ALTER PROCEDURE sp_GetAvgNightPrice
    @date_from DATE,
    @date_to DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(AVG(rnd.final_night_price), 0) AS avg_price
    FROM RESERVATION_NIGHT_DETAIL rnd
    INNER JOIN RESERVATION r ON rnd.reservation_id = r.id
    WHERE rnd.active = 1
      AND r.check_in >= @date_from
      AND r.check_in <= @date_to;
END
GO

-- 3. Total Reservations
CREATE OR ALTER PROCEDURE sp_GetTotalReservations
    @date_from DATE,
    @date_to DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) AS total_count
    FROM RESERVATION
    WHERE active = 1
      AND check_in >= @date_from
      AND check_in <= @date_to;
END
GO

-- 4. Revenue By Month
CREATE OR ALTER PROCEDURE sp_GetRevenueByMonth
    @date_from DATE,
    @date_to DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        YEAR(check_in)    AS year,
        MONTH(check_in)   AS month,
        SUM(total_amount) AS total
    FROM RESERVATION
    WHERE active = 1
      AND check_in >= @date_from
      AND check_in <= @date_to
    GROUP BY YEAR(check_in), MONTH(check_in)
    ORDER BY year, month;
END
GO

-- 5. Revenue By Room Type
CREATE OR ALTER PROCEDURE sp_GetRevenueByRoomType
    @date_from DATE,
    @date_to DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        rt.name             AS room_type,
        SUM(r.total_amount) AS total
    FROM RESERVATION r
    INNER JOIN ROOM      ro ON r.room_id        = ro.id
    INNER JOIN ROOM_TYPE rt ON ro.room_type_id  = rt.id
    WHERE r.active = 1
      AND r.check_in >= @date_from
      AND r.check_in <= @date_to
    GROUP BY rt.name
    ORDER BY total DESC;
END
GO

-- 6. Revenue By Offer
CREATE OR ALTER PROCEDURE sp_GetRevenueByOffer
    @date_from DATE,
    @date_to DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        ISNULL(o.name, 'Sin oferta')  AS offer_name,
        SUM(r.total_amount)           AS total,
        COUNT(r.id)                   AS reservations_count
    FROM RESERVATION r
    LEFT JOIN OFFER o ON r.selected_offer_id = o.id
    WHERE r.active = 1
      AND r.check_in >= @date_from
      AND r.check_in <= @date_to
    GROUP BY o.name
    ORDER BY total DESC;
END
GO

-- =============================================
-- Consultas de bitácora
-- =============================================


-- Insertar registro en bitácora
CREATE PROCEDURE sp_CreateActivityLog
    @admin_user_id INT,
    @action_code   INT,
    @screen        NVARCHAR(100),
    @ip_address    NVARCHAR(45),
    @detail        NVARCHAR(255) = NULL
AS
BEGIN
    DECLARE @action_code_id INT
    SELECT @action_code_id = id FROM ACTION_CODE WHERE code = @action_code
 
    IF @action_code_id IS NULL
        RETURN
 
    INSERT INTO ACTIVITY_LOG (admin_user_id, action_code_id, screen, ip_address, detail, created_at)
    VALUES (@admin_user_id, @action_code_id, @screen, @ip_address, @detail, DATEADD(HOUR, -6, GETUTCDATE()))
END
GO

 
-- Obtener todos los registros con joins
CREATE PROCEDURE sp_GetAllActivityLogs
AS
BEGIN
    SELECT
        al.id,
        au.username,
        ac.code     AS action_code,
        ac.name     AS action_name,
        ac.category,
        al.screen,
        al.detail,
        al.ip_address,
        al.created_at
    FROM ACTIVITY_LOG al
    INNER JOIN ADMIN_USER  au ON au.id = al.admin_user_id
    INNER JOIN ACTION_CODE ac ON ac.id = al.action_code_id
    ORDER BY al.created_at DESC
END
GO
 
-- Obtener todos los códigos de acción
CREATE PROCEDURE sp_GetAllActionCodes
AS
BEGIN
    SELECT
        id,
        code,
        name,
        category,
        description
    FROM ACTION_CODE
    ORDER BY code ASC
END
GO

-- Activity Logs filtrados
CREATE PROCEDURE sp_FilteredActivityLogs
    @id          INT           = NULL,
    @username    NVARCHAR(100) = NULL,
    @actionCode  INT           = NULL,
    @dateFrom    DATETIME2     = NULL,
    @dateTo      DATETIME2     = NULL
AS
BEGIN
    SELECT
        al.id,
        au.username,
        ac.code     AS action_code,
        ac.name     AS action_name,
        ac.category,
        al.screen,
        al.detail,
        al.ip_address,
        al.created_at
    FROM ACTIVITY_LOG al
    INNER JOIN ADMIN_USER  au ON au.id = al.admin_user_id
    INNER JOIN ACTION_CODE ac ON ac.id = al.action_code_id
    WHERE
        (@id         IS NULL OR al.id = @id)
        AND
        (@username   IS NULL OR au.username LIKE '%' + @username + '%')
        AND
        (@actionCode IS NULL OR ac.code = @actionCode)
        AND
        (@dateFrom   IS NULL OR al.created_at >= @dateFrom)
        AND
        (@dateTo     IS NULL OR al.created_at < DATEADD(DAY, 1, @dateTo))  -- 👈 incluye todo el día
    ORDER BY al.created_at DESC
END
GO