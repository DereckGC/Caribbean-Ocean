



USE IF7100_CaribbeanOceanDB
GO

-- =========================================
-- ADMIN_USER
-- =========================================
CREATE TABLE ADMIN_USER (
    id INT IDENTITY(1,1) NOT NULL,
    username NVARCHAR(50) NOT NULL,
    password_hash NVARCHAR(255) NOT NULL,
    name NVARCHAR(150) NOT NULL,
    email NVARCHAR(150) NOT NULL,
    last_login DATETIME2 NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_ADMIN_USER PRIMARY KEY (id),
    CONSTRAINT UQ_ADMIN_USER_USERNAME UNIQUE (username),
    CONSTRAINT UQ_ADMIN_USER_EMAIL UNIQUE (email)
);
GO

-- =========================================
-- ROOM_TYPE
-- =========================================
CREATE TABLE ROOM_TYPE (
    id INT IDENTITY(1,1) NOT NULL,
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(500) NULL,
    daily_rate DECIMAL(10,2) NOT NULL,
    image_url NVARCHAR(255) NULL,
	features NVARCHAR(500) NULL,
    updated_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_ROOM_TYPE PRIMARY KEY (id)
);
GO


-- =========================================
-- SEASON
-- =========================================
CREATE TABLE SEASON (
    id INT IDENTITY(1,1) NOT NULL,
    name NVARCHAR(100) NOT NULL,
	discount_amount DECIMAL (5,2) NOT NULL,
    start_date DATETIME2 NOT NULL,
    end_date DATETIME2 NOT NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_SEASON PRIMARY KEY (id),
    CONSTRAINT UQ_SEASON_NAME UNIQUE (name),
    CONSTRAINT CHK_SEASON_DATES CHECK (end_date >= start_date),
	CONSTRAINT CHK_SEASON_DISCOUNT_AMOUNT CHECK (discount_amount >= 0 and discount_amount <= 100)
);
GO

-- =========================================
-- ROOM
-- =========================================
CREATE TABLE ROOM (
    id INT IDENTITY(1,1) NOT NULL,
    number INT NOT NULL,
    room_type_id INT NOT NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_ROOM PRIMARY KEY (id),
    CONSTRAINT UQ_ROOM_NUMBER UNIQUE (number),
    CONSTRAINT FK_ROOM_ROOM_TYPE FOREIGN KEY (room_type_id) REFERENCES ROOM_TYPE(id)
);
GO
-- =========================================
-- OFFER
-- =========================================
CREATE TABLE OFFER (
    id INT IDENTITY(1,1) NOT NULL,
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(500) NULL,
    discount_percentage DECIMAL(5,2) NOT NULL,
    start_time DATETIME2 NOT NULL,
    end_time DATETIME2 NOT NULL,
    room_type_id INT NOT NULL,
    updated_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    updated_by INT NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_OFFER PRIMARY KEY (id),
    CONSTRAINT FK_OFFER_ROOM_TYPE FOREIGN KEY (room_type_id) REFERENCES ROOM_TYPE(id),
    CONSTRAINT FK_OFFER_UPDATED_BY FOREIGN KEY (updated_by) REFERENCES ADMIN_USER(id),
    CONSTRAINT CHK_OFFER_DATES CHECK (end_time >= start_time),
    CONSTRAINT CHK_OFFER_DISCOUNT CHECK (discount_percentage >= 0 AND discount_percentage <= 100)
);
GO

-- =========================================
-- ADVERTISEMENT
-- =========================================
CREATE TABLE ADVERTISEMENT (
    id INT IDENTITY(1,1) NOT NULL,
    image_url NVARCHAR(255) NOT NULL,
    target_link NVARCHAR(500) NULL,
    updated_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    updated_by INT NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_ADVERTISEMENT PRIMARY KEY (id),
    CONSTRAINT FK_ADVERTISEMENT_UPDATED_BY FOREIGN KEY (updated_by) REFERENCES ADMIN_USER(id)
);
GO

-- =========================================
-- PAGE_CONTENT
-- =========================================
CREATE TABLE PAGE_CONTENT (
    id INT IDENTITY(1,1) NOT NULL,
    [key] NVARCHAR(100) NOT NULL,
    title NVARCHAR(200) NOT NULL,
    subtitle NVARCHAR(100) NOT NULL,
    [description] NVARCHAR(MAX) NULL,
    url_content NVARCHAR(MAX) NULL,
    updated_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    updated_by INT NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_PAGE_CONTENT PRIMARY KEY (id),
    CONSTRAINT UQ_PAGE_CONTENT_KEY UNIQUE ([key]),
    CONSTRAINT FK_PAGE_CONTENT_UPDATED_BY FOREIGN KEY (updated_by) REFERENCES ADMIN_USER(id),
    CONSTRAINT CK_URL_CONTENT_JSON CHECK (ISJSON(url_content) > 0)
);
GO


-- =========================================
-- HOTEL_CONTACT
-- =========================================
CREATE TABLE HOTEL_CONTACT (
    id INT IDENTITY (1,1) NOT NULL,
    [type] NVARCHAR (20),
    contact NVARCHAR(MAX),
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_HOTEL_CONTACT PRIMARY KEY (id),
    CONSTRAINT CHK_TYPE_CONTACT CHECK ([type] IN ('whatsapp', 'calls', 'email', 'instagram', 'tiktok', 'facebook'))
);
GO

-- =========================================
-- FACILITY
-- =========================================
CREATE TABLE FACILITY (
    id INT IDENTITY(1,1) NOT NULL,
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(500) NULL,
	features NVARCHAR (MAX),
    image_url NVARCHAR(255) NULL,
    display_order INT NOT NULL DEFAULT 0,
    updated_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    updated_by INT NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_FACILITY PRIMARY KEY (id),
    CONSTRAINT FK_FACILITY_UPDATED_BY FOREIGN KEY (updated_by) REFERENCES ADMIN_USER(id),
    CONSTRAINT CHK_FACILITY_DISPLAY_ORDER CHECK (display_order >= 0),
	CONSTRAINT CK_FACILITY_FEATURES CHECK (ISJSON(features) > 0)
);
GO

-- =========================================
-- RESERVATION
-- =========================================
CREATE TABLE RESERVATION (
    id INT IDENTITY(1,1) NOT NULL,
    code NVARCHAR(50) NOT NULL,
    room_id INT NOT NULL,
    client_name NVARCHAR(100) NOT NULL,
    client_lastname NVARCHAR(100) NOT NULL,
    masked_card NVARCHAR(25) NOT NULL,
    email nvarchar(255),
    created_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    check_in DATETIME2 NOT NULL,
    check_out DATETIME2 NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL,
    active BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_RESERVATION PRIMARY KEY (id),
    CONSTRAINT UQ_RESERVATION_CODE UNIQUE (code),
    CONSTRAINT FK_RESERVATION_ROOM FOREIGN KEY (room_id) REFERENCES ROOM(id),
    CONSTRAINT CHK_RESERVATION_DATES CHECK (check_out > check_in),
    CONSTRAINT CHK_RESERVATION_TOTAL CHECK (total_amount >= 0)
);


GO
ALTER TABLE RESERVATION
ADD pricing_used_offer BIT NOT NULL DEFAULT 0,
    selected_offer_id INT NULL;


 ALTER TABLE RESERVATION
ADD CONSTRAINT FK_RESERVATION_SELECTED_OFFER
FOREIGN KEY (selected_offer_id)
REFERENCES OFFER(id);
-- =========================================
-- INDEXES
-- =========================================
CREATE INDEX IX_ROOM_ROOM_TYPE_ID_active ON ROOM(room_type_id, active);
GO
CREATE INDEX IX_RESERVATION_ROOM_ID_active_DATES ON RESERVATION(room_id, active, check_in, check_out);
GO


-- =============================================
-- ACTION CODE
-- =============================================

CREATE TABLE [dbo].[ACTION_CODE](
    [id]          [int] IDENTITY(1,1) NOT NULL,
    [code]        [int] NOT NULL,
    [name]        [nvarchar](50) NOT NULL,
    [category]    [nvarchar](50) NOT NULL,
    [description] [nvarchar](255) NOT NULL,
    CONSTRAINT [PK_ACTION_CODE]      PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UQ_ACTION_CODE_CODE] UNIQUE NONCLUSTERED ([code] ASC)
)
GO

-- =============================================
-- ACTIVITY LOG
-- =============================================
CREATE TABLE [dbo].[ACTIVITY_LOG](
    [id]            [int] IDENTITY(1,1) NOT NULL,
    [admin_user_id] [int] NOT NULL,
    [action_code_id][int] NOT NULL,
    [screen]        [nvarchar](100) NOT NULL,
    [ip_address]    [nvarchar](45) NOT NULL,
    [detail]        [nvarchar](255) NULL,
    [created_at]    [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_ACTIVITY_LOG] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ACTIVITY_LOG_ADMIN_USER]   FOREIGN KEY ([admin_user_id])
        REFERENCES [dbo].[ADMIN_USER] ([id]),
    CONSTRAINT [FK_ACTIVITY_LOG_ACTION_CODE]  FOREIGN KEY ([action_code_id])
        REFERENCES [dbo].[ACTION_CODE] ([id])
)
GO