create database CVSTSystemDBv2

CREATE TABLE [dbo].[Appointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentDate] [datetime] NOT NULL,
	[Type] [varchar](50) NULL,
	[TotalPrice] [decimal](10, 2) NULL,
	[Status] [varchar](50) NULL,
	[PaymentStatus] [varchar](50) NULL,
	[ServiceId] [int] NULL,
	[ParentId] [int] NULL,
	[ChildId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentService]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[Child]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[DoseRecord]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[DoseSchedule]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[Feedback]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[Item]    Script Date: 2/21/2025 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemType] [nvarchar](50) NOT NULL,
	[VaccineId] [int] NOT NULL,
	[PackageId] [int] NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[PackageStock]    Script Date: 2/21/2025 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackageStock](
	[Id] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[BatchCode] [nvarchar](50) NOT NULL,
	[ReceiveDate] [date] NOT NULL,
	[PackageId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[Service]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[ServiceVaccine]    Script Date: 2/21/2025 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceVaccine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumberOfDose] [int] NULL,
	[ItemId] [int] NULL,
	[ServiceId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/21/2025 4:21:40 PM ******/
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
/****** Object:  Table [dbo].[Vaccine]    Script Date: 2/21/2025 4:21:40 PM ******/
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
	[CanBeInPackage] [bit] NULL,
 CONSTRAINT [PK__Vaccine__3214EC07AF5AA54E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccinePackage]    Script Date: 2/21/2025 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccinePackage](
	[PackageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TotalQuantity] [int] NULL,
 CONSTRAINT [PK_VaccinePackage] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccinePackageInfo]    Script Date: 2/21/2025 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccinePackageInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VaccineId] [int] NOT NULL,
	[PackageId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_VaccinePackageInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccineStock]    Script Date: 2/21/2025 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccineStock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[BatchCode] [nvarchar](50) NULL,
	[ReceiveDate] [date] NULL,
	[VaccineId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus], [ServiceId], [ParentId], [ChildId]) VALUES (1, CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Vaccination', CAST(100.00 AS Decimal(10, 2)), N'Scheduled', N'Pending', 1, 1, 1)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus], [ServiceId], [ParentId], [ChildId]) VALUES (2, CAST(N'2024-08-21T00:00:00.000' AS DateTime), N'Vaccination', CAST(250.00 AS Decimal(10, 2)), N'Completed', N'Paid', 2, 2, 2)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus], [ServiceId], [ParentId], [ChildId]) VALUES (3, CAST(N'2024-08-22T00:00:00.000' AS DateTime), N'Checkup', CAST(50.00 AS Decimal(10, 2)), N'Scheduled', N'Pending', 3, 1, 3)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus], [ServiceId], [ParentId], [ChildId]) VALUES (4, CAST(N'2024-08-23T00:00:00.000' AS DateTime), N'Immunization', CAST(120.00 AS Decimal(10, 2)), N'Completed', N'Paid', 4, 2, 4)
INSERT [dbo].[Appointment] ([Id], [AppointmentDate], [Type], [TotalPrice], [Status], [PaymentStatus], [ServiceId], [ParentId], [ChildId]) VALUES (5, CAST(N'2024-08-24T00:00:00.000' AS DateTime), N'Protection', CAST(90.00 AS Decimal(10, 2)), N'Scheduled', N'Pending', 5, 1, 5)
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
SET IDENTITY_INSERT [dbo].[Item] ON 

INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (1, N'Vaccine', 2, NULL)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (2, N'Vaccine', 10, NULL)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (3, N'Package', 3, 1)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (4, N'Package', 5, 1)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (5, N'Package', 9, 1)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (6, N'Package', 4, 2)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (7, N'Package', 6, 2)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (8, N'Package', 2, 5)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (9, N'Package', 3, 5)
INSERT [dbo].[Item] ([ItemId], [ItemType], [VaccineId], [PackageId]) VALUES (10, N'Package', 9, 5)
SET IDENTITY_INSERT [dbo].[Item] OFF
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

INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [ItemId], [ServiceId]) VALUES (1, 1, 1, 1)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [ItemId], [ServiceId]) VALUES (2, 2, 2, 2)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [ItemId], [ServiceId]) VALUES (3, 1, 3, 3)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [ItemId], [ServiceId]) VALUES (4, 2, 4, 4)
INSERT [dbo].[ServiceVaccine] ([Id], [NumberOfDose], [ItemId], [ServiceId]) VALUES (5, 3, 5, 5)
SET IDENTITY_INSERT [dbo].[ServiceVaccine] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (1, N'parent1@example.com', N'John Doe', N'1234567890', N'hashedpassword1', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Parent')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (2, N'parent2@example.com', N'Jane Smith', N'0987654321', N'hashedpassword2', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Parent')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (3, N'admin@example.com', N'Admin User', NULL, N'adminpassword', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Admin')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (4, N'doctor@example.com', N'Dr. Emily White', N'1112223333', N'hashedpassword3', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Staff')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (5, N'nurse@example.com', N'Nurse Kelly', N'4445556666', N'hashedpassword4', 1, CAST(N'2025-02-07T21:34:02.123' AS DateTime), NULL, N'Nurse')
INSERT [dbo].[User] ([Id], [Email], [FullName], [PhoneNumber], [Password], [Status], [CreateAt], [UpdateAt], [Role]) VALUES (6, N'khoa@example.com', N'khoa le', NULL, N'$2a$11$Skbt4pMM.6T5NOqkOwl.IujvD41dQu4TkPXL/vlaXGw6ft5EMdRPG', 1, CAST(N'2025-02-15T07:37:42.747' AS DateTime), CAST(N'2025-02-15T07:37:42.747' AS DateTime), N'Staff')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Vaccine] ON 

INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (1, N'BCG', N'Tuberculosis vaccine', N'India', 0, 5, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (2, N'Hepatitis B', N'Hepatitis B vaccine', N'USA', 0, 12, 1, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (3, N'Polio', N'Polio vaccine', N'UK', 0, 5, 1, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (4, N'MMR', N'Measles, Mumps, Rubella vaccine', N'Germany', 1, 12, 1, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (5, N'DTP', N'Diphtheria, Tetanus, Pertussis vaccine', N'France', 2, 7, 1, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (6, N'Varicella', N'Chickenpox vaccine', N'USA', 12, NULL, 1, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (7, N'Influenza (Flu)', N'Seasonal influenza vaccine', N'Various', 6, NULL, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (8, N'Hepatitis A', N'Hepatitis A vaccine', N'Various', 12, NULL, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (9, N'Hib', N'Haemophilus influenzae type b vaccine', N'Various', 2, 5, 1, 1)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (10, N'Rotavirus', N'Rotavirus gastroenteritis vaccine', N'Various', 2, 8, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (11, N'Meningococcal Conjugate', N'Meningitis vaccine', N'Various', 11, 55, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (12, N'HPV', N'Human Papillomavirus vaccine', N'USA', 9, 45, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (13, N'Pneumococcal Conjugate', N'Pneumonia vaccine', N'Various', 6, 65, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (14, N'Tdap', N'Tetanus, Diphtheria, Pertussis (adult booster)', N'Various', 10, NULL, 1, 0)
INSERT [dbo].[Vaccine] ([Id], [Name], [Description], [Origin], [MinAge], [MaxAge], [Status], [CanBeInPackage]) VALUES (15, N'Measles', N'Measles vaccine', N'Various', 12, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[Vaccine] OFF
GO
SET IDENTITY_INSERT [dbo].[VaccinePackage] ON 

INSERT [dbo].[VaccinePackage] ([PackageId], [Name], [Description], [TotalQuantity]) VALUES (1, N'Childhood Essentials 1', N'Recommended vaccinations for early childhood, Contains: DTP, Polio, Hib', 4)
INSERT [dbo].[VaccinePackage] ([PackageId], [Name], [Description], [TotalQuantity]) VALUES (2, N'Childhood Essentials 2', N'Follow-up vaccinations for older children, Contains: MMR, Varicella', 2)
INSERT [dbo].[VaccinePackage] ([PackageId], [Name], [Description], [TotalQuantity]) VALUES (5, N'Combo Protection', N'Combination vaccine for common diseases, Contains: Hepatitis B, Polio, Hib', 3)
SET IDENTITY_INSERT [dbo].[VaccinePackage] OFF
GO
SET IDENTITY_INSERT [dbo].[VaccinePackageInfo] ON 

INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (3, 3, 1, 1)
INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (4, 5, 1, 1)
INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (5, 9, 1, 1)
INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (6, 4, 2, 1)
INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (7, 6, 2, 1)
INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (13, 2, 5, 1)
INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (14, 3, 5, 1)
INSERT [dbo].[VaccinePackageInfo] ([Id], [VaccineId], [PackageId], [Quantity]) VALUES (15, 9, 5, 1)
SET IDENTITY_INSERT [dbo].[VaccinePackageInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[VaccineStock] ON 

INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [BatchCode], [ReceiveDate], [VaccineId]) VALUES (1, 100, CAST(N'2025-12-31' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), N'4a9b3c', CAST(N'2025-01-05' AS Date), 1)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [BatchCode], [ReceiveDate], [VaccineId]) VALUES (2, 150, CAST(N'2026-06-30' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), N'WxYz12', CAST(N'2025-01-05' AS Date), 2)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [BatchCode], [ReceiveDate], [VaccineId]) VALUES (3, 200, CAST(N'2025-09-15' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), N'AbCdEf', CAST(N'2025-01-05' AS Date), 3)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [BatchCode], [ReceiveDate], [VaccineId]) VALUES (4, 120, CAST(N'2026-04-10' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), N'7890Gh', CAST(N'2025-01-05' AS Date), 4)
INSERT [dbo].[VaccineStock] ([Id], [Quantity], [ExpiryDate], [UpdateAt], [BatchCode], [ReceiveDate], [VaccineId]) VALUES (5, 80, CAST(N'2025-11-20' AS Date), CAST(N'2025-02-07T21:34:02.130' AS DateTime), NULL, CAST(N'2025-01-05' AS Date), 5)
SET IDENTITY_INSERT [dbo].[VaccineStock] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__A9D10534E1A8DCF6]    Script Date: 2/21/2025 4:21:41 PM ******/
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
ALTER TABLE [dbo].[Vaccine] ADD  CONSTRAINT [DF__Vaccine__Status__5812160E]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[VaccineStock] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([ChildId])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
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
ALTER TABLE [dbo].[DoseRecord]  WITH CHECK ADD  CONSTRAINT [FK__DoseRecor__Vacci__619B8048] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[DoseRecord] CHECK CONSTRAINT [FK__DoseRecor__Vacci__619B8048]
GO
ALTER TABLE [dbo].[DoseSchedule]  WITH CHECK ADD FOREIGN KEY([ChildId])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[DoseSchedule]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[DoseSchedule]  WITH CHECK ADD  CONSTRAINT [FK__DoseSched__Vacci__6477ECF3] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[DoseSchedule] CHECK CONSTRAINT [FK__DoseSched__Vacci__6477ECF3]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Vaccine] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Vaccine]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_VaccinePackage] FOREIGN KEY([PackageId])
REFERENCES [dbo].[VaccinePackage] ([PackageId])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_VaccinePackage]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PackageStock]  WITH CHECK ADD  CONSTRAINT [FK_PackageStock_VaccinePackage] FOREIGN KEY([PackageId])
REFERENCES [dbo].[VaccinePackage] ([PackageId])
GO
ALTER TABLE [dbo].[PackageStock] CHECK CONSTRAINT [FK_PackageStock_VaccinePackage]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[ServiceVaccine]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[ServiceVaccine]  WITH CHECK ADD  CONSTRAINT [FK_ServiceVaccine_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[ServiceVaccine] CHECK CONSTRAINT [FK_ServiceVaccine_Item]
GO
ALTER TABLE [dbo].[VaccinePackageInfo]  WITH CHECK ADD  CONSTRAINT [FK_VaccinePackageInfo_Vaccine] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[VaccinePackageInfo] CHECK CONSTRAINT [FK_VaccinePackageInfo_Vaccine]
GO
ALTER TABLE [dbo].[VaccinePackageInfo]  WITH CHECK ADD  CONSTRAINT [FK_VaccinePackageInfo_VaccinePackage] FOREIGN KEY([PackageId])
REFERENCES [dbo].[VaccinePackage] ([PackageId])
GO
ALTER TABLE [dbo].[VaccinePackageInfo] CHECK CONSTRAINT [FK_VaccinePackageInfo_VaccinePackage]
GO
ALTER TABLE [dbo].[VaccineStock]  WITH CHECK ADD  CONSTRAINT [FK_VaccineStock_Vaccine] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([Id])
GO
ALTER TABLE [dbo].[VaccineStock] CHECK CONSTRAINT [FK_VaccineStock_Vaccine]
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD CHECK  (([Gender]='F' OR [Gender]='M'))
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
USE [master]
GO
ALTER DATABASE [CVSTSystemDB] SET  READ_WRITE 
GO
