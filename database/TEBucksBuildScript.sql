USE [tebucks]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 11/2/2023 5:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Balance] [money] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 11/2/2023 5:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfers]    Script Date: 11/2/2023 5:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfers](
	[TransferId] [int] IDENTITY(1,1) NOT NULL,
	[UserFromId] [int] NOT NULL,
	[UserToId] [int] NOT NULL,
	[TransferType] [varchar](50) NOT NULL,
	[TransferStatus] [varchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_Transfers] PRIMARY KEY CLUSTERED 
(
	[TransferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/2/2023 5:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1001,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([AccountId], [PersonId], [Balance]) VALUES (2, 12, 1000.0000)
GO
INSERT [dbo].[Accounts] ([AccountId], [PersonId], [Balance]) VALUES (3, 13, 1000.0000)
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 
GO
INSERT [dbo].[Persons] ([Id], [LoginId], [FirstName], [LastName]) VALUES (12, 1015, N'john jacob', N'jinglehimer')
GO
INSERT [dbo].[Persons] ([Id], [LoginId], [FirstName], [LastName]) VALUES (13, 1016, N'JImmy', N'hendrix')
GO
SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
SET IDENTITY_INSERT [dbo].[Transfers] ON 
GO
INSERT [dbo].[Transfers] ([TransferId], [UserFromId], [UserToId], [TransferType], [TransferStatus], [Amount]) VALUES (2, 12, 13, N'Send', N'Rejected', 5.0000)
GO
INSERT [dbo].[Transfers] ([TransferId], [UserFromId], [UserToId], [TransferType], [TransferStatus], [Amount]) VALUES (3, 12, 13, N'Send', N'Pending', 6.0000)
GO
INSERT [dbo].[Transfers] ([TransferId], [UserFromId], [UserToId], [TransferType], [TransferStatus], [Amount]) VALUES (4, 13, 12, N'Send', N'Pending', 25.0000)
GO
SET IDENTITY_INSERT [dbo].[Transfers] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 
GO
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt]) VALUES (1015, N'hisname', N'/cwyj7Xd2AX/q+fiznmwC0USKng=', N'+gQ3wuqJfkY=')
GO
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt]) VALUES (1016, N'purplehaze', N'6WIrdBISF2KT1x0OVmcGt6w6gT4=', N'Rjo9PDrn/48=')
GO
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_username]    Script Date: 11/2/2023 5:28:20 PM ******/
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [UQ_username] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Accounts] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Persons_Accounts]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Users_Person] FOREIGN KEY([LoginId])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Users_Person]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_Transfers_From_Persons] FOREIGN KEY([UserFromId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_Transfers_From_Persons]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_Transfers_To_Persons] FOREIGN KEY([UserToId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_Transfers_To_Persons]
GO
