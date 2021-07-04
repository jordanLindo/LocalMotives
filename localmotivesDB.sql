
/*Check if database exists Drop Database*/
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
	WHERE name = 'LocalmotivesDB')
BEGIN
DROP DATABASE [LocalmotivesDB]
print '' print'*** Dropping Database LocalmotivesDB'
END
GO

print '' print '*** Creating Database'
GO

CREATE DATABASE [LocalmotivesDB]
GO

print '' print '*** Using Database LocalmotivesDB'
GO

USE [LocalmotivesDB]
GO

print '' print '*** Creating Station Table'

CREATE TABLE [dbo].[Station]
(
	 [StationID]		[int]				IDENTITY(1000000,1)		NOT NULL
    ,[StationName]		[nvarchar](150)								NOT NULL
    ,[StationType]		[nvarchar](20)								NOT NULL
    ,[Active]			[bit]				DEFAULT 1
	,CONSTRAINT			[pk_StationID]		PRIMARY KEY		([StationID] ASC)
)
GO

print '' print '*** Creating sample Station records'
GO

INSERT INTO [dbo].[Station]
([StationName],[StationType])
VALUES
	 ('Belle Station','Full')
	,('Sinclare Station','Open')
	,('Rocky Station','Full')
	,('Icy Station','Full')
	,('Toasty Station','Full')
	,('Quiet Station','Full')
	,('Cold River Station','Open')
	,('Cold Tree Station','Full')
	,('Crater Rock Station','Service')

GO

print '' print'*** Creating Route table'
GO

CREATE TABLE [dbo].[Route]
(
	 [RouteID]				[int]				IDENTITY(1000000,1)			NOT NULL
	,[TotalTimeMin]			[int]											NOT NULL
	,[Active]				[bit]											NOT NULL
		DEFAULT 1
	,CONSTRAINT		[pk_RouteID]							PRIMARY KEY			([RouteID] ASC)
)
GO

print '' print'*** Creating sample Route records'
GO

INSERT INTO [dbo].[Route]
	([TotalTimeMin])
VALUES
	 (135)
	,(119)
	,(109)
GO

print '' print'*** Creating RouteLine table'
GO

CREATE TABLE [dbo].[RouteLine]
(
	 [RouteLineID]			[int]		IDENTITY(1000000,1)					NOT NULL
	,[RouteID]				[int]											NOT NULL
	,[StartStationID]		[int]											NOT NULL
	,[TimeToNext]			[int]											NOT NULL
	,[RoutePosition]		[int]											NOT NULL
		DEFAULT 1
	,CONSTRAINT 			[pk_RouteLineID]				PRIMARY KEY			([RouteLineID] ASC)
	,CONSTRAINT				[fk_RouteID_RouteLine]			FOREIGN KEY			([RouteID])
		REFERENCES	[Route]([RouteID])
	,CONSTRAINT				[fk_StartStationID_RouteLine]		FOREIGN KEY			([StartStationID])
		REFERENCES	[Station]([StationID])
)
GO

print '' print '*** Creating sample RouteLine records'
GO

INSERT INTO [dbo].[RouteLine]
	([RouteID],[StartStationID],[RoutePosition],[TimeToNext])
	VALUES
	 (1000000,1000000,7,15)
	,(1000000,1000001,8,15)
	,(1000000,1000002,1,15)
	,(1000000,1000003,2,15)
	,(1000000,1000004,3,15)
	,(1000000,1000005,4,15)
	,(1000000,1000006,5,15)
	,(1000000,1000007,6,30)
	,(1000001,1000000,1,22)
	,(1000001,1000002,2,15)
	,(1000001,1000003,3,15)
	,(1000001,1000004,4,22)
	,(1000001,1000006,5,15)
	,(1000001,1000007,6,30)
	,(1000002,1000000,3,22)
	,(1000002,1000002,4,10)
	,(1000002,1000008,5,10)
	,(1000002,1000004,1,22)
	,(1000002,1000006,1,15)
	,(1000002,1000007,2,30)
GO

print '' print '*** Creating Train table'
GO

CREATE TABLE [dbo].[Train]
(
	 [TrainID]				[int]				IDENTITY(1000000,1)			NOT NULL
    ,[TrainName]			[nvarchar](100)									NOT NULL
    ,[RouteID]				[int]											NOT NULL
		DEFAULT NULL
    ,[Active]				[bit]         		DEFAULT 1					NOT NULL
	,CONSTRAINT			[pk_TrainID]			PRIMARY KEY			([TrainID] ASC)
	,CONSTRAINT			[fk_RouteID_Train]		FOREIGN KEY			([RouteID])
		REFERENCES	[Route]([RouteID])
)

print '' print '*** Creating sample Train records'
GO

INSERT INTO [dbo].[Train]
([TrainName],[RouteID])
VALUES
	 ('Mamba',1000000)
	,('Pythia',1000001)
	,('Condor',1000002)

GO

print '' print '*** Creating CoachType table'
GO

CREATE TABLE [dbo].[CoachType]
(
	 [CoachTypeID]			[int]				IDENTITY(1000000,1)			NOT NULL
    ,[Description]			[nvarchar](50)									NULL
    ,[Active]				[bit]				DEFAULT 1					NOT NULL
	,CONSTRAINT		[pk_CoachTypeID]			PRIMARY KEY 	([CoachTypeID] ASC)
)
GO

print '' print '*** Creating sample Coach records'
GO

INSERT INTO [dbo].[CoachType]
	([Description])
VALUES
	 ('Dining')
	,('Sleeper')
	,('Cargo')
	,('Coach')
	,('First Class')

GO

print '' print '*** Creating Customer table'
GO

CREATE TABLE [dbo].[Customer]
(
	 [CustomerID]		[int]				IDENTITY(1000000,1)			NOT NULL
	,[Email]			[nvarchar](150)									NULL
		DEFAULT	NULL
	,[FirstName]		[nvarchar](150)									NOT NULL
		DEFAULT 'Unspecified'
    ,[LastName]			[nvarchar](150)								 	NOT NULL
		DEFAULT 'Unspecified'
	,CONSTRAINT		[pk_CustomerID]			PRIMARY KEY			([CustomerID] ASC)
	
)
GO

print '' print '*** Creating sample Customer records'
GO

INSERT INTO [dbo].[Customer]
	([FirstName],[LastName],[Email])
VALUES
	 ('John','James','johnj@home.com')
	,('Stan','Smythe','ss@home.com')
	,('Jay','James', 'jj@home.edu')
	,(DEFAULT,DEFAULT,DEFAULT)
GO

print '' print'*** Creating Employee table'
GO

CREATE TABLE [dbo].[Employee]
(
	 [EmployeeID]		[int]					IDENTITY(1000000,1)		NOT NULL
	,[FirstName]		[nvarchar](150)									NOT NULL
	,[LastName]			[nvarchar](150)									NOT NULL
	,[Email]			[nvarchar](250)									NOT NULL
	,[PhoneNumber]		[nvarchar](12)									NOT NULL
	,[Active]			[bit]											NOT NULL
		DEFAULT 1
	,[PasswordHash]		[nvarchar](100)									NOT NULL
		DEFAULT '9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E'
	,CONSTRAINT 	[pk_EmployeeID]					PRIMARY KEY			([EmployeeID] ASC)
)
GO

print '' print'*** Creating sample Employee records'
GO

INSERT INTO [dbo].[Employee]
	([FirstName],[LastName],[Email],[PhoneNumber])
	VALUES
	 ('System','Admin','admin@work.com','00000000000')
	,('Alvin','Albertson','aal@work.com','13132423535')
	,('Olivia','Olafsen','olive@work.com','12132423535')
	,('Norman','Bates','nbates@work.com','12111123535')
	,('Second','Admin','2admin@work.com','0000000001')
GO

print '' print '*** Creating Reservation table'
GO

CREATE TABLE [dbo].[Reservation]
(
	 [ReservationID]	[int]				IDENTITY(1000000,1)			
	,[CustomerID]		[int]				NULL
	,[EmployeeID]		[int]				NULL
    ,[TravelDate]		[datetime]			NOT NULL
    ,[StationStart]		[int]				NOT NULL
    ,[StationEnd]		[int]				NOT NULL
    ,[Active]			[bit]				DEFAULT 1
	,CONSTRAINT [pk_ReservationID]				PRIMARY KEY	([ReservationID] ASC)
    ,CONSTRAINT [fk_CustomerID_Reservation] 	FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID])
    ,CONSTRAINT [fk_StationStart_Reservation] 	FOREIGN KEY ([StationStart]) REFERENCES [Station]([StationID])
    ,CONSTRAINT [fk_StationEnd_Reservation]		FOREIGN KEY ([StationEnd]) REFERENCES [Station]([StationID])
)
GO

print '' print '*** Creating sample Reservation records'
GO


INSERT INTO [dbo].[Reservation]
([CustomerID],[EmployeeID],[TravelDate],[StationStart],[StationEnd])
VALUES
	 (1000000,1000000,'2018-12-02 23:00:00',1000002,1000001)
	,(1000001,1000002,'2018-12-04 09:00:00',1000000,1000002)
	,(1000002,1000000,'2018-12-10 20:00:00',1000002,1000001)
GO

print '' print '*** Creating NONCLUSTERED INDEX TravelDate in reservation'
GO

CREATE NONCLUSTERED INDEX [ix_TravelDate_Reservation] on [Reservation]([TravelDate] ASC)
GO

print '' print '*** Creating SeatType table'
GO

CREATE TABLE [dbo].[SeatType]
(
	 [SeatTypeID]					[int]				IDENTITY(1000000,1) 			NOT NULL
    ,[Description]					[nvarchar](50)										NOT NULL
	,[Price]						[money]												NOT NULL
	,[Active]						[bit]												NOT NULL
		DEFAULT 1
	,CONSTRAINT [pk_SeatTypeID]							PRIMARY KEY	([SeatTypeID] ASC)
)
GO

print '' print '*** Creating sample SeatType records'
GO

INSERT INTO [dbo].[SeatType]
([Description],[Price])
VALUES
	 ('Table 2 Seat',$50)
	,('Single bed',$90)
	,('Two bed room',$200)
	,('Single Seat',$20)
	,('Deluxe Seat',$40)
	,('Two Bench room',$60)
	,('Bench',$30)
	,('Single Bed room',$180)
	
GO

print '' print '*** Creating CarList Table'
GO
	
CREATE TABLE [dbo].[CarList]
(
		 [CarListID]						[int]		IDENTITY(1000000,1)		NOT NULL
		,[TrainID]							[int]								NOT NULL
		,CONSTRAINT	[pk_CarListID]										PRIMARY KEY ([CarListID] ASC)
		,CONSTRAINT [fk_TrainID_CarList]								FOREIGN KEY ([TrainID])
			REFERENCES [Train]([TrainID])
)
GO

print '' print '*** Creating sample CarList records'
GO
 
INSERT INTO [dbo].[CarList]
	([TrainID])
	VALUES
	 (1000000)
	,(1000001)
	,(1000002)
GO

print '' print '*** Creating TrainCar table'
GO

CREATE TABLE [dbo].[TrainCar]
(
	 [TrainCarID]							[int]			IDENTITY(1000000,1)	NOT NULL
	,[CoachTypeID]							[int]								NOT NULL
	,[CarListID]							[int]								NOT NULL
	,[Active]								[bit]			DEFAULT 1			NOT NULL
	,CONSTRAINT	[pk_TrainCarID]						PRIMARY KEY ([TrainCarID] ASC)
	,CONSTRAINT [fk_CoachTypeID_TrainCar]					FOREIGN KEY ([CoachTypeID])
		REFERENCES [CoachType]([CoachTypeID])
	,CONSTRAINT	[fk_CarListID_TrainCar]					FOREIGN KEY	([CarListID])
		REFERENCES [CarList]([CarListID])
)
GO

print '' print '*** Creating sample TrainCar records'
GO


INSERT INTO [dbo].[TrainCar]
	([CoachTypeID],[CarListID])
	VALUES
	 (1000000,1000000)
	,(1000002,1000000)
	,(1000003,1000000)
	,(1000003,1000000)
	,(1000003,1000000)
	,(1000004,1000000)
	,(1000000,1000000)
	,(1000002,1000000)
	,(1000001,1000000)
	,(1000001,1000000)
	,(1000002,1000001)
	,(1000003,1000001)
	,(1000003,1000001)
	,(1000002,1000002)
	,(1000001,1000002)
	,(1000001,1000002)
	,(1000002,1000002)
	,(1000003,1000002)
GO


print '' print'*** Creating SeatList Table'
GO

CREATE TABLE[dbo].[SeatList]
(
	 [SeatListID]										[int]		IDENTITY(1000000,1)				NOT NULL
	,[TrainCarID]										[int]										NOT NULL
	,[Active]											[bit]										NOT NULL
		DEFAULT 1
	,CONSTRAINT		[pk_SeatListID]						PRIMARY KEY	([SeatListID] ASC)
	,CONSTRAINT 	[fk_TrainCarID_SeatList]			FOREIGN KEY	([TrainCarID])
		REFERENCES [TrainCar]([TrainCarID])
)
GO

print '' print'*** Creating sample SeatList records'
GO

INSERT INTO [dbo].[SeatList]
	([TrainCarID])
	VALUES
	 (1000000)
	,(1000002)
	,(1000008)
	,(1000003)
	,(1000010)
	,(1000001)
	,(1000004)
	,(1000005)
	,(1000006)
GO

print '' print '*** Creating Seat Table'
GO

CREATE TABLE [dbo].[Seat]
(
	 [SeatID]											[int]		IDENTITY(1000000,1)				NOT NULL
	,[SeatListID]										[int]										NOT NULL
	,[SeatTypeID]										[int]										NOT NULL
	,[Active]											[bit]										NOT NULL
		DEFAULT 1
	,[Available]										[bit]										NOT NULL
		DEFAULT 1
	,CONSTRAINT		[pk_SeatID]								PRIMARY KEY	([SeatID] ASC)
	,CONSTRAINT		[fk_SeatListID_Seat]					FOREIGN KEY	([SeatListID])
		REFERENCES [SeatList]([SeatListID])
	,CONSTRAINT 	[fk_SeatTypeID_Seat]					FOREIGN KEY ([SeatTypeID])
		REFERENCES [SeatType]([SeatTypeID])
)

print '' print '*** Creating sample Seat records'
GO

INSERT INTO [dbo].[Seat]
	([SeatListID],[SeatTypeID])
	VALUES
	 (1000000,1000000)
	,(1000000,1000000)
	,(1000000,1000000)
	,(1000000,1000000)
	,(1000000,1000000)
	,(1000000,1000000)
	,(1000001,1000001)
	,(1000001,1000001)
	,(1000001,1000002)
	,(1000001,1000002)
	,(1000001,1000006)
	,(1000003,1000006)
	,(1000002,1000006)
	,(1000002,1000006)
	,(1000002,1000006)
	,(1000002,1000006)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000003)
	,(1000002,1000005)
	,(1000002,1000005)
	,(1000003,1000004)
	,(1000003,1000004)
	,(1000003,1000004)
	,(1000003,1000004)
	,(1000003,1000007)
	,(1000003,1000007)
	,(1000004,1000006)
	,(1000004,1000006)
	,(1000004,1000006)
	,(1000004,1000006)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000003)
	,(1000004,1000005)
	,(1000004,1000005)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000003)
	,(1000005,1000005)
	,(1000005,1000005)
GO

print '' print '*** Creating ReservationLine Table'
GO

CREATE TABLE [dbo].[ReservationLine]
(
		 [SeatID]										[int]					NOT NULL
		,[ReservationID]								[int]					NOT NULL
		,CONSTRAINT	[pk_SeatID_ReservationID]							PRIMARY KEY ([SeatID] ASC, [ReservationID] ASC)
		,CONSTRAINT [fk_SeatID_ReservationLine]							FOREIGN KEY	([SeatID])
			REFERENCES [Seat]([SeatID])
		,CONSTRAINT [fk_ReservationID_ReservationLine]							FOREIGN KEY ([ReservationID])
			REFERENCES [Reservation]([ReservationID])
)
GO

print '' print '*** Creating sample ReservationLine records'
GO

INSERT INTO [dbo].[ReservationLine]
	([SeatID],[ReservationID])
VALUES
	 (1000000,1000000)
	,(1000001,1000000)
	,(1000002,1000001)
GO


print '' print '*** Creating EmployeeList table'
GO

CREATE TABLE [dbo].[EmployeeList]
(
	 [EmployeeListID]						[int]	IDENTITY(1000000,1)			NOT NULL
	,[TrainID]								[int]								NOT NULL
	,CONSTRAINT	[pk_EmployeeListID]							PRIMARY KEY ([EmployeeListID] ASC)
	,CONSTRAINT [fk_TrainID_EmployeeList]					FOREIGN KEY ([TrainID])
		REFERENCES [Train]([TrainID])
)
GO

print '' print '*** Creating sample EmployeeList records'
GO

INSERT INTO [dbo].[EmployeeList]
	([TrainID])
	VALUES
	 (1000000)
	,(1000001)
	,(1000002)
GO

print '' print '*** Creating EmployeeLine table'
GO

CREATE TABLE [dbo].[EmployeeLine]
(
	 [EmployeeID]								[int]					NOT NULL
	,[EmployeeListID]							[int]					NOT NULL
	,CONSTRAINT [pk_EmployeeID_EmployeeListID]				PRIMARY KEY ([EmployeeID] ASC, [EmployeeListID] ASC)
	,CONSTRAINT [fk_EmployeeID]								FOREIGN KEY	([EmployeeID])
		REFERENCES [Employee]([EmployeeID])
	,CONSTRAINT [fk_EmployeeListID]							FOREIGN KEY ([EmployeeListID])
		REFERENCES [EmployeeList]([EmployeeListID])
)
GO

print '' print '*** Creating sample EmployeeLine records'
GO

INSERT INTO [dbo].[EmployeeLine]
	([EmployeeID],[EmployeeListID])
	VALUES
	 (1000000,1000002)
	,(1000001,1000000)
	,(1000002,1000001)
GO

print '' print '*** Creating Role table'
GO

CREATE TABLE [dbo].[Role]
(
	 [RoleID]									[nvarchar](200)			NOT NULL
	,[Description]								[nvarchar](200)			NULL
	,CONSTRAINT [pk_RoleID]						PRIMARY KEY	([RoleID] ASC)
)

print '' print '*** Creating sample Role records'
GO

INSERT INTO [dbo].[Role]
	([RoleID])
	VALUES
	 ('Clerk')
	,('Administrator')
	,('Conductor')
	,('Manager')
GO

print '' print '*** Creating EmployeeRole table'
GO

CREATE TABLE [dbo].[EmployeeRole]
(
	 [EmployeeID]								[int]					NOT NULL
	,[RoleID] 									[nvarchar](200)			NOT NULL
	,CONSTRAINT [pk_EmployeeID_RoleID]			PRIMARY KEY ([EmployeeID] ASC, [RoleID] ASC)
	,CONSTRAINT [fk_EmployeeID_EmployeeRole]	FOREIGN KEY ([EmployeeID])
		REFERENCES [Employee]([EmployeeID])
	,CONSTRAINT [fk_RoleID]						FOREIGN KEY ([RoleID])
		REFERENCES [Role]([RoleID])
)
GO

print '' print '*** Creating sample EmployeeRole records'
GO

INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID],[RoleID])
	VALUES
	 (1000000,'Clerk')
	,(1000000,'Administrator')
	,(1000000,'Manager')
	,(1000001,'Clerk')
	,(1000002,'Conductor')
	,(1000003,'Manager')
	,(1000004, 'Administrator')
	
GO

print '' print '*** Creating TrainSchedule table'
GO

CREATE TABLE [dbo].[TrainSchedule]
(
	 [TrainScheduleID]								[int]		IDENTITY(1000000,1) NOT NULL
	,[CreationDate]									[datetime]						NOT NULL
	,-- The employee creating the schedule
	 [EmployeeID]									[int]							NOT NULL
	,[StartDate]									[datetime]						NOT NULL
	,[Active]										[bit]							NOT NULL
		DEFAULT 1
	,CONSTRAINT		[pk_TrainScheduleID]			PRIMARY KEY		([TrainScheduleID] ASC)
	,CONSTRAINT		[fk_EmployeeID_TrainSchedule]	FOREIGN KEY		([EmployeeID])
		REFERENCES	[Employee]([EmployeeID])
)
GO

print '' print'*** Creating sample TrainSchedule record'
GO

INSERT INTO [dbo].[TrainSchedule]
	([CreationDate],[EmployeeID],[StartDate],[Active])
	VALUES
	(2019-12-01,1000000,2019-12-01,0),
	(2019-12-02,1000000,2019-12-02,1)
GO

print '' print'*** Creating TrainScheduleLine table'
GO

CREATE TABLE [dbo].[TrainScheduleLine]
(
	 [TrainScheduleLineID]							[int]		IDENTITY(1000000,1)		NOT NULL
	,[RouteLineID]									[int]								NOT NULL
	,[ArrivalTime]									[datetime]							NOT NULL
	,[TrainScheduleID]								[int]								NOT NULL
	,CONSTRAINT		[pk_TrainScheduleLineID]		PRIMARY KEY		([TrainScheduleLineID] ASC)
	,CONSTRAINT		[fk_RouteLineID]					FOREIGN KEY		([RouteLineID])
		REFERENCES [RouteLine]([RouteLineID])
)
GO

print '' print '*** Creating sample TrainScheduleLine records'
GO

INSERT INTO [dbo].[TrainScheduleLine]
	([RouteLineID],[ArrivalTime],[TrainScheduleID])
	VALUES
	 (1000002,'3/9/2020 8:43:02 AM',1000001)
	,(1000003,'3/9/2020 8:58:02 AM',1000001)
	,(1000004,'3/9/2020 9:13:02 AM',1000001)
	,(1000005,'3/9/2020 9:28:02 AM',1000001)
	,(1000006,'3/9/2020 9:43:02 AM',1000001)
	,(1000007,'3/9/2020 9:58:02 AM',1000001)
	,(1000000,'3/9/2020 10:28:02 AM',1000001)
	,(1000001,'3/9/2020 10:43:02 AM',1000001)
	,(1000002,'3/9/2020 10:58:02 AM',1000001)
	,(1000003,'3/9/2020 11:13:02 AM',1000001)
	,(1000004,'3/9/2020 11:28:02 AM',1000001)
	,(1000005,'3/9/2020 11:43:02 AM',1000001)
	,(1000006,'3/9/2020 11:58:02 AM',1000001)
	,(1000007,'3/9/2020 12:13:02 PM',1000001)
	,(1000000,'3/9/2020 12:43:02 PM',1000001)
	,(1000001,'3/9/2020 12:58:02 PM',1000001)
	,(1000002,'3/9/2020 1:13:02 PM',1000001)
	,(1000003,'3/9/2020 1:28:02 PM',1000001)
	,(1000004,'3/9/2020 1:43:02 PM',1000001)
	,(1000005,'3/9/2020 1:58:02 PM',1000001)
	,(1000006,'3/9/2020 2:13:02 PM',1000001)
	,(1000007,'3/9/2020 2:28:02 PM',1000001)
	,(1000000,'3/9/2020 2:58:02 PM',1000001)
	,(1000001,'3/9/2020 3:13:02 PM',1000001)
	,(1000002,'3/9/2020 3:28:02 PM',1000001)
	,(1000003,'3/9/2020 3:43:02 PM',1000001)
	,(1000004,'3/9/2020 3:58:02 PM',1000001)
	,(1000005,'3/9/2020 4:13:02 PM',1000001)
	,(1000006,'3/9/2020 4:28:02 PM',1000001)
	,(1000007,'3/9/2020 4:43:02 PM',1000001)
	,(1000000,'3/9/2020 5:13:02 PM',1000001)
	,(1000001,'3/9/2020 5:28:02 PM',1000001)
	,(1000002,'3/9/2020 5:43:02 PM',1000001)
	,(1000003,'3/9/2020 5:58:02 PM',1000001)
	,(1000004,'3/9/2020 6:13:02 PM',1000001)
	,(1000005,'3/9/2020 6:28:02 PM',1000001)
	,(1000006,'3/9/2020 6:43:02 PM',1000001)
	,(1000007,'3/9/2020 6:58:02 PM',1000001)
	,(1000000,'3/9/2020 7:28:02 PM',1000001)
	,(1000001,'3/9/2020 7:43:02 PM',1000001)
	,(1000002,'3/9/2020 7:58:02 PM',1000001)
	,(1000003,'3/9/2020 8:13:02 PM',1000001)
	,(1000004,'3/9/2020 8:28:02 PM',1000001)
	,(1000005,'3/9/2020 8:43:02 PM',1000001)
	,(1000006,'3/9/2020 8:58:02 PM',1000001)
	,(1000007,'3/9/2020 9:13:02 PM',1000001)
	,(1000000,'3/9/2020 9:43:02 PM',1000001)
	,(1000001,'3/9/2020 9:58:02 PM',1000001)
	,(1000008,'3/9/2020 8:43:02 AM',1000001)
	,(1000009,'3/9/2020 9:05:02 AM',1000001)
	,(1000010,'3/9/2020 9:20:02 AM',1000001)
	,(1000011,'3/9/2020 9:35:02 AM',1000001)
	,(1000012,'3/9/2020 9:57:02 AM',1000001)
	,(1000013,'3/9/2020 10:12:02 AM',1000001)
	,(1000017,'3/9/2020 8:43:02 AM',1000001)
	,(1000018,'3/9/2020 9:05:02 AM',1000001)
	,(1000019,'3/9/2020 9:20:02 AM',1000001)
	,(1000014,'3/9/2020 9:50:02 AM',1000001)
	,(1000015,'3/9/2020 10:12:02 AM',1000001)
	,(1000016,'3/9/2020 10:22:02 AM',1000001)

--  STORED PROCEDURES  --

print'' print'*** Creating procedure sp_select_scheduleline_by_stationid'
GO

CREATE PROCEDURE [sp_select_scheduleline_by_stationid]
(
	@StationID						[int]
)
AS
BEGIN
	SELECT [TrainScheduleLineID],[ArrivalTime],[TrainScheduleLine].[RouteLineID]
	,[RouteLine].[RouteID],[TrainSchedule].[TrainScheduleID],[StationName],[TrainName],[TrainID]
	FROM [dbo].[TrainScheduleLine]
	JOIN [TrainSchedule] ON [TrainSchedule].[TrainScheduleID] = [TrainScheduleLine].[TrainScheduleID]
	JOIN [RouteLine] ON [TrainScheduleLine].[RouteLineID] = [RouteLine].[RouteLineID]
	JOIN [Train] ON [Train].[RouteID] = [RouteLine].[RouteID]
	JOIN [Station] ON [RouteLine].[StartStationID] = [Station].[StationID]
	WHERE [StartStationID] = @StationID
END
GO


print'' print '*** Creating sp_authenticate_user'
GO

CREATE PROCEDURE [sp_authenticate_user]
(

	 @Email			[nvarchar](250)
	,@PasswordHash	[nvarchar](100)
)
AS
BEGIN
	SELECT  COUNT([EmployeeID])
	FROM 	[dbo].[Employee]
	WHERE	[Email] = @Email
	AND		[PasswordHash]	= @PasswordHash
	AND		[Active] = 1
END
GO

print '' print'*** Creating procedure sp_new_customer'
GO
--Returns @@IDENTITY

CREATE PROCEDURE [sp_new_customer]
(
	 @Email			[nvarchar](150)
	,@FirstName		[nvarchar](150)
	,@LastName		[nvarchar](150)
)
AS
BEGIN
	INSERT INTO [dbo].[Customer]
	([Email],[FirstName],[LastName])
	VALUES
	(@Email,@FirstName,@LastName)
	SELECT @@IDENTITY
END
GO

print '' print'*** Creating procedure sp_insert_reservation_full'
GO

CREATE PROCEDURE [sp_insert_reservation_full]
(
	 @CustomerID			[int]
	,@EmployeeID			[int]
    ,@TravelDate			[datetime]
    ,@StationStart			[int]
	,@StationEnd			[int]
)
AS
BEGIN
	INSERT INTO [dbo].[Reservation]
    ([CustomerID],[EmployeeID],[TravelDate],[StationStart],[StationEnd])
    VALUES
    (@CustomerID,@EmployeeID,@TravelDate,@StationStart,@StationEnd)
END
GO

print '' print'*** Creating procedure sp_insert_reservation_no_customerid'
GO

CREATE PROCEDURE [sp_insert_reservation_no_customerid]
(
	 @EmployeeID			[int]
    ,@TravelDate			[datetime]
    ,@StationStart			[int]
	,@StationEnd			[int]
)
AS
BEGIN
	INSERT INTO [dbo].[Reservation]
    ([EmployeeID],[TravelDate],[StationStart],[StationEnd])
    VALUES
    (@EmployeeID,@TravelDate,@StationStart,@StationEnd)
END
GO

print '' print '*** Creating procedure sp_select_employee_by_email'
GO

CREATE PROCEDURE [sp_select_employee_by_email]
(
	 @Email					[nvarchar](250)
)
AS
BEGIN
	SELECT [EmployeeID],[FirstName],[LastName],[Email],[PhoneNumber],[Active],[PasswordHash]
	FROM [dbo].[Employee]
	WHERE [Email] = @Email
END
GO

print '' print '*** Creating procedure sp_select_roles_by_employeeid'
GO

CREATE PROCEDURE [sp_select_roles_by_employeeid]
(
	@EmployeeID				[int]
)
AS
BEGIN
	SELECT [RoleID]
	FROM [dbo].[EmployeeRole]
	WHERE [EmployeeID] = @EmployeeID
END
GO

print '' print '*** Creating procedure sp_select_employee_by_active'
GO

CREATE PROCEDURE [sp_select_employee_by_active]
(
	@ACTIVE					[bit]
)
AS
BEGIN
	SELECT [EmployeeID],[FirstName],[LastName],[Email],[PhoneNumber],[Active],[PasswordHash]
	FROM [dbo].[Employee]
	WHERE [Active] = @Active
END
GO

print '' print '*** Creating procedure sp_update_password'
GO

CREATE PROCEDURE [sp_update_password]
(
	 @EmployeeID				[int]
	,@OldPasswordHash			[nvarchar](100)
	,@NewPasswordHash			[nvarchar](100)
)
AS
BEGIN
	UPDATE [dbo].[Employee]
	SET 	[PasswordHash] = @NewPasswordHash
	WHERE 	[EmployeeID] = @EmployeeID
	AND 	[PasswordHash] = @OldPasswordHash
	AND 	[Active] = 1
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating procedure sp_count_customer_by_email'
GO

CREATE PROCEDURE [sp_count_customer_by_email]
(
	@Email				[nvarchar](250)
)
AS
BEGIN
	SELECT COUNT([customerID])
	FROM [dbo].[Customer]
	WHERE [Email] = @Email
END
GO

print '' print '*** Creating procedure sp_select_customer_by_email'
GO

CREATE PROCEDURE [sp_select_customer_by_email]
(
	@Email				[nvarchar](250)
)
AS
BEGIN
	SELECT [CustomerID],[FirstName],[LastName]
	FROM [dbo].[Customer]
	WHERE [Email] = @Email
END
GO

print '' print'*** Creating procedure sp_update_customer'
GO

CREATE PROCEDURE [sp_update_customer]
(
	 @CustomerID				[int]
	,@OldEmail					[nvarchar](250)
	,@OldFirstName				[nvarchar](150)
	,@OldLastName				[nvarchar](150)
	
	,@NewEmail					[nvarchar](250)
	,@NewFirstName				[nvarchar](150)
	,@NewLastName				[nvarchar](150)
)
AS
BEGIN
	UPDATE [dbo].[Customer]
	SET [Email] = @NewEmail
	,[FirstName] = @NewFirstName
	,[LastName] = @NewLastName
	WHERE [customerID] = @CustomerID
	AND [Email] = @OldEmail
	AND [FirstName] = @OldFirstName
	AND [LastName] = @OldLastName
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating procedure sp_select_all_station'
GO

CREATE PROCEDURE [sp_select_all_station_by_active]
(
	@Active				[bit]
)
AS
BEGIN
	SELECT [StationID],[StationName],[StationType]
    FROM [dbo].[Station]
    WHERE [Active] = @Active;
END
GO

print '' print '*** Creating procedure sp_insert_Station'
GO

CREATE PROCEDURE [sp_insert_station]
(
	 @StationName				[nvarchar](150)
    ,@StationType				[nvarchar](200)
)
AS
BEGIN
	INSERT INTO [dbo].[Station]
	([StationName],[StationType])
	VALUES
    (@StationName,@StationType);
    SELECT @@IDENTITY;
END
GO

print '' print '*** Creating procedure sp_update_station'
GO

CREATE PROCEDURE [sp_update_station]
(
	 @StationID					[int]
    ,@OldStationName			[nvarchar](150)
    ,@OldStationType			[nvarchar](200)
    
    ,@NewStationName			[nvarchar](150)
    ,@NewStationType			[nvarchar](200)
)
AS
BEGIN
	UPDATE Station
    SET  [StationName] = @NewStationName, [StationType] = @NewStationType
    WHERE [StationID] = @StationID
    AND [StationName] = @OldStationName
    AND [StationType] = @OldStationType
	RETURN @@ROWCOUNT
END
GO

print '' print'*** Creating procedure sp_active_station'
GO

CREATE PROCEDURE [sp_active_station]
(
	 @StationID				[int]
    ,@Active				[bit]
)
AS
BEGIN
	UPDATE [dbo].[Station]
    SET [Active] = @Active
    WHERE [StationID] = @StationID
	RETURN @@ROWCOUNT
END
GO

print '' print'*** Creating procedure sp_select_station_by_name'
GO

CREATE PROCEDURE [sp_select_station_by_name]
(
	@StationName				[nvarchar](150)
)
AS
BEGIN
	SELECT [StationID],[StationName],[StationType]
	FROM [dbo].[Station]
	WHERE [StationName] = @StationName
END
GO

print '' print'*** Creating procedure sp_select_station_by_id'
GO

CREATE PROCEDURE [sp_select_station_by_id]
(
	@StationID				[int]
)
AS
BEGIN
	SELECT [StationID],[StationName],[StationType]
	FROM [dbo].[Station]
	WHERE [StationID] = @StationID
END
GO
	
print '' print '*** Creating procedure sp_select_all_route_by_active'
GO

CREATE PROCEDURE [sp_select_all_route_by_active]
(
	@Active					[bit]
)
AS
BEGIN
	SELECT [RouteID],[TotalTimeMin]
	FROM [dbo].[Route]
	WHERE [Active] = @Active
END
GO

print '' print '*** Creating procedure sp_select_routelineview_by_routeid'
GO

CREATE PROCEDURE [sp_select_routeline_by_routeid]
(
	@RouteID						[int]
)
AS
BEGIN
	SELECT [RouteLineID],[StartStationID],[StationName],[TimeToNext],[RoutePosition]
	FROM [dbo].[RouteLine] JOIN [Station]
	ON [RouteLine].[StartStationID] = [Station].[StationID]
	WHERE [RouteID] = @RouteID
	ORDER BY [RoutePosition]
END
GO

print '' print '*** Creating procedure sp_select_all_seat_type'
GO

CREATE PROCEDURE [sp_select_all_seat_type]
AS
BEGIN
	SELECT [SeatTypeID],[Description],[Price],[Active]
	FROM [dbo].[SeatType]
	ORDER BY [Price]
END
GO

print '' print '*** Creating procedure sp_select_all_seat_type_by_active'
GO

CREATE PROCEDURE [sp_select_all_seat_type_by_active]
(
	@Active						[bit]
)
AS
BEGIN
	SELECT [SeatTypeID],[Description],[Price]
	FROM [dbo].[SeatType]
	WHERE [Active] = @Active
	ORDER BY [Price]
END
GO

print '' print '*** Creating procedure sp_update_seat_type'
GO

CREATE PROCEDURE [sp_update_seat_type]
(
	 @SeatTypeID			[int]
	,@OldDescription		[nvarchar](50)
	,@OldPrice				[money]
	
	,@NewDescription		[nvarchar](50)
	,@NewPrice				[money]
)
AS
BEGIN
	UPDATE [dbo].[SeatType]
	SET [Description] = @NewDescription, [Price] = @NewPrice
	WHERE [SeatTypeID] = @SeatTypeID
	AND [Description] = @OldDescription
	AND [Price] = @OldPrice
	RETURN @@ROWCOUNT
END
GO

print '' print'*** Creating procedure sp_select_all_seat_by_available'
GO

CREATE PROCEDURE [sp_select_all_seat_by_available]
(
	@Available					[bit]
)
AS
BEGIN
	SELECT [SeatID],[SeatListID],[SeatTypeID],[Available],[Active]
	FROM [dbo].[Seat]
	WHERE [Available] = @Available
END
GO

print '' print'*** Creating procedure sp_select_all_seat'
GO

CREATE PROCEDURE [sp_select_all_seat]
AS
BEGIN
	SELECT [SeatID],[Seat].[SeatTypeID],[Seat].[SeatListID],[SeatType].[Description],[Price],[Available],[Seat].[Active],[TrainName],[Train].[TrainID]
	FROM [Seat] 
	JOIN [SeatType]		ON		[Seat].[SeatTypeID] = [SeatType].[SeatTypeID]
	JOIN [SeatList]  	ON		[Seat].[SeatListID] = [SeatList].[SeatListID]
	JOIN [TrainCar] 	ON 		[SeatList].[TrainCarID] = [TrainCar].[TrainCarID]
	JOIN [CarList]  	ON		[TrainCar].[CarListID] = [CarList].[CarListID]
	JOIN [Train]		ON		[CarList].[TrainID] = [Train].[TrainID]
	WHERE [Seat].[Active] = 1
END
GO

print '' print'*** Creating procedure sp_select_seat_view_by_id'
GO

CREATE PROCEDURE [sp_select_seat_view_by_id]
(
	@SeatID							[int]
)
AS
BEGIN
	SELECT [SeatID],[Seat].[SeatTypeID],[Seat].[SeatListID],[SeatType].[Description],[Price],[Available],[Seat].[Active],[TrainName],[Train].[TrainID]
	FROM [Seat] 
	JOIN [SeatType]		ON		[Seat].[SeatTypeID] = [SeatType].[SeatTypeID]
	JOIN [SeatList]  	ON		[Seat].[SeatListID] = [SeatList].[SeatListID]
	JOIN [TrainCar] 	ON 		[SeatList].[TrainCarID] = [TrainCar].[TrainCarID]
	JOIN [CarList]  	ON		[TrainCar].[CarListID] = [CarList].[CarListID]
	JOIN [Train]		ON		[CarList].[TrainID] = [Train].[TrainID]
	WHERE [SeatID] = @SeatID
END
GO

print '' print '*** Creating procedure sp_select_all_seat_by_seatlistid' 
GO

CREATE PROCEDURE [sp_select_all_seat_by_seatlistid]
(
	@SeatListID 							[int]
)
AS
BEGIN
	SELECT [SeatID],[Seat].[SeatTypeID],[Seat].[SeatListID],[SeatType].[Description],[Price],[Available],[Seat].[Active],[TrainName],[Train].[TrainID]
	FROM [Seat] 
	JOIN [SeatType]		ON		[Seat].[SeatTypeID] = [SeatType].[SeatTypeID]
	JOIN [SeatList]  	ON		[Seat].[SeatListID] = [SeatList].[SeatListID]
	JOIN [TrainCar] 	ON 		[SeatList].[TrainCarID] = [TrainCar].[TrainCarID]
	JOIN [CarList]  	ON		[TrainCar].[CarListID] = [CarList].[CarListID]
	JOIN [Train]		ON		[CarList].[TrainID] = [Train].[TrainID]
	WHERE [SeatList].[SeatListID] = @SeatListID
END
GO

print '' print'*** Creating procedure sp_select_seat_type_detail_by_seattypeid'
GO

CREATE PROCEDURE [sp_select_seat_type_detail_by_seattypeid]
(
	@SeatTypeID							[int]
)
AS
BEGIN
	SELECT 	[Description],[Price]
	FROM	[dbo].[SeatType]
	WHERE	[SeatTypeID] = @SeatTypeID
END
GO	

print '' print'*** Creating procedure sp_select_available_seat_by_seattypeid'
GO

CREATE PROCEDURE [sp_select_available_seat_by_seattypeid]
(
	@SeatTypeID					[int]
)
AS
BEGIN
	SELECT [SeatID],[SeatListID]
	FROM [dbo].[Seat]
	WHERE [SeatTypeID] = @SeatTypeID
	AND [Available] = 1
END
GO

print '' print '*** Creating procedure sp_update_seat'
GO

CREATE PROCEDURE [sp_update_seat]
(
	 @SeatID						[int]
	,@OldSeatListID					[int]
	,@OldSeatTypeID					[int]
	,@OldAvailable					[bit]
	,@OldActive						[bit]
	
	,@NewSeatListID					[int]
	,@NewSeatTypeID					[int]
	,@NewAvailable					[bit]
	,@NewActive						[bit]
)
AS
BEGIN
	UPDATE [dbo].[Seat]
	SET [SeatListID] = @NewSeatListID,[SeatTypeID] = @NewSeatTypeID, [Available] = @NewAvailable, [Active] = @NewActive
	WHERE [SeatID] = @SeatID
	AND [SeatListID] = @OldSeatListID
	AND [SeatTypeID] = @OldSeatTypeID
	AND [Available] = @OldAvailable
	AND [Active] = @OldActive
	RETURN @@ROWCOUNT
END
GO
	
print '' print'*** Creating procedure sp_insert_seat'
GO

CREATE PROCEDURE [sp_insert_seat]
(
	 @SeatListID						[int]
	,@SeatTypeID						[int]
)
AS
BEGIN
	INSERT INTO	[dbo].[Seat]
	([SeatListID],[SeatTypeID])
	VALUES
	(@SeatListID,@SeatTypeID)
END
GO

print '' print'*** Creating procedure sp_insert_seat_type'
GO

CREATE PROCEDURE [sp_insert_seat_type]
(
	 @Description					[nvarchar](50)
	,@Price							[money]
)
AS
BEGIN
	INSERT INTO [dbo].[SeatType]
	([Description],[Price])
	VALUES
	(@Description,@Price)
END
GO

print '' print'*** Creating procedure sp_delete_seat_by_id'
GO

CREATE PROCEDURE [sp_delete_seat_by_id]
(
	@SeatID						[int]
)
AS
BEGIN
	DELETE FROM [dbo].[Seat]
	WHERE [SeatID] = @SeatID
END
GO

print '' print'*** Creating procedure sp_select_seattypeid_by_description'
GO

CREATE PROCEDURE [sp_select_seat_type_by_description]
(
	@Description					[nvarchar](50)
)
AS
BEGIN
	SELECT [SeatTypeID]
	FROM [dbo].[SeatType]
	WHERE [Description] = @Description
END
GO

print '' print'*** Creating procedure sp_update_route'
GO

CREATE PROCEDURE [sp_update_route]
(
	 @RouteID						[int]
	,@OldTotalTime					[int]
	,@OldActive						[bit]
	
	,@NewTotalTime					[int]
	,@NewActive						[bit]
)
AS
BEGIN
	UPDATE [dbo].[Route]
	SET [TotalTimeMin] = @NewTotalTime
	,[Active] = @NewActive
	WHERE [RouteID] = @RouteID
	AND [TotalTimeMin] = @OldTotalTime
	AND [Active] = @OldActive
END
GO

print '' print'*** Creating procedure sp_insert_route'
GO

CREATE PROCEDURE [sp_insert_route]
(
	 @TotalTime						[int]
)
AS
BEGIN
	INSERT INTO [dbo].[Route]
	([TotalTimeMin])
	VALUES
	(@TotalTime)
END
GO

print '' print'*** Creating procedure sp_update_routeline'
GO

CREATE PROCEDURE [sp_update_routeline]
(
	 @RouteLineID					[int]
	,@OldRouteID					[int]
	,@OldStartStationID				[int]
	,@OldTimeToNext					[int]
	,@OldRoutePosition				[int]
	
	,@NewRouteID					[int]
	,@NewStartStationID				[int]
	,@NewTimeToNext					[int]
	,@NewRoutePosition				[int]
)
AS
BEGIN
	UPDATE [dbo].[RouteLine]
	SET [RouteID] = @NewRouteID
	,[StartStationID] = @NewStartStationID
	,[TimeToNext] = @NewTimeToNext
	,[RoutePosition] = @NewRoutePosition
	WHERE [RouteLineID] = @RouteLineID
	AND [RouteID] = @OldRouteID
	AND [StartStationID] = @OldStartStationID
	AND [TimeToNext] = @OldTimeToNext
	AND [RoutePosition] = @OldRoutePosition
END
GO

print '' print'*** Creating procedure sp_insert_routeline'
GO

CREATE PROCEDURE [sp_insert_routeline]
(
	 @RouteID						[int]
	,@StartStationID				[int]
	,@TimeToNext					[int]
	,@RoutePosition					[int]
)
AS
BEGIN
	INSERT INTO [dbo].[RouteLine]
	([RouteID],[StartStationID],[TimeToNext],[RoutePosition])
	VALUES
	(@RouteID,@StartStationID,@TimeToNext,@RoutePosition)
END
GO

print '' print '*** Creating procedure sp_select_trainschedule_by_id'
GO

CREATE PROCEDURE [sp_select_trainschedule_by_id]
(
	@TrainScheduleID					[int]
)
AS
BEGIN
	SELECT [CreationDate],[EmployeeID],[StartDate],[Active]
	FROM [dbo].[TrainSchedule]
	WHERE [TrainScheduleID] = @TrainScheduleID
END 
GO

print '' print '*** Creating procedure sp_select_trainschedule'
GO

CREATE PROCEDURE [sp_select_trainschedule]
AS
BEGIN
	SELECT [TrainScheduleID],[CreationDate],[EmployeeID],[StartDate]
	FROM [dbo].[TrainSchedule]
	WHERE [Active] = 1
END 
GO


print '' print'*** Creating procedure sp_insert_trainschedule'
GO

CREATE PROCEDURE [sp_insert_trainschedule]
(
	 @CreationDate						[datetime]
	,@EmployeeID						[int]
	,@StartDate							[datetime]
	,@NewScheduleID						[int] OUT
)
AS
BEGIN
	UPDATE [dbo].[TrainSchedule]
	SET [Active] = 0
	WHERE [Active] = 1
	INSERT INTO [dbo].[TrainSchedule]
	([CreationDate],[EmployeeID],[StartDate])
	VALUES
	(@CreationDate,@EmployeeID,@StartDate)
	SET @NewScheduleID = SCOPE_IDENTITY()
END
GO

print '' print'*** Creating procedure sp_insert_trainscheduleline'
GO

CREATE PROCEDURE [sp_insert_trainscheduleline]
(
	 @RouteLineID					[int]
	,@ArrivalTime					[datetime]
	,@TrainScheduleID				[int]
)
AS
BEGIN
	INSERT INTO [dbo].[TrainScheduleLine]
	([RouteLineID],[ArrivalTime],[TrainScheduleID])
	VALUES
	(@RouteLineID,@ArrivalTime,@TrainScheduleID)
END
GO

print '' print'*** Creating procedure sp_select_all_trainscheduleline_by_routelineID'
GO

CREATE PROCEDURE [sp_select_all_trainscheduleline_by_routeLineID]
(
	@RouteLineID				[int]
)
AS
BEGIN
	SELECT [TrainScheduleLineID],[ArrivalTime],[TrainSchedule].[TrainScheduleID]
	,[RouteLine].[RouteID],[StartStationID],[StationName],[TrainName],[TrainID]
	FROM [TrainScheduleLine] JOIN [TrainSchedule] 
	ON [TrainSchedule].[TrainScheduleID] = [TrainScheduleLine].[TrainScheduleID]
	JOIN [RouteLine] ON [TrainScheduleLine].[RouteLineID] = [RouteLine].[RouteLineID]
	JOIN [Train] ON [Train].[RouteID] = [RouteLine].[RouteID]
	JOIN [Station] ON [RouteLine].[StartStationID] = [Station].[StationID]
	WHERE [RouteLine].[RouteLineID] = @RouteLineID
	AND [TrainSchedule].[Active] = 1
END
GO

print '' print'*** Creating procedure sp_select_all_trainscheduleline_by_trainscheduleid'
GO

CREATE PROCEDURE [sp_select_all_trainscheduleline_by_trainscheduleid]
(
	@TrainScheduleID				[int]
)
AS
BEGIN
	SELECT [TrainScheduleLineID],[ArrivalTime],[TrainScheduleLine].[RouteLineID]
	,[RouteLine].[RouteID],[StartStationID],[StationName],[TrainName],[TrainID]
	FROM [TrainScheduleLine] 
	JOIN [TrainSchedule] ON [TrainSchedule].[TrainScheduleID] = [TrainScheduleLine].[TrainScheduleID]
	JOIN [RouteLine] ON [TrainScheduleLine].[RouteLineID] = [RouteLine].[RouteLineID]
	JOIN [Train] ON [Train].[RouteID] = [RouteLine].[RouteID]
	JOIN [Station] ON [RouteLine].[StartStationID] = [Station].[StationID]
	WHERE [TrainSchedule].[TrainScheduleID] = @TrainScheduleID
	AND [TrainSchedule].[Active] = 1
END
GO


print '' print'*** Creating sp_select_train_by_id'
GO

CREATE PROCEDURE [sp_select_train_by_id]
(
	 @TrainID						[int]
)
AS
BEGIN
	SELECT [TrainName],[RouteID],[Active]
	FROM [dbo].[Train]
	WHERE [TrainID] = @TrainID
END
GO

print '' print'*** Creating sp_select_train_by_name'
GO

CREATE PROCEDURE [sp_select_train_by_name]
(
	 @TrainName						[nvarchar](100)
)
AS
BEGIN
	SELECT [TrainID],[RouteID],[Active]
	FROM [dbo].[Train]
	WHERE [TrainName] = @TrainName
END
GO

print '' print'*** Creating procedure sp_select_all_train_by_active'
GO

CREATE PROCEDURE [sp_select_all_train_by_active]
(
	@Active								[bit]
)
AS
BEGIN
	SELECT [TrainID],[TrainName],[RouteID]
	FROM [Train]
	WHERE [Active] = @Active
END
GO


print '' print '*** Creating procedure sp_insert_train'
GO

CREATE PROCEDURE [sp_insert_train]
(
	 @TrainName							[nvarchar](100)
	,@RouteID							[int]
)
AS
BEGIN
	INSERT INTO [dbo].[Train]
	([TrainName],[RouteID])
	VALUES
	(@TrainName,@RouteID)
END
GO

print '' print '*** Creating procedure sp_update_train'
GO

CREATE PROCEDURE [sp_update_train]
(
	 @TrainID						[int]
	,@OldTrainName					[nvarchar](100)
	,@OldRouteID					[int]
	,@OldActive						[bit]
	
	,@NewTrainName					[nvarchar](100)
	,@NewRouteID					[int]
	,@NewActive						[bit]
)
AS
BEGIN
	UPDATE [dbo].[Train]
	SET [TrainName] = @NewTrainName,[RouteID] = @NewRouteID, [Active] = @NewActive
	WHERE [TrainID] = @TrainID
	AND	[TrainName] = @OldTrainName
	AND	[RouteID] = @OldRouteID
	AND	[Active] = @OldActive
END
GO

print '' print'*** Creating procedure sp_select_all_traincar_by_trainid'
GO

CREATE PROCEDURE [sp_select_all_traincar_by_trainid]
(
	@TrainID							[int]
)
AS
BEGIN
	SELECT [TrainCar].[TrainCarID],[TrainCar].[CoachTypeID],[CoachType].[Description],[TrainCar].[CarListID],[SeatListID]
	FROM [CarList] 
	JOIN [TrainCar] ON [CarList].[CarListID] = [TrainCar].[CarListID]
	JOIN [CoachType] ON [TrainCar].[CoachTypeID] = [CoachType].[CoachTypeID]
	JOIN [SeatList] ON  [TrainCar].[TrainCarID] = [SeatList].[TrainCarID]
	WHERE [TrainID] = @TrainID
	AND [TrainCar].[Active] = 1
END
GO

print '' print'*** Creating procedure sp_deactivate_traincar'
GO

CREATE PROCEDURE [sp_deactivate_traincar]
(
	@TrainCarID							[int]
)
AS
BEGIN
	UPDATE [dbo].[TrainCar]
	SET		[Active] = 0
	WHERE 	[TrainCarID] = @TrainCarID
END
GO

print '' print'*** Creating procedure sp_insert_traincar'
GO

CREATE PROCEDURE [sp_insert_traincar]
(
	 @CoachTypeID				[int]
	,@CarListID					[int]
)
AS
BEGIN
	INSERT INTO [dbo].[TrainCar]
	([CoachTypeID],[CarListID])
	VALUES
	(@CoachTypeID,@CarListID)
END
GO

print '' print'*** Creating procedure sp_update_traincar'
GO

CREATE PROCEDURE [sp_update_traincar]
(
	 @TrainCarID						[int]
	,@OldCoachTypeID					[int]
	,@OldCarListID						[int]
	
	,@NewCoachTypeID					[int]
	,@NewCarListID						[int]
)
AS
BEGIN
	UPDATE [dbo].[TrainCar]
	SET [CoachTypeID] = @NewCoachTypeID, [CarListID] = @NewCarListID
	WHERE [TrainCarID] = @TrainCarID
	AND [CoachTypeID] = @OldCoachTypeID
	AND [CarListID] = @OldCarListID
END
GO

print '' print '*** Creating procedure sp_select_distinct_stationtypes'
GO

CREATE PROCEDURE [sp_select_distinct_stationtypes]
AS
BEGIN
	SELECT DISTINCT [StationType]
	FROM [dbo].[Station]
END
GO

print '' print'*** Creating procedure sp_update_employee'
GO

CREATE PROCEDURE [sp_update_employee]
(
	 @EmployeeID						[int]
	,@OldFirstName						[nvarchar](150)
	,@OldLastName						[nvarchar](150)
	,@OldEmail							[nvarchar](250)
	,@OldPhoneNumber					[nvarchar](11)
	
	,@NewFirstName						[nvarchar](150)
	,@NewLastName						[nvarchar](150)
	,@NewEmail							[nvarchar](250)
	,@NewPhoneNumber					[nvarchar](11)
)
AS
BEGIN
	UPDATE [dbo].[Employee]
	SET [FirstName] = @NewFirstName, [LastName] = @NewLastName
	,[Email] = @NewEmail,[PhoneNumber] = @NewPhoneNumber
	WHERE [EmployeeID] = @EmployeeID
	AND [FirstName] = @OldFirstName
	AND	[LastName] = @OldLastName
	AND [Email] = @OldEmail 
	AND [PhoneNumber] = @OldPhoneNumber
END
GO

print '' print'*** Creating procedure sp_insert_employee'
GO

CREATE PROCEDURE [sp_insert_employee]
(
	 @FirstName					[nvarchar](150)
	,@LastName					[nvarchar](150)
	,@Email						[nvarchar](250)
	,@PhoneNumber				[nvarchar](11)
)
AS
BEGIN
	INSERT INTO [dbo].[Employee]
	([FirstName],[LastName],[Email],[PhoneNumber])
	VALUES
	(@FirstName,@LastName,@Email,@PhoneNumber)
END
GO

print '' print '*** Creating procedure sp_insert_employeerole'
GO

CREATE PROCEDURE [sp_insert_employeerole]
(
	 @EmployeeID				[int]
	,@RoleID					[nvarchar](200)
)
AS
BEGIN
	INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID],[RoleID])
	VALUES
	(@EmployeeID,@RoleID)
END
GO

print '' print '*** Creating procedure sp_select_all_roles'
GO

CREATE PROCEDURE [sp_select_all_roles]
AS
BEGIN
	SELECT [RoleID]
	FROM [Role]
END
GO

print '' print'*** Creating procedure sp_set_employee_active'
GO

CREATE PROCEDURE [sp_set_employee_active]
(
	 @EmployeeID				[int]
	,@Active					[bit]
)
AS
BEGIN
	UPDATE [dbo].[Employee]
	SET [Active] = @Active
	WHERE [EmployeeID] = @EmployeeID
END
GO

print '' print'*** Creating procedure sp_delete_employeerole'
GO

CREATE PROCEDURE [sp_delete_employeerole]
(
	 @EmployeeID				[int]
	,@RoleID					[nvarchar](200)
)
AS
BEGIN
	DELETE FROM [dbo].[EmployeeRole]
	WHERE [EmployeeID] = @EmployeeID
	AND	[RoleID] = @RoleID
END
GO

print '' print '*** Creating procedure sp_count_admin'
GO

CREATE PROCEDURE [sp_count_admin]
AS
BEGIN
	SELECT COUNT([EmployeeID])
	FROM [dbo].[EmployeeRole]
	WHERE [RoleID] = "Administrator"
END
GO