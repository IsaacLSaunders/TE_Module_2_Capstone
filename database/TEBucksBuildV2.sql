USE [master]
GO
/****** Object:  Database [tebucksV2]    Script Date: 11/5/2023 11:48:10 AM ******/
CREATE DATABASE [tebucksV2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tebucksV2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\tebucksV2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tebucksV2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\tebucksV2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [tebucksV2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tebucksV2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tebucksV2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tebucksV2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tebucksV2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tebucksV2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tebucksV2] SET ARITHABORT OFF 
GO
ALTER DATABASE [tebucksV2] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [tebucksV2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tebucksV2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tebucksV2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tebucksV2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tebucksV2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tebucksV2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tebucksV2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tebucksV2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tebucksV2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [tebucksV2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tebucksV2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tebucksV2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tebucksV2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tebucksV2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tebucksV2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tebucksV2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tebucksV2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [tebucksV2] SET  MULTI_USER 
GO
ALTER DATABASE [tebucksV2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tebucksV2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tebucksV2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tebucksV2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tebucksV2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tebucksV2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [tebucksV2] SET QUERY_STORE = OFF
GO
USE [tebucksV2]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 11/5/2023 11:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounts](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[balance] [int] NOT NULL,
 CONSTRAINT [PK_account] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transfers]    Script Date: 11/5/2023 11:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/5/2023 11:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[accounts] ON 
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (1, 1002, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (3, 1004, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (4, 1005, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (5, 1006, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (6, 1007, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (8, 1008, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (9, 1009, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (10, 1010, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (11, 1011, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (12, 1012, 1000)
GO
INSERT [dbo].[accounts] ([account_id], [userId], [balance]) VALUES (13, 1013, 1000)
GO
SET IDENTITY_INSERT [dbo].[accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[transfers] ON 
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (1, N'Send', N'Approved', 1005, 1006, 7.0000)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (2, N'Request', N'Rejected', 1005, 1006, 7.0000)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (3, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (4, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (5, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (6, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (7, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (8, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (9, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (10, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (11, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (12, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (13, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (14, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (15, N'Send', N'Approved', 1008, 1002, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (16, N'Send', N'Approved', 1011, 1012, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (17, N'Send', N'Approved', 1011, 1012, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (18, N'Send', N'Approved', 1011, 1012, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (19, N'Send', N'Approved', 1011, 1012, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (20, N'Send', N'Approved', 1011, 1012, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (21, N'Send', N'Approved', 1013, 1012, 5.2500)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (22, N'Send', N'Approved', 1013, 1012, 5.0000)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (23, N'Request', N'Pending', 1013, 1012, 5.0000)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (24, N'Request', N'Pending', 1013, 1012, 8.0000)
GO
INSERT [dbo].[transfers] ([transferId], [transferType], [transferStatus], [userFrom], [userTo], [amount]) VALUES (25, N'Request', N'Pending', 1013, 1012, 5.0000)
GO
SET IDENTITY_INSERT [dbo].[transfers] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1002, N'John', N'Jinglehimer', N'hisname', N'hash', N'salt')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1004, N'Joe', N'Jinglehimer', N'hisname2', N'hash', N'salt')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1005, N'Jacob', N'Jinglehimer', N'hisname3', N'hash', N'salt')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1006, N'Joseph', N'Jinglehimer', N'hisname4', N'hash', N'salt')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1007, N'Josiah', N'Jinglehimer', N'hisname5', N'hash', N'salt')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1008, N'isaac', N'saunders', N'izzy', N'b1Dykcdnw2A5Iii9DY96dNWmRlg=', N'K9vaDJwaG0Q=')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1009, N'bob', N'barker', N'bobby', N'pnKGxb99ZWNAEIrxookTW95RoIQ=', N'CkHe5NpNx9w=')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1010, N'joe', N'shmoe', N'username', N'qXY8hY+tRMaJNlyHR7bul8Zs4gI=', N'hNkZDaAlH/I=')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1011, N'joe', N'shmoe', N'username1', N'B1cY6QY5wqkYrOPwqcliexMVo0U=', N'X2zkWqvduXU=')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1012, N'bob', N'bob', N'bob', N'EpeAL9u572FDuvOwSe3fiWJczqc=', N'ZL7trxpV8qs=')
GO
INSERT [dbo].[users] ([user_id], [firstname], [lastname], [username], [password_hash], [salt]) VALUES (1013, N'jim', N'jim', N'jim', N'RmszImAgkKUlxb54mCip57JUlVo=', N'9kc0+u06DxA=')
GO
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_username]    Script Date: 11/5/2023 11:48:11 AM ******/
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [UQ_username] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[accounts]  WITH CHECK ADD  CONSTRAINT [FK_accounts_users] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[accounts] CHECK CONSTRAINT [FK_accounts_users]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_from_users] FOREIGN KEY([userFrom])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_from_users]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_to_users] FOREIGN KEY([userTo])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_to_users]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [CHK_amount] CHECK  (([amount]>(0)))
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [CHK_amount]
GO
USE [master]
GO
ALTER DATABASE [tebucksV2] SET  READ_WRITE 
GO
