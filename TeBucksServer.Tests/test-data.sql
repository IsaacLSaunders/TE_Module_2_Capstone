BEGIN TRANSACTION;

--NEW
CREATE TABLE [dbo].[accounts](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[balance] [int] NOT NULL,
 CONSTRAINT [PK_account] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


--NEW
CREATE TABLE [dbo].[transfers](
	[transferId] [int] IDENTITY(1,1) NOT NULL,
	[transferType] [varchar](50) NOT NULL,
	[transferStatus] [varchar](50) NOT NULL,
	[userFrom] [int] NOT NULL,
	[userTo] [int] NOT NULL,
	[amount] [money] NOT NULL,
 CONSTRAINT [PK_transfer] PRIMARY KEY CLUSTERED 
(
	[transferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


--NEW
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1001,1) NOT NULL,
	[firstname] [varchar](50) NOT NULL,
	[lastname] [varchar](50) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_username] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[users] ON
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) 
VALUES 
(1010, N'joe', N'shmoe', N'username', N'qXY8hY+tRMaJNlyHR7bul8Zs4gI=', N'hNkZDaAlH/I='),
(1011, N'joe', N'shmoe', N'username1', N'B1cY6QY5wqkYrOPwqcliexMVo0U=', N'X2zkWqvduXU='),
(1012, N'bob', N'bob', N'bob', N'EpeAL9u572FDuvOwSe3fiWJczqc=', N'ZL7trxpV8qs='),
(1013, N'jim', N'jim', N'jim', N'RmszImAgkKUlxb54mCip57JUlVo=', N'9kc0+u06DxA=')
SET IDENTITY_INSERT [dbo].[users] OFF

SET IDENTITY_INSERT [dbo].[accounts] ON
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) 
VALUES 
(10, 1010, 1000),
(11, 1011, 1000),
(12, 1012, 1000),
(13, 1013, 1000)
SET IDENTITY_INSERT [dbo].[accounts] OFF

SET IDENTITY_INSERT [dbo].[transfers] ON
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) 
VALUES 
(10, N'Send', N'Approved', 1013, 1012, 5.0000),
(11, N'Send', N'Approved', 1012, 1013, 5.0000),
(12, N'Request', N'Pending', 1013, 1012, 10.0000),
(13, N'Request', N'Pending', 1012, 1013, 10.0000),
(14, N'Request', N'Approved', 1013, 1012, 15.0000),
(15, N'Request', N'Approved', 1012, 1013, 15.0000)
SET IDENTITY_INSERT [dbo].[transfers] OFF

--ALTERS for FK's
ALTER TABLE [dbo].[accounts]  WITH CHECK ADD  CONSTRAINT [FK_accounts_users] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([user_id])
ALTER TABLE [dbo].[accounts] CHECK CONSTRAINT [FK_accounts_users]
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_from_users] FOREIGN KEY([userFrom])
REFERENCES [dbo].[users] ([user_id])
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_from_users]
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_to_users] FOREIGN KEY([userTo])
REFERENCES [dbo].[users] ([user_id])
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_to_users]
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [CHK_amount] CHECK  (([amount]>(0)))
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [CHK_amount]


COMMIT TRANSACTION;