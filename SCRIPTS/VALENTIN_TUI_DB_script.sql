-- VALENTIN_TUI_APP
-- Branch 001
-- Valentin Belhomme
-- 26/08/2023

USE [VALENTIN_TUI_DB]
GO

-- Table __EFMigrationsHistory
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE name='__EFMigrationsHistory' and xtype='U')
BEGIN 
	CREATE TABLE [dbo].[__EFMigrationsHistory](
		[MigrationId] [nvarchar](150) NOT NULL,
		[ProductVersion] [nvarchar](32) NOT NULL,
	 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
	(
		[MigrationId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO


-- Table Airport 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE name='Airport' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Airport](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [varchar](150) NULL,
		[Latitude] [float] NOT NULL,
		[Longitude] [float] NOT NULL
	 CONSTRAINT [PK_Airport] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

-- Table Flight 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE name='Flight' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Flight](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[DepartureAirportId] [int] FOREIGN KEY REFERENCES Airport(Id) NOT NULL,
		[ArrivalAirportId] [int] FOREIGN KEY REFERENCES Airport(Id) NOT NULL,
		[Distance] [int]
	 CONSTRAINT [PK_Flight] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] 
END
GO


/*********-- INSERT SOME DATA --*********/
IF NOT EXISTS (SELECT 1 FROM [dbo].[Airport] WHERE Name = 'Hannover Airport')
BEGIN 
	INSERT INTO [dbo].[Airport] 
	VALUES (
		'Hannover Airport', 
		52.46144, 
		9.68724
	)
END 
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Airport] WHERE Name = 'Charles de Gaulle International Airport')
BEGIN 
	INSERT INTO [dbo].[Airport] 
	VALUES (
		'Charles de Gaulle International Airport', 
		49.00689, 
		2.57108
	)
END 
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Airport] WHERE Name = 'Barcelona El Prat Josep Tarradellas Airport')
BEGIN 
	INSERT INTO [dbo].[Airport] 
	VALUES (
		'Barcelona El Prat Josep Tarradellas Airport', 
		41.29694, 
		2.07905
	)
END 
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Airport] WHERE Name = 'Heathrow Airport')
BEGIN 
	INSERT INTO [dbo].[Airport] 
	VALUES (
		'Heathrow Airport', 
		51.46774, 
		-0.45878
	)
END 
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Airport] WHERE Name = 'Frankfurt Airport')
BEGIN 
	INSERT INTO [dbo].[Airport] 
	VALUES (
		'Frankfurt Airport', 
		50.02294, 
		8.52494
	)
END 
GO

IF NOT EXISTS (SELECT * FROM [dbo].[Flight] f
				INNER JOIN [dbo].[Airport] a1 on a1.id = f.DepartureAirportId
				INNER JOIN [dbo].[Airport] a2 on a2.id = f.ArrivalAirportId
				WHERE EXISTS (
					SELECT 1 FROM [dbo].[Airport] WHERE id in ( f.DepartureAirportId , f.ArrivalAirportId)) 
				AND f.ID = 1)
BEGIN 
	INSERT INTO [dbo].[Flight] 
	VALUES (1, 3, null)
END 
GO