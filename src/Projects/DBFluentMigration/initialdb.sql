
/****** Object:  Table [dbo].[Appointment]    Script Date: 12/18/2012 4:40:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Date] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[AppointmentType] [nvarchar](255) NULL,
	[Completed] [bit] NULL,
	[LocationId] [int] NULL,
	[TrainerId] [int] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Appointment_Client]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment_Client](
	[AppointmentId] [int] NOT NULL,
	[ClientId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[BirthDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[Email] [nvarchar](255) NULL,
	[MobilePhone] [nvarchar](255) NULL,
	[SecondaryPhone] [nvarchar](255) NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[Source] [nvarchar](255) NULL,
	[SourceOther] [nvarchar](max) NULL,
	[SessionRatesId] [int] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Location]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[Zip] [nvarchar](255) NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payment]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[FullHour] [int] NULL,
	[FullHourTenPack] [int] NULL,
	[HalfHour] [int] NULL,
	[HalfHourTenPack] [int] NULL,
	[Pair] [int] NULL,
	[PaymentTotal] [float] NULL,
	[FullHourPrice] [float] NULL,
	[FullHourTenPackPrice] [float] NULL,
	[HalfHourPrice] [float] NULL,
	[HalfHourTenPackPrice] [float] NULL,
	[PairPrice] [float] NULL,
	[ClientId] [int] NULL,
	[PairTenPack] [int] NULL,
	[PairTenPackPrice] [float] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Operations]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Operations](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[ParentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Permissions]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Permissions](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Allow] [bit] NOT NULL,
	[Level] [int] NOT NULL,
	[OperationId] [int] NOT NULL,
	[UserId] [int] NULL,
	[UsersGroupId] [int] NULL,
	[Description] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroups]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroups](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ParentId] [int] NULL,
	[Description] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroupsHierarchy]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroupsHierarchy](
	[ParentGroup] [int] NOT NULL,
	[ChildGroup] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChildGroup] ASC,
	[ParentGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersToUsersGroups]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersToUsersGroups](
	[GroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Date] [datetime] NULL,
	[Cost] [float] NULL,
	[AppointmentType] [nvarchar](255) NULL,
	[SessionUsed] [bit] NULL,
	[TrainerPaid] [bit] NULL,
	[PurchaseBatchNumber] [nvarchar](255) NULL,
	[TrainerCheckNumber] [int] NULL,
	[InArrears] [bit] NULL,
	[ClientId] [int] NULL,
	[AppointmentId] [int] NULL,
	[TrainerId] [int] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SessionRates]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionRates](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[FullHour] [float] NULL,
	[HalfHour] [float] NULL,
	[FullHourTenPack] [float] NULL,
	[HalfHourTenPack] [float] NULL,
	[Pair] [float] NULL,
	[PairTenPack] [float] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Trainer_Client]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainer_Client](
	[TrainerId] [int] NOT NULL,
	[ClientId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrainerClientRate]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerClientRate](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Percent] [int] NULL,
	[UserId] [int] NULL,
	[ClientId] [int] NULL,
	[TrainerId] [int] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrainerPayment]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerPayment](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Total] [float] NULL,
	[TrainerId] [int] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrainerPaymentSessionItem]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerPaymentSessionItem](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[AppointmentCost] [float] NULL,
	[TrainerPay] [float] NULL,
	[ClientId] [int] NULL,
	[AppointmentId] [int] NULL,
	[TrainerPaymentId] [int] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[BirthDate] [datetime] NULL,
	[Email] [nvarchar](255) NULL,
	[PhoneMobile] [nvarchar](255) NULL,
	[SecondaryPhone] [nvarchar](255) NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[UserLoginInfoId] [int] NULL,
	[Color] [nvarchar](255) NULL,
	[ClientRateDefault] [int] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_UserRole]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_UserRole](
	[UserId] [int] NOT NULL,
	[UserRoleId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginInfo](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[LoginName] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Salt] [nvarchar](255) NULL,
	[CanLogin] [bit] NULL,
	[LastVisitDate] [datetime] NULL,
	[ByPassToken] [uniqueidentifier] NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 12/18/2012 4:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ChangedDate] [datetime] NULL,
	[ChangedById] [int] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedById] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Appointment] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Appointment] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[Client] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Client] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[Location] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Location] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[SessionRates] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[SessionRates] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[TrainerClientRate] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TrainerClientRate] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[TrainerPayment] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TrainerPayment] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[UserLoginInfo] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserLoginInfo] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[UserRole] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserRole] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_manyToOne_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([EntityId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_manyToOne_Location]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_manyToOne_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_manyToOne_Trainer]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Appointment_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_methodFit_Appointment_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Appointment_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_methodFit_Appointment_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[Appointment_Client]  WITH CHECK ADD  CONSTRAINT [FK_Clients_manyToMany_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([EntityId])
GO
ALTER TABLE [dbo].[Appointment_Client] CHECK CONSTRAINT [FK_Clients_manyToMany_Appointment]
GO
ALTER TABLE [dbo].[Appointment_Client]  WITH CHECK ADD  CONSTRAINT [FK_Clients_manyToMany_Appointment_otherFK] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([EntityId])
GO
ALTER TABLE [dbo].[Appointment_Client] CHECK CONSTRAINT [FK_Clients_manyToMany_Appointment_otherFK]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_manyToOne_SessionRates] FOREIGN KEY([SessionRatesId])
REFERENCES [dbo].[SessionRates] ([EntityId])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_manyToOne_SessionRates]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Client_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_methodFit_Client_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Client_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_methodFit_Client_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Payment_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_methodFit_Payment_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Payment_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_methodFit_Payment_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payments_oneToMany_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([EntityId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payments_oneToMany_Client]
GO
ALTER TABLE [dbo].[security_Operations]  WITH CHECK ADD  CONSTRAINT [FKE58BBFF82B7CDCD3] FOREIGN KEY([ParentId])
REFERENCES [dbo].[security_Operations] ([EntityId])
GO
ALTER TABLE [dbo].[security_Operations] CHECK CONSTRAINT [FKE58BBFF82B7CDCD3]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4C2EE8F612] FOREIGN KEY([UsersGroupId])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4C2EE8F612]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4C71C937C7] FOREIGN KEY([OperationId])
REFERENCES [dbo].[security_Operations] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4C71C937C7]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4CFC8C2B95] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4CFC8C2B95]
GO
ALTER TABLE [dbo].[security_UsersGroups]  WITH CHECK ADD  CONSTRAINT [FKEC3AF233D0CB87D0] FOREIGN KEY([ParentId])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroups] CHECK CONSTRAINT [FKEC3AF233D0CB87D0]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FK69A3B61FA860AB70] FOREIGN KEY([ChildGroup])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FK69A3B61FA860AB70]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FK69A3B61FA87BAE50] FOREIGN KEY([ParentGroup])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FK69A3B61FA87BAE50]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7817F27A1238D4D4] FOREIGN KEY([GroupId])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7817F27A1238D4D4]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7817F27AA6C99102] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7817F27AA6C99102]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Session_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_methodFit_Session_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_Session_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_methodFit_Session_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_oneToMany_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([EntityId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Sessions_oneToMany_Appointment]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_oneToMany_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([EntityId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Sessions_oneToMany_Client]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_oneToMany_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Sessions_oneToMany_Trainer]
GO
ALTER TABLE [dbo].[SessionRates]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_SessionRates_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[SessionRates] CHECK CONSTRAINT [FK_methodFit_SessionRates_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[SessionRates]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_SessionRates_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[SessionRates] CHECK CONSTRAINT [FK_methodFit_SessionRates_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[Trainer_Client]  WITH CHECK ADD  CONSTRAINT [FK_Clients_manyToMany_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Trainer_Client] CHECK CONSTRAINT [FK_Clients_manyToMany_Trainer]
GO
ALTER TABLE [dbo].[Trainer_Client]  WITH CHECK ADD  CONSTRAINT [FK_Clients_manyToMany_Trainer_otherFK] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([EntityId])
GO
ALTER TABLE [dbo].[Trainer_Client] CHECK CONSTRAINT [FK_Clients_manyToMany_Trainer_otherFK]
GO
ALTER TABLE [dbo].[TrainerClientRate]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_TrainerClientRate_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerClientRate] CHECK CONSTRAINT [FK_methodFit_TrainerClientRate_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[TrainerClientRate]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_TrainerClientRate_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerClientRate] CHECK CONSTRAINT [FK_methodFit_TrainerClientRate_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[TrainerClientRate]  WITH CHECK ADD  CONSTRAINT [FK_TrainerClientRate_manyToOne_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerClientRate] CHECK CONSTRAINT [FK_TrainerClientRate_manyToOne_Client]
GO
ALTER TABLE [dbo].[TrainerClientRate]  WITH CHECK ADD  CONSTRAINT [FK_TrainerClientRate_manyToOne_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerClientRate] CHECK CONSTRAINT [FK_TrainerClientRate_manyToOne_User]
GO
ALTER TABLE [dbo].[TrainerClientRate]  WITH CHECK ADD  CONSTRAINT [FK_TrainerClientRates_oneToMany_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerClientRate] CHECK CONSTRAINT [FK_TrainerClientRates_oneToMany_Trainer]
GO
ALTER TABLE [dbo].[TrainerPayment]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_TrainerPayment_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPayment] CHECK CONSTRAINT [FK_methodFit_TrainerPayment_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[TrainerPayment]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_TrainerPayment_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPayment] CHECK CONSTRAINT [FK_methodFit_TrainerPayment_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[TrainerPayment]  WITH CHECK ADD  CONSTRAINT [FK_TrainerPayments_oneToMany_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPayment] CHECK CONSTRAINT [FK_TrainerPayments_oneToMany_Trainer]
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_TrainerPaymentSessionItem_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem] CHECK CONSTRAINT [FK_methodFit_TrainerPaymentSessionItem_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_TrainerPaymentSessionItem_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem] CHECK CONSTRAINT [FK_methodFit_TrainerPaymentSessionItem_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem]  WITH CHECK ADD  CONSTRAINT [FK_TrainerPaymentSessionItem_manyToOne_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem] CHECK CONSTRAINT [FK_TrainerPaymentSessionItem_manyToOne_Appointment]
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem]  WITH CHECK ADD  CONSTRAINT [FK_TrainerPaymentSessionItem_manyToOne_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem] CHECK CONSTRAINT [FK_TrainerPaymentSessionItem_manyToOne_Client]
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem]  WITH CHECK ADD  CONSTRAINT [FK_TrainerPaymentSessionItems_oneToMany_TrainerPayment] FOREIGN KEY([TrainerPaymentId])
REFERENCES [dbo].[TrainerPayment] ([EntityId])
GO
ALTER TABLE [dbo].[TrainerPaymentSessionItem] CHECK CONSTRAINT [FK_TrainerPaymentSessionItems_oneToMany_TrainerPayment]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_User_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_methodFit_User_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_User_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_methodFit_User_manyToOne_CreatedById]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_manyToOne_UserLoginInfo] FOREIGN KEY([UserLoginInfoId])
REFERENCES [dbo].[UserLoginInfo] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_manyToOne_UserLoginInfo]
GO
ALTER TABLE [dbo].[User_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_manyToMany_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[User_UserRole] CHECK CONSTRAINT [FK_UserRoles_manyToMany_User]
GO
ALTER TABLE [dbo].[User_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_manyToMany_User_otherFK] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([EntityId])
GO
ALTER TABLE [dbo].[User_UserRole] CHECK CONSTRAINT [FK_UserRoles_manyToMany_User_otherFK]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_UserRole_manyToOne_ChangedById] FOREIGN KEY([ChangedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_methodFit_UserRole_manyToOne_ChangedById]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_methodFit_UserRole_manyToOne_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_methodFit_UserRole_manyToOne_CreatedById]
GO
