USE [master]
GO
/****** Object:  Database [TSDApp]    Script Date: 17/01/2021 15:59:34 ******/
CREATE DATABASE [TSDApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TSDApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TSDApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TSDApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TSDApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TSDApp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TSDApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TSDApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TSDApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TSDApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TSDApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TSDApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [TSDApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TSDApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TSDApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TSDApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TSDApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TSDApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TSDApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TSDApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TSDApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TSDApp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TSDApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TSDApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TSDApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TSDApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TSDApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TSDApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TSDApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TSDApp] SET RECOVERY FULL 
GO
ALTER DATABASE [TSDApp] SET  MULTI_USER 
GO
ALTER DATABASE [TSDApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TSDApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TSDApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TSDApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TSDApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TSDApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TSDApp', N'ON'
GO
ALTER DATABASE [TSDApp] SET QUERY_STORE = OFF
GO
USE [TSDApp]
GO
/****** Object:  Table [dbo].[tblBanks]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBanks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
 CONSTRAINT [PK__tblBanks__3213E83F65F81309] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblIssueTicketButton]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIssueTicketButton](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](100) NULL,
	[arName] [nvarchar](100) NULL,
	[serviceId] [int] NULL,
	[screenId] [int] NULL,
	[isBusy] [bit] NULL,
 CONSTRAINT [PK_tblIssueTicket] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblScreens]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblScreens](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[isActive] [bit] NULL,
	[bankId] [int] NULL,
	[isBusy] [bit] NULL,
 CONSTRAINT [PK__tblScree__3213E83F162B5424] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblService]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblService](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblShowMessageButton]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShowMessageButton](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](100) NULL,
	[arName] [nvarchar](100) NULL,
	[messageEN] [nvarchar](100) NULL,
	[messageAR] [nvarchar](100) NULL,
	[screenId] [int] NULL,
	[isBusy] [bit] NULL,
 CONSTRAINT [PK_tblShowMessage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblService] ON 
GO
INSERT [dbo].[tblService] ([id], [name]) VALUES (1, N'Withdraw')
GO
INSERT [dbo].[tblService] ([id], [name]) VALUES (2, N'Deposit')
GO
SET IDENTITY_INSERT [dbo].[tblService] OFF
GO
ALTER TABLE [dbo].[tblIssueTicketButton]  WITH CHECK ADD  CONSTRAINT [FK_tblIssueTicket_tblScreens] FOREIGN KEY([screenId])
REFERENCES [dbo].[tblScreens] ([id])
GO
ALTER TABLE [dbo].[tblIssueTicketButton] CHECK CONSTRAINT [FK_tblIssueTicket_tblScreens]
GO
ALTER TABLE [dbo].[tblIssueTicketButton]  WITH CHECK ADD  CONSTRAINT [FK_tblIssueTicket_tblServiceType] FOREIGN KEY([serviceId])
REFERENCES [dbo].[tblService] ([id])
GO
ALTER TABLE [dbo].[tblIssueTicketButton] CHECK CONSTRAINT [FK_tblIssueTicket_tblServiceType]
GO
ALTER TABLE [dbo].[tblScreens]  WITH CHECK ADD  CONSTRAINT [FK__tblScreen__BankI__267ABA7A] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
GO
ALTER TABLE [dbo].[tblScreens] CHECK CONSTRAINT [FK__tblScreen__BankI__267ABA7A]
GO
ALTER TABLE [dbo].[tblShowMessageButton]  WITH CHECK ADD  CONSTRAINT [FK_tblShowMessage_tblScreens] FOREIGN KEY([screenId])
REFERENCES [dbo].[tblScreens] ([id])
GO
ALTER TABLE [dbo].[tblShowMessageButton] CHECK CONSTRAINT [FK_tblShowMessage_tblScreens]
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-00842421-1a0b-4af6-a4d2-8f96fde9facf]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-00842421-1a0b-4af6-a4d2-8f96fde9facf] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-00842421-1a0b-4af6-a4d2-8f96fde9facf]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-00842421-1a0b-4af6-a4d2-8f96fde9facf] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-00842421-1a0b-4af6-a4d2-8f96fde9facf') > 0)   DROP SERVICE [SqlQueryNotificationService-00842421-1a0b-4af6-a4d2-8f96fde9facf]; if (OBJECT_ID('SqlQueryNotificationService-00842421-1a0b-4af6-a4d2-8f96fde9facf', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-00842421-1a0b-4af6-a4d2-8f96fde9facf]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-00842421-1a0b-4af6-a4d2-8f96fde9facf]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-217c47bb-bcf9-4d05-8bb5-42545611f50c]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-217c47bb-bcf9-4d05-8bb5-42545611f50c] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-217c47bb-bcf9-4d05-8bb5-42545611f50c]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-217c47bb-bcf9-4d05-8bb5-42545611f50c] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-217c47bb-bcf9-4d05-8bb5-42545611f50c') > 0)   DROP SERVICE [SqlQueryNotificationService-217c47bb-bcf9-4d05-8bb5-42545611f50c]; if (OBJECT_ID('SqlQueryNotificationService-217c47bb-bcf9-4d05-8bb5-42545611f50c', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-217c47bb-bcf9-4d05-8bb5-42545611f50c]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-217c47bb-bcf9-4d05-8bb5-42545611f50c]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-23a7d28d-3cbd-4330-a8d7-febb061c6623]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-23a7d28d-3cbd-4330-a8d7-febb061c6623] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-23a7d28d-3cbd-4330-a8d7-febb061c6623]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-23a7d28d-3cbd-4330-a8d7-febb061c6623] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-23a7d28d-3cbd-4330-a8d7-febb061c6623') > 0)   DROP SERVICE [SqlQueryNotificationService-23a7d28d-3cbd-4330-a8d7-febb061c6623]; if (OBJECT_ID('SqlQueryNotificationService-23a7d28d-3cbd-4330-a8d7-febb061c6623', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-23a7d28d-3cbd-4330-a8d7-febb061c6623]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-23a7d28d-3cbd-4330-a8d7-febb061c6623]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-89b6f2c8-2bea-402f-a595-628e8decee5d]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-89b6f2c8-2bea-402f-a595-628e8decee5d] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-89b6f2c8-2bea-402f-a595-628e8decee5d]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-89b6f2c8-2bea-402f-a595-628e8decee5d] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-89b6f2c8-2bea-402f-a595-628e8decee5d') > 0)   DROP SERVICE [SqlQueryNotificationService-89b6f2c8-2bea-402f-a595-628e8decee5d]; if (OBJECT_ID('SqlQueryNotificationService-89b6f2c8-2bea-402f-a595-628e8decee5d', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-89b6f2c8-2bea-402f-a595-628e8decee5d]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-89b6f2c8-2bea-402f-a595-628e8decee5d]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-cf32166b-4cc6-4190-9e2e-0f49342bd7be]    Script Date: 17/01/2021 15:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-cf32166b-4cc6-4190-9e2e-0f49342bd7be] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-cf32166b-4cc6-4190-9e2e-0f49342bd7be]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-cf32166b-4cc6-4190-9e2e-0f49342bd7be] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-cf32166b-4cc6-4190-9e2e-0f49342bd7be') > 0)   DROP SERVICE [SqlQueryNotificationService-cf32166b-4cc6-4190-9e2e-0f49342bd7be]; if (OBJECT_ID('SqlQueryNotificationService-cf32166b-4cc6-4190-9e2e-0f49342bd7be', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-cf32166b-4cc6-4190-9e2e-0f49342bd7be]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-cf32166b-4cc6-4190-9e2e-0f49342bd7be]; END COMMIT TRANSACTION; END
GO
USE [master]
GO
ALTER DATABASE [TSDApp] SET  READ_WRITE 
GO
