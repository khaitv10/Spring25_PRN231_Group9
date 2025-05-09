--USE [master]
--GO
/****** Object:  Database [CVSTSystemDB]    Script Date: 2/7/2025 11:50:41 PM ******/
--CREATE DATABASE [CVSTSystemDB]
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'CVSTSystemDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.VUKHAI\MSSQL\DATA\CVSTSystemDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
-- LOG ON 
--( NAME = N'CVSTSystemDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.VUKHAI\MSSQL\DATA\CVSTSystemDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
-- WITH CATALOG_COLLATION = DATABASE_DEFAULT
--GO
--ALTER DATABASE [CVSTSystemDB] SET COMPATIBILITY_LEVEL = 150
--GO
--IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
--begin
--EXEC [CVSTSystemDB].[dbo].[sp_fulltext_database] @action = 'enable'
--end
GO
ALTER DATABASE [CVSTSystemDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CVSTSystemDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CVSTSystemDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CVSTSystemDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CVSTSystemDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CVSTSystemDB] SET  MULTI_USER 
GO
ALTER DATABASE [CVSTSystemDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CVSTSystemDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CVSTSystemDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CVSTSystemDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CVSTSystemDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CVSTSystemDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CVSTSystemDB] SET QUERY_STORE = OFF
GO
USE [CVSTSystemDB]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentDate] [datetime] NOT NULL,
	[Type] [varchar](50) NULL,
	[TotalPrice] [decimal](10, 2) NULL,
	[Status] [varchar](50) NULL,
	[PaymentStatus] [varchar](50) NULL,
	[ParentId] [int] NULL,
	[ChildId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentService]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentService](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](50) NULL,
	[DoseDate] [datetime] NULL,
	[AppointmentId] [int] NULL,
	[ServiceId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Child]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Child](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](255) NOT NULL,
	[DOB] [date] NOT NULL,
	[Gender] [char](1) NULL,
	[Note] [text] NULL,
	[ParentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoseRecord]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoseRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoseDate] [date] NOT NULL,
	[DoseNumber] [int] NULL,
	[Status] [varchar](50) NULL,
	[CreateAt] [datetime] NULL,
	[ServiceId] [int] NULL,
	[ChildId] [int] NULL,
	[VaccineId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoseSchedule]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoseSchedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NextDoseDate] [date] NOT NULL,
	[DoseNumber] [int] NULL,
	[Status] [varchar](50) NULL,
	[CreateAt] [datetime] NULL,
	[ServiceId] [int] NULL,
	[ChildId] [int] NULL,
	[VaccineId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [int] NULL,
	[Comment] [text] NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[AppointmentId] [int] NULL,
	[ParentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[Message] [text] NOT NULL,
	[CreateAt] [datetime] NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[PaymentMethod] [varchar](50) NOT NULL,
	[PaymentStatus] [varchar](50) NULL,
	[PaidAt] [datetime] NULL,
	[TransactionCode] [varchar](100) NULL,
	[AppointmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[Price] [decimal](10, 2) NULL,
	[TotalDoses] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateAt] [datetime] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceVaccine]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceVaccine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumberOfDose] [int] NULL,
	[VaccineId] [int] NULL,
	[ServiceId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[FullName] [varchar](255) NOT NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Password] [varchar](255) NOT NULL,
	[Status] [bit] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateAt] [datetime] NULL,
	[Role] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vaccine]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vaccine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[Origin] [varchar](255) NULL,
	[MinAge] [int] NULL,
	[MaxAge] [int] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccineStock]    Script Date: 2/7/2025 11:50:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccineStock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[VaccineId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus],  [ParentId], [ChildId]) VALUES (1, CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Vaccination', CAST(100.00 AS Decimal(10, 2)), N'Scheduled', N'Pending' , 1, 1)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus],  [ParentId], [ChildId]) VALUES (2, CAST(N'2024-08-21T00:00:00.000' AS DateTime), N'Vaccination', CAST(250.00 AS Decimal(10, 2)), N'Completed', N'Paid', 2, 2)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus],  [ParentId], [ChildId]) VALUES (3, CAST(N'2024-08-22T00:00:00.000' AS DateTime), N'Checkup', CAST(50.00 AS Decimal(10, 2)), N'Scheduled', N'Pending', 1, 3)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus],  [ParentId], [ChildId]) VALUES (4, CAST(N'2024-08-23T00:00:00.000' AS DateTime), N'Immunization', CAST(120.00 AS Decimal(10, 2)), N'Completed', N'Paid', 2, 4)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus],  [ParentId], [ChildId]) VALUES (5, CAST(N'2024-08-24T00:00:00.000' AS DateTime), N'Protection', CAST(90.00 AS Decimal(10, 2)), N'Scheduled', N'Pending', 1, 5)
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
SET IDENTITY_INSERT [dbo].[Child] ON 

INSERT [dbo].[Child] ([Id], [FullName], [DOB], [Gender], [Note], [ParentId]) VALUES (1, N'Alice Doe', CAST(N'2020-05-15' AS Date), N'F', NULL, 1)
INSERT [dbo].[Child] ([Id], [FullName], [DOB], [Gender], [Note], [ParentId]) VALUES (2, N'Bob Smith', CAST(N'2019-08-22' AS Date), N'M', NULL, 2)
INSERT [dbo].[Child] ([Id], [FullName], [DOB], [Gender], [Note], [ParentId]) VALUES (3, N'Charlie Brown', CAST(N'2021-03-10' AS Date), N'M', NULL, 1)
INSERT [dbo].[Child] ([Id], [FullName], [DOB], [Gender], [Note], [ParentId]) VALUES (4, N'Daisy White', CAST(N'2018-11-05' AS Date), N'F', NULL, 2)
INSERT [dbo].[Child] ([Id], [FullName], [DOB], [Gender], [Note], [ParentId]) VALUES (5, N'Ethan Johnson', CAST(N'2017-06-30' AS Date), N'M', NULL, 1)
SET IDENTITY_INSERT [dbo].[Child] OFF
GO
SET IDENTITY_INSERT [dbo].[DoseRecord] ON 

INSERT [dbo].[DoseRecord] ([Id], [DoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (1, CAST(N'2024-08-20' AS Date), 1, N'Completed', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 1, 1, 1)
INSERT [dbo].[DoseRecord] ([Id], [DoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (2, CAST(N'2024-08-21' AS Date), 2, N'Completed', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 2, 2, 2)
INSERT [dbo].[DoseRecord] ([Id], [DoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (3, CAST(N'2024-08-22' AS Date), 1, N'Scheduled', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 3, 3, 3)
INSERT [dbo].[DoseRecord] ([Id], [DoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (4, CAST(N'2024-08-23' AS Date), 1, N'Completed', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 4, 4, 4)
INSERT [dbo].[DoseRecord] ([Id], [DoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (5, CAST(N'2024-08-24' AS Date), 2, N'Scheduled', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 5, 5, 5)
SET IDENTITY_INSERT [dbo].[DoseRecord] OFF
GO
SET IDENTITY_INSERT [dbo].[DoseSchedule] ON 

INSERT [dbo].[DoseSchedule] ([Id], [NextDoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (1, CAST(N'2024-09-15' AS Date), 1, N'Scheduled', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 1, 1, 1)
INSERT [dbo].[DoseSchedule] ([Id], [NextDoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (2, CAST(N'2024-09-20' AS Date), 2, N'Scheduled', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 2, 2, 2)
INSERT [dbo].[DoseSchedule] ([Id], [NextDoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (3, CAST(N'2024-09-25' AS Date), 1, N'Pending', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 3, 3, 3)
INSERT [dbo].[DoseSchedule] ([Id], [NextDoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (4, CAST(N'2024-09-30' AS Date), 1, N'Scheduled', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 4, 4, 4)
INSERT [dbo].[DoseSchedule] ([Id], [NextDoseDate], [DoseNumber], [Status], [CreateAt], [ServiceId], [ChildId], [VaccineId]) VALUES (5, CAST(N'2024-10-05' AS Date), 2, N'Pending', CAST(N'2025-02-07T21:34:02.130' AS DateTime), 5, 5, 5)
SET IDENTITY_INSERT [dbo].[DoseSchedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 

INSERT [dbo].[Feedback] ([Id], [Rating], [Comment], [CreatedDate], [Status], [AppointmentId], [ParentId]) VALUES (1, 5, N'Great service!', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'Approved', 1, 1)
INSERT [dbo].[Feedback] ([Id], [Rating], [Comment], [CreatedDate], [Status], [AppointmentId], [ParentId]) VALUES (2, 4, N'Good experience.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'Approved', 2, 2)
INSERT [dbo].[Feedback] ([Id], [Rating], [Comment], [CreatedDate], [Status], [AppointmentId], [ParentId]) VALUES (3, 3, N'Average service.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'Pending', 3, 1)
INSERT [dbo].[Feedback] ([Id], [Rating], [Comment], [CreatedDate], [Status], [AppointmentId], [ParentId]) VALUES (4, 5, N'Excellent staff!', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'Approved', 4, 2)
INSERT [dbo].[Feedback] ([Id], [Rating], [Comment], [CreatedDate], [Status], [AppointmentId], [ParentId]) VALUES (5, 2, N'Needs improvement.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'Pending', 5, 1)
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([NotificationId], [Message], [CreateAt], [UserId]) VALUES (1, N'Your appointment has been confirmed.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), 1)
INSERT [dbo].[Notification] ([NotificationId], [Message], [CreateAt], [UserId]) VALUES (2, N'Your payment was successful.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), 2)
INSERT [dbo].[Notification] ([NotificationId], [Message], [CreateAt], [UserId]) VALUES (3, N'A new vaccine has been added.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), 3)
INSERT [dbo].[Notification] ([NotificationId], [Message], [CreateAt], [UserId]) VALUES (4, N'Your feedback has been received.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), 1)
INSERT [dbo].[Notification] ([NotificationId], [Message], [CreateAt], [UserId]) VALUES (5, N'New schedule available for vaccination.', CAST(N'2025-02-07T21:34:02.127' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([Id], [Amount], [PaymentMethod], [PaymentStatus], [PaidAt], [TransactionCode], [AppointmentId]) VALUES (1, CAST(100.00 AS Decimal(10, 2)), N'Credit Card', N'Paid', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'TXN12345', 1)
INSERT [dbo].[Payment] ([Id], [Amount], [PaymentMethod], [PaymentStatus], [PaidAt], [TransactionCode], [AppointmentId]) VALUES (2, CAST(250.00 AS Decimal(10, 2)), N'PayPal', N'Paid', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'TXN12346', 2)
INSERT [dbo].[Payment] ([Id], [Amount], [PaymentMethod], [PaymentStatus], [PaidAt], [TransactionCode], [AppointmentId]) VALUES (3, CAST(50.00 AS Decimal(10, 2)), N'Cash', N'Pending', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'TXN12347', 3)
INSERT [dbo].[Payment] ([Id], [Amount], [PaymentMethod], [PaymentStatus], [PaidAt], [TransactionCode], [AppointmentId]) VALUES (4, CAST(120.00 AS Decimal(10, 2)), N'Credit Card', N'Paid', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'TXN12348', 4)
INSERT [dbo].[Payment] ([Id], [Amount], [PaymentMethod], [PaymentStatus], [PaidAt], [TransactionCode], [AppointmentId]) VALUES (5, CAST(90.00 AS Decimal(10, 2)), N'Bank Transfer', N'Pending', CAST(N'2025-02-07T21:34:02.127' AS DateTime), N'TXN12349', 5)
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([Id], [Name], [Description], [Price], [TotalDoses], [CreateAt], [UpdateAt], [Status]) VALUES (1, N'Basic Vaccination', N'Covers basic vaccines for children', CAST(100.00 AS Decimal(10, 2)), 3, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, 1)
INSERT [dbo].[Service] ([Id], [Name], [Description], [Price], [TotalDoses], [CreateAt], [UpdateAt], [Status]) VALUES (2, N'Full Immunization', N'Covers all essential vaccines', CAST(250.00 AS Decimal(10, 2)), 5, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, 1)
INSERT [dbo].[Service] ([Id], [Name], [Description], [Price], [TotalDoses], [CreateAt], [UpdateAt], [Status]) VALUES (3, N'Single Dose Hepatitis B', N'Hepatitis B vaccine single dose', CAST(50.00 AS Decimal(10, 2)), 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, 1)
INSERT [dbo].[Service] ([Id], [Name], [Description], [Price], [TotalDoses], [CreateAt], [UpdateAt], [Status]) VALUES (4, N'MMR Immunization', N'Measles, Mumps, Rubella package', CAST(120.00 AS Decimal(10, 2)), 2, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, 1)
INSERT [dbo].[Service] ([Id], [Name], [Description], [Price], [TotalDoses], [CreateAt], [UpdateAt], [Status]) VALUES (5, N'DTP Protection', N'Diphtheria, Tetanus, Pertussis shots', CAST(90.00 AS Decimal(10, 2)), 3, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Service] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceVaccine] ON 

INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [VaccineId], [ServiceId]) VALUES (1, 1, 1, 1)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [VaccineId], [ServiceId]) VALUES (2, 2, 2, 2)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [VaccineId], [ServiceId]) VALUES (3, 1, 3, 3)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [VaccineId], [ServiceId]) VALUES (4, 2, 4, 4)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [VaccineId], [ServiceId]) VALUES (5, 3, 5, 5)
SET IDENTITY_INSERT [dbo].[ServiceVaccine] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (1, N'parent1@example.com', N'John Doe', N'1234567890', N'hashedpassword1', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'User')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (2, N'parent2@example.com', N'Jane Smith', N'0987654321', N'hashedpassword2', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'User')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (3, N'admin@example.com', N'Admin User', NULL, N'adminpassword', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Admin')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (4, N'doctor@example.com', N'Dr. Emily White', N'1112223333', N'hashedpassword3', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Staff')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (5, N'nurse@example.com', N'Nurse Kelly', N'4445556666', N'hashedpassword4', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Staff')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Vaccine] ON 

INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status]) VALUES (1, N'BCG', N'Tuberculosis vaccine', N'India', 0, 5, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status]) VALUES (2, N'Hepatitis B', N'Hepatitis B vaccine', N'USA', 0, 12, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status]) VALUES (3, N'Polio', N'Polio vaccine', N'UK', 0, 5, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status]) VALUES (4, N'MMR', N'Measles, Mumps, Rubella vaccine', N'Germany', 1, 12, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status]) VALUES (5, N'DTP', N'Diphtheria, Tetanus, Pertussis vaccine', N'France', 2, 7, 1)
SET IDENTITY_INSERT [dbo].[Vaccine] OFF
GO
SET IDENTITY_INSERT [dbo].[VaccineStock] ON 

INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [VaccineId]) VALUES (1, 100, CAST(N'2025-12-31' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), 1)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [VaccineId]) VALUES (2, 150, CAST(N'2026-06-30' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), 2)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [VaccineId]) VALUES (3, 200, CAST(N'2025-09-15' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), 3)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [VaccineId]) VALUES (4, 120, CAST(N'2026-04-10' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), 4)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [VaccineId]) VALUES (5, 80, CAST(N'2025-11-20' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), 5)
SET IDENTITY_INSERT [dbo].[VaccineStock] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__A9D10534D9070BEB]    Script Date: 2/7/2025 11:50:41 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DoseRecord] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[DoseSchedule] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Feedback] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [PaidAt]
GO
ALTER TABLE [dbo].[Service] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Service] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Vaccine] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[VaccineStock] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([ChildId])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [dbo].[User] ([Id])


GO
ALTER TABLE [dbo].[AppointmentService]  WITH CHECK ADD FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[AppointmentService]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[DoseRecord]  WITH CHECK ADD FOREIGN KEY([ChildId])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[DoseRecord]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[DoseRecord]  WITH CHECK ADD FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[DoseSchedule]  WITH CHECK ADD FOREIGN KEY([ChildId])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[DoseSchedule]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[DoseSchedule]  WITH CHECK ADD FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[ServiceVaccine]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[ServiceVaccine]  WITH CHECK ADD FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[VaccineStock]  WITH CHECK ADD FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD CHECK  (([Gender]='F' OR [Gender]='M'))
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
USE [master]
GO
ALTER DATABASE [CVSTSystemDB] SET  READ_WRITE 
GO
