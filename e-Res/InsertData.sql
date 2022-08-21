USE [master]
GO
/****** Object:  Database [ERes]    Script Date: 21/08/2022 18:57:23 ******/
CREATE DATABASE [ERes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ERes', FILENAME = N'/var/opt/mssql/data' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ERes_log', FILENAME = N'/var/opt/mssql/data' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ERes] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ERes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ERes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ERes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ERes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ERes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ERes] SET ARITHABORT OFF 
GO
ALTER DATABASE [ERes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ERes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ERes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ERes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ERes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ERes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ERes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ERes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ERes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ERes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ERes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ERes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ERes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ERes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ERes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ERes] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ERes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ERes] SET RECOVERY FULL 
GO
ALTER DATABASE [ERes] SET  MULTI_USER 
GO
ALTER DATABASE [ERes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ERes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ERes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ERes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ERes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ERes] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ERes', N'ON'
GO
ALTER DATABASE [ERes] SET QUERY_STORE = OFF
GO
USE [ERes]
GO
/****** Object:  Schema [identity]    Script Date: 21/08/2022 18:57:23 ******/
CREATE SCHEMA [identity]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bills]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[Id] [uniqueidentifier] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[TotalAmountOfNights] [float] NOT NULL,
	[TotalAmountOfServices] [float] NOT NULL,
	[PriceOfNight] [float] NOT NULL,
	[TotalAmount] [float] NOT NULL,
	[PaidDate] [datetime2](7) NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[ReservationId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[Id] [uniqueidentifier] NOT NULL,
	[UserFromId] [uniqueidentifier] NOT NULL,
	[UserToId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Seen] [bit] NOT NULL,
	[Clicked] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[LogoId] [uniqueidentifier] NULL,
	[LocationId] [uniqueidentifier] NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsApartment] [bit] NOT NULL,
	[IsHotel] [bit] NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guests]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guests](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[CompanyId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Guests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[UserProfilePictureId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [uniqueidentifier] NOT NULL,
	[Longitude] [nvarchar](max) NOT NULL,
	[Latitude] [nvarchar](max) NOT NULL,
	[CityId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[Id] [uniqueidentifier] NOT NULL,
	[DateFrom] [datetime2](7) NOT NULL,
	[DateTo] [datetime2](7) NOT NULL,
	[TotalAmount] [float] NOT NULL,
	[GuestId] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[IsFinished] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationServices]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationServices](
	[Id] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
	[ReservationId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ReservationServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] [uniqueidentifier] NOT NULL,
	[Grade] [float] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Color] [nvarchar](max) NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Verifications]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Verifications](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
	[ExpireDate] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Verifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[asp_net_role_claims]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[asp_net_role_claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_asp_net_role_claims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[asp_net_roles]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[asp_net_roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_asp_net_roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[asp_net_user_claims]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[asp_net_user_claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_asp_net_user_claims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[asp_net_user_logins]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[asp_net_user_logins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_asp_net_user_logins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[asp_net_user_roles]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[asp_net_user_roles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_asp_net_user_roles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [identity].[asp_net_user_tokens]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[asp_net_user_tokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_asp_net_user_tokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[asp_net_users]    Script Date: 21/08/2022 18:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[asp_net_users](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Gender] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[RefreshTokenExpireDate] [datetime2](7) NOT NULL,
	[CompanyId] [uniqueidentifier] NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_asp_net_users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220814144935_added-all-migrations', N'6.0.5')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220820105003_changedTypeOfUser', N'6.0.5')
GO
INSERT [dbo].[Bills] ([Id], [IsPaid], [TotalAmountOfNights], [TotalAmountOfServices], [PriceOfNight], [TotalAmount], [PaidDate], [CompanyId], [ReservationId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'01616ca3-a956-4278-a86a-08da82b197cd', 1, 640, 230, 40, 870, CAST(N'2022-08-21T18:08:37.5520352' AS DateTime2), N'a2930258-a0f3-4416-ba96-08da6a9528da', N'31ba1cd9-be26-48ac-51b1-08da82b1833b', 0, CAST(N'2022-08-20T13:40:49.3933605' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Bills] ([Id], [IsPaid], [TotalAmountOfNights], [TotalAmountOfServices], [PriceOfNight], [TotalAmount], [PaidDate], [CompanyId], [ReservationId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'd91c21ef-00f7-4dad-9f53-08da838f71d4', 0, 30, 0, 10, 30, NULL, N'a2930258-a0f3-4416-ba96-08da6a9528da', N'd3253e07-0bc2-4bb0-24a9-08da837ba934', 0, CAST(N'2022-08-21T18:08:53.9664313' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Bills] ([Id], [IsPaid], [TotalAmountOfNights], [TotalAmountOfServices], [PriceOfNight], [TotalAmount], [PaidDate], [CompanyId], [ReservationId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'2c0983ec-fa0d-4304-9f54-08da838f71d4', 0, 2280, 35, 40, 2315, NULL, N'a2930258-a0f3-4416-ba96-08da6a9528da', N'01d2d1ec-7184-4623-f752-08da83911452', 0, CAST(N'2022-08-21T18:21:30.3022706' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Chat] ([Id], [UserFromId], [UserToId], [Content], [IsDeleted], [Seen], [Clicked], [CreatedDate]) VALUES (N'5d82b5da-2030-4b12-309c-08da83815473', N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'TESTTT', 0, 0, 1, CAST(N'2022-08-21T16:32:51.7290247' AS DateTime2))
GO
INSERT [dbo].[Chat] ([Id], [UserFromId], [UserToId], [Content], [IsDeleted], [Seen], [Clicked], [CreatedDate]) VALUES (N'379bb9e3-debb-408f-309d-08da83815473', N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'TESTTT', 0, 0, 1, CAST(N'2022-08-21T16:32:53.3298336' AS DateTime2))
GO
INSERT [dbo].[Chat] ([Id], [UserFromId], [UserToId], [Content], [IsDeleted], [Seen], [Clicked], [CreatedDate]) VALUES (N'88557538-deb7-4afe-309e-08da83815473', N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ABC', 0, 0, 1, CAST(N'2022-08-21T16:33:00.4894206' AS DateTime2))
GO
INSERT [dbo].[Chat] ([Id], [UserFromId], [UserToId], [Content], [IsDeleted], [Seen], [Clicked], [CreatedDate]) VALUES (N'27866c93-d91b-43ae-309f-08da83815473', N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'testtt', 0, 1, 1, CAST(N'2022-08-21T16:33:19.0278602' AS DateTime2))
GO
INSERT [dbo].[Chat] ([Id], [UserFromId], [UserToId], [Content], [IsDeleted], [Seen], [Clicked], [CreatedDate]) VALUES (N'e9dc8f70-9ef0-4fce-30a0-08da83815473', N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'testtt', 0, 1, 1, CAST(N'2022-08-21T16:33:20.2562484' AS DateTime2))
GO
INSERT [dbo].[Chat] ([Id], [UserFromId], [UserToId], [Content], [IsDeleted], [Seen], [Clicked], [CreatedDate]) VALUES (N'cdbfe836-3a9a-4701-30a1-08da83815473', N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'abc', 0, 1, 1, CAST(N'2022-08-21T16:33:28.2381988' AS DateTime2))
GO
INSERT [dbo].[Chat] ([Id], [UserFromId], [UserToId], [Content], [IsDeleted], [Seen], [Clicked], [CreatedDate]) VALUES (N'7e1d0028-a17c-4e56-30a2-08da83815473', N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'pozdrav', 0, 0, 1, CAST(N'2022-08-21T18:15:21.5836047' AS DateTime2))
GO
INSERT [dbo].[Cities] ([Id], [Title], [CountryId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', N'Mostar', N'6770521d-70f2-4878-31e1-08da6a8579b0', 0, CAST(N'2022-07-20T21:49:20.9351511' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Cities] ([Id], [Title], [CountryId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'6f431bc6-8702-4be6-bee5-08da6a88f16d', N'Sarajevo', N'6770521d-70f2-4878-31e1-08da6a8579b0', 0, CAST(N'2022-07-20T21:49:30.9391543' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Cities] ([Id], [Title], [CountryId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'a9dea3ae-391e-4cbb-bee6-08da6a88f16d', N'Banja Luka', N'6770521d-70f2-4878-31e1-08da6a8579b0', 0, CAST(N'2022-07-20T21:49:39.3032388' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Cities] ([Id], [Title], [CountryId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f4e19bae-a68b-497a-bee7-08da6a88f16d', N'Zenica', N'6770521d-70f2-4878-31e1-08da6a8579b0', 0, CAST(N'2022-07-20T21:49:46.8796093' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Cities] ([Id], [Title], [CountryId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'9102cf68-a2be-4165-bee8-08da6a88f16d', N'Tuzla', N'6770521d-70f2-4878-31e1-08da6a8579b0', 0, CAST(N'2022-07-20T21:49:50.0076144' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Companies] ([Id], [Title], [Address], [LogoId], [LocationId], [CreatedDate], [IsApartment], [IsHotel]) VALUES (N'a2930258-a0f3-4416-ba96-08da6a9528da', N'EDEN Hotel', N'Braće Šarića 43', N'7e9afbc0-9230-4e43-7da2-08da7d7b9db1', N'f1c3e424-12dc-4a17-61f3-08da7a203a40', CAST(N'2022-07-20T23:16:47.5389602' AS DateTime2), 0, 1)
GO
INSERT [dbo].[Companies] ([Id], [Title], [Address], [LogoId], [LocationId], [CreatedDate], [IsApartment], [IsHotel]) VALUES (N'd79371b5-6649-4106-802f-ba36d5bb425f', N'Apartman DADA', N'Braće Fejića 50', N'6c596008-2beb-4435-ef40-08da837a0368', N'b95610b7-c1e9-4447-b050-08da73d8c9b8', CAST(N'2022-08-01T18:19:33.8229380' AS DateTime2), 1, 0)
GO
INSERT [dbo].[Countries] ([Id], [Title], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'6770521d-70f2-4878-31e1-08da6a8579b0', N'Bosna i Hercegovina', 0, CAST(N'2022-07-20T19:24:13.2230000' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Countries] ([Id], [Title], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'4fdb01bf-749e-4730-16eb-08da6a85ae77', N'Hrvatska', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Countries] ([Id], [Title], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'3e3a6f13-5cf1-4797-789e-08da6a85e626', N'Njemacka', 0, CAST(N'2022-07-20T21:27:33.5618128' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Emails] ([Id], [Title], [Content], [UserId]) VALUES (N'7ba690d1-df26-4fff-f62e-08da829a1587', N'4bbab8', N'VERIFIKACIJSKI KOD', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Emails] ([Id], [Title], [Content], [UserId]) VALUES (N'4a3b653b-1f8d-408b-95dd-08da82b00571', N'aa4469', N'VERIFIKACIJSKI KOD', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Emails] ([Id], [Title], [Content], [UserId]) VALUES (N'5ca89f9c-8e59-4af1-95de-08da82b00571', N'8b6194', N'VERIFIKACIJSKI KOD', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Emails] ([Id], [Title], [Content], [UserId]) VALUES (N'1e55c859-d5e8-4ee3-13a0-08da83783edd', N'e4c809', N'VERIFIKACIJSKI KOD', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Emails] ([Id], [Title], [Content], [UserId]) VALUES (N'b21128b5-b4c9-4945-fc36-08da837b0df5', N'5a39a1', N'VERIFIKACIJSKI KOD', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f9ec1b57-4de4-4650-3174-08da6b37cb27', N'string12521', N'string', N'string', NULL, 1, CAST(N'2022-07-21T18:40:58.6030604' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'9e2f3e53-3ca6-4a58-588a-08da6b3fc89e', N'string', N'string251521', N'string', NULL, 0, CAST(N'2022-07-21T19:38:10.4275795' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b980c149-fa73-4c23-6866-08da6b401e8d', N'string', N'string', N'string', NULL, 0, CAST(N'2022-07-21T19:40:34.7277489' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'0abbdf4e-b463-4bd5-d810-08da6b5b3963', N'string', N'string', N'string', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-21T22:54:36.0409728' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'710be6a7-61f5-4249-365f-08da6ca71f66', N'Adi', N'Hodzic', N'062623123', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-23T14:30:26.7583310' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f6eff9ed-d721-4c65-6716-08da6ff04162', N'Benjamin', N'Hodzic', N'241241wr', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-27T18:51:30.5930607' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'9984ab09-fbd5-4c8d-10c1-08da6ff063dd', N'adi', N'adi', N'21521512', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-27T18:52:28.4181341' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'0ce1af26-da43-4117-dc79-08da6ff58d37', N'testt', N'testt', N'3512521521321', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-27T19:29:25.3036251' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'755a9c2e-3633-4dda-24a5-08da6ffb673c', N'Test', N'User', N'000000000', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-27T20:11:18.5492122' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'29cd2d88-507c-4ca2-5769-08da717bb8c9', N'Samra', N'Dželilovic', N'21521521321', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-29T18:02:22.1124900' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'2639ab29-df1f-43ac-a6bb-08da73df19cd', N'Adi', N'Hodzic', N'9299999', N'd79371b5-6649-4106-802f-ba36d5bb425f', 1, CAST(N'2022-08-01T18:58:47.4123984' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f7604d23-170d-4dd4-e11f-08da788aaff6', N'Benjamin', N'Hodzic', N'29195921', N'd79371b5-6649-4106-802f-ba36d5bb425f', 1, CAST(N'2022-08-07T17:37:07.8210868' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f9020e54-94de-491d-4235-08da788af315', N'test', N'test', N'2121421', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:39:00.4765539' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'cd98d5b3-da81-49c3-3532-08da788b1655', N'abv', N'avvsa', N'2121', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:39:59.6432879' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'c5d7bec9-f169-46ec-9849-08da788c8595', N'Adi', N'Hodzic', N'asasa', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:50:15.7452839' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'bbded6df-f0a3-46c9-984a-08da788c8595', N'Adi', N'Hodzic', N'asfsaf', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:50:32.6640308' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ed02292c-3f94-4c63-984b-08da788c8595', N'Adi', N'Hodzic', N'saffs', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:51:17.3706082' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'04373272-4456-4dac-984c-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:53:07.9868828' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'68d6b2e3-3286-4448-984d-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:57:36.9685694' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'd1cb88c0-1ac2-4362-984e-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:59:08.6479832' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5e6ca1f3-b2d0-4db5-984f-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:59:11.8750704' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'4638a8dd-e365-4cc7-9850-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T17:59:46.1386999' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'a22c7ca5-aeaf-4748-9851-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:00:50.0271434' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'408b8e61-fae4-4e37-9852-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:01:36.2579708' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'3b9694f7-400a-49b6-9853-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:02:41.2006090' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'65b7ce55-4404-4573-9854-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:04:34.4640147' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'8e28472a-39c2-4251-9855-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:04:45.4964882' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f0f288d5-4372-4c78-9856-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:04:52.9537456' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'1292d4ed-807b-45f4-9857-08da788c8595', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:05:10.7293378' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'18d8fb52-9f19-4357-3123-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:11:11.4991684' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ced218b6-d5bc-4c8d-3124-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:11:11.6752393' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ce8f8984-9d32-4cfc-3125-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:14:22.1688118' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'341dd512-b9e0-402f-3126-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:15:21.1180966' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'efdfccc0-c77d-4313-3127-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:15:57.5369403' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'2f63e208-dd26-4135-3128-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:16:40.4442263' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'7bb67e9c-39d3-45ef-3129-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:17:26.4612614' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5bfe863e-edd4-4d85-312a-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:18:06.5402840' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'7cce9bcd-b922-4337-312b-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:19:59.5664946' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'10fd8459-c6be-473a-312c-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:21:13.9362898' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'3e8ffcdb-566d-4bf7-312d-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:21:47.5514372' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'52797c53-611a-4c6e-312e-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:22:31.6654876' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'7ae5a1e9-6e43-4084-312f-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:22:58.5840822' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'36cfc18c-60a4-4a94-3130-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:23:37.8166799' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'66ad3f52-811c-4e5c-3131-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:24:32.9760204' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'893dcc10-0f18-47bb-3132-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:25:13.9651944' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'c9dd84e5-4715-457c-3133-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:25:20.4152387' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'159554fe-9754-475d-3134-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:26:01.1609847' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'edf4ec4d-4550-4442-3135-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:28:29.2413647' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'94dde547-e29e-4557-3136-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:28:51.5528403' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'150f58e1-cbf1-4bdf-3137-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'd79371b5-6649-4106-802f-ba36d5bb425f', 1, CAST(N'2022-08-07T18:40:03.3666469' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'6a56311b-8b4c-46dd-3138-08da788f7210', N'Adi', N'Hodzic', N'062610068', N'd79371b5-6649-4106-802f-ba36d5bb425f', 1, CAST(N'2022-08-07T18:40:40.9684606' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'3bcba8b3-52a1-4ed0-11f1-08da7893b952', N'Adi', N'Hodzic', N'062610068', N'd79371b5-6649-4106-802f-ba36d5bb425f', 1, CAST(N'2022-08-07T18:41:49.0495258' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'e1b14f38-8d45-4af1-11f2-08da7893b952', N'Adi', N'Hodzic', N'062610068', N'd79371b5-6649-4106-802f-ba36d5bb425f', 1, CAST(N'2022-08-07T18:41:49.0495596' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'bc08f8e3-74f5-48de-11f3-08da7893b952', N'Adi', N'Hodzic', N'062610068', N'd79371b5-6649-4106-802f-ba36d5bb425f', 1, CAST(N'2022-08-07T18:41:49.0495636' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'215fe9db-7cec-487c-133c-08da789413cb', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:44:20.8437296' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'75509691-5eda-4136-133d-08da789413cb', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:44:24.6671116' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5b4c2754-f51a-4763-133e-08da789413cb', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-07T18:44:44.2488013' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'c54293fe-a42e-431a-190d-08da7a219fbd', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-09T18:10:05.8776087' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'0fe69798-e7bb-445f-190e-08da7a219fbd', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-09T18:12:09.1553890' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'175a0c04-ec36-43ea-190f-08da7a219fbd', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-09T18:12:47.6032982' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'2db139d6-adf6-4d46-1910-08da7a219fbd', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-09T18:13:53.8179727' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5caa895f-6c78-4c75-1911-08da7a219fbd', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-09T18:14:00.1943347' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'9fdfec45-6fea-410c-1912-08da7a219fbd', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-09T18:14:24.7581032' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5a19f213-5ee6-422f-1913-08da7a219fbd', N'Adi', N'Hodzic', N'062610068', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-09T18:14:29.4254090' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'9f966a47-b46b-4df4-7e5a-08da7bb9377c', N'Adi', N'Hodzic', N'0626100682', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-11T18:47:45.6217610' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'0a193c9e-27eb-48e3-bc72-08da7bbadf4a', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-11T18:59:36.6416869' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'15bf8af0-43e8-4dec-bc73-08da7bbadf4a', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-11T18:59:36.6416826' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b027852b-ebdc-41ce-f28e-08da7bbb165e', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-11T19:01:09.0463835' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f581bb99-6ce4-40e3-28c7-08da7c878747', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-12T19:24:35.7051256' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ff77602c-0056-4deb-e7f4-08da7d5d1411', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-13T20:53:14.8186689' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'1d35ea89-9685-4bec-e7f5-08da7d5d1411', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-13T20:53:33.3669479' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'81f939c3-c7c9-4a0e-e7f6-08da7d5d1411', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-13T20:53:44.9526893' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'1bdb38fb-ece4-4f78-301c-08da7d6132c9', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-13T21:22:44.3554924' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5cbe5299-5c9f-40af-301d-08da7d6132c9', N'mobile', N'mobile', N'mobile', N'a2930258-a0f3-4416-ba96-08da6a9528da', 0, CAST(N'2022-08-13T23:40:25.6988750' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'a4910394-bff4-4e4c-ef14-08da7df6c5b6', N'mobile', N'mobile', N'mobile', N'd79371b5-6649-4106-802f-ba36d5bb425f', 0, CAST(N'2022-08-14T15:13:25.8386874' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Guests] ([Id], [FirstName], [LastName], [PhoneNumber], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'0c4dcbcc-afe8-4e25-d5d4-08da7dfab9d0', N'test', N'test', N'212412412', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-08-14T15:41:43.8900850' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'4aa8010e-6bde-41c5-1263-08da732026d7', N'/Uploads/Images/a7b0eb29-1cdb-49bd-9ebb-cf2adc8ecec7.jpg', NULL, 1, CAST(N'2022-07-31T20:11:55.3812571' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'7ee4628d-ac47-4ab8-9ff5-08da73dfb294', N'/Uploads/Images/0a9a55e9-b648-40f7-9401-d2f25de32556.jpg', NULL, 1, CAST(N'2022-08-01T19:03:03.7239056' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'e46e6743-812e-4c76-9ff6-08da73dfb294', N'/Uploads/Images/b1cd42d4-537b-4762-bc44-4ec7164556f0.jpg', NULL, 1, CAST(N'2022-08-01T19:03:12.0612750' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'c94d63ff-fa0c-4f2c-9ff7-08da73dfb294', N'/Uploads/Images/b0cfb56c-9a0c-466c-9c5a-d25bfb841c7a.jpg', NULL, 1, CAST(N'2022-08-01T19:03:14.8976928' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'21a48358-fa06-4ab8-9ff8-08da73dfb294', N'/Uploads/Images/840d7168-eea6-4a1d-80fa-6cb537d5d81d.jpg', NULL, 1, CAST(N'2022-08-01T19:03:16.9794967' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'c1b3cef2-9f02-4a8d-9ff9-08da73dfb294', N'/Uploads/Images/6b238f64-d09f-46ca-a291-122a4624035e.jpg', NULL, 1, CAST(N'2022-08-01T19:03:19.0137786' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'2f2bd800-1deb-4ad3-d3fe-08da74a1b3f6', N'/Uploads/Images/66804aa3-9842-4f6b-9a9d-2181f08ab305.jpg', NULL, 0, CAST(N'2022-08-02T18:12:00.8614916' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5f117770-92c0-4a10-23e8-08da7d773fd7', N'/Uploads/Images/39ab7304-83f0-4ebd-a393-0f31470c23b0.jpg', NULL, 1, CAST(N'2022-08-14T00:00:35.1652654' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'7e9afbc0-9230-4e43-7da2-08da7d7b9db1', N'/Uploads/Images/26e85347-f26c-4bcf-884b-c7a37734a2f6.jpg', N'f24e8df0-b5c7-461e-13d7-08da73d9df28', 1, CAST(N'2022-08-14T00:32:27.2327211' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ec93042f-1a4a-45b3-ff62-08da7d7e3909', N'/Uploads/Images/98f86830-bddc-4b7e-a583-7bafa521b01d.jpg', N'ceece2e6-4966-4652-a711-08da6b3bd31e', 1, CAST(N'2022-08-14T00:50:29.8805325' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'8af265c8-63d9-42f9-b049-08da82b0ee24', N'/Uploads/Images/5f65a910-42a4-4477-a2d3-e2930e19c8d8.jpg', NULL, 1, CAST(N'2022-08-20T13:36:04.7486472' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f6e4a9b3-38aa-48d4-b04a-08da82b0ee24', N'/Uploads/Images/4b38e8ad-1805-4638-9113-f5a81cefeac0.jpg', N'f24e8df0-b5c7-461e-13d7-08da73d9df28', 1, CAST(N'2022-08-20T13:36:26.8827064' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'e8f41c76-68da-457d-b04b-08da82b0ee24', N'/Uploads/Images/f06470b1-d9b3-4a54-ab14-10927e7a71a3.jpg', NULL, 1, CAST(N'2022-08-20T13:36:33.1184802' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'3348c829-8499-4bf6-b04c-08da82b0ee24', N'/Uploads/Images/0507c431-e39d-4e4a-9bf1-58a1ac928542.jpg', NULL, 1, CAST(N'2022-08-20T13:36:58.0683436' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'97f8cdf0-2e6a-49c2-b04d-08da82b0ee24', N'/Uploads/Images/a32a728e-f83e-4b54-9963-49b9cf37952d.jpg', NULL, 1, CAST(N'2022-08-20T13:36:59.8960503' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'8f080128-5d40-4b76-b04e-08da82b0ee24', N'/Uploads/Images/f8387347-2dec-4302-8470-74d5709450dd.jpg', NULL, 1, CAST(N'2022-08-20T13:37:01.8099509' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b109af23-cfb2-40f9-b04f-08da82b0ee24', N'/Uploads/Images/59a0ae6a-69e9-44e3-9532-d67017d91117.jpg', NULL, 1, CAST(N'2022-08-20T13:37:03.7937073' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'fc741117-c727-4f3e-b050-08da82b0ee24', N'/Uploads/Images/92db351e-4645-4aa8-8063-8c86e5c2bae7.jpg', NULL, 1, CAST(N'2022-08-20T13:37:05.5329178' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'efcacce5-ab8c-4b68-b051-08da82b0ee24', N'/Uploads/Images/3a3dddf7-3d64-4eea-9bab-b1ad19b1475e.jpg', NULL, 1, CAST(N'2022-08-20T13:37:07.6051454' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'eafb4f64-4a1c-4b9f-b052-08da82b0ee24', N'/Uploads/Images/db70a045-5b3d-40d9-9ef9-4a5956445b5e.jpg', NULL, 1, CAST(N'2022-08-20T13:37:15.1805176' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'523908b3-eb5d-470d-89c3-08da8378864b', N'/Uploads/Images/00caa4b0-1903-41b5-a8d0-e7a293e48283.jpg', N'cf276b4c-9c1c-4112-af9a-08da8378861d', 0, CAST(N'2022-08-21T15:24:49.8620906' AS DateTime2), N'cf276b4c-9c1c-4112-af9a-08da8378861d', N'cf276b4c-9c1c-4112-af9a-08da8378861d')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'717955d5-9265-47d1-ef3c-08da837a0368', N'/Uploads/Images/7faa17d4-4bb3-4529-9293-7db9fda25143.jpg', NULL, 0, CAST(N'2022-08-21T15:35:29.2342481' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'3a0ea0a2-c0b3-4d1b-ef3d-08da837a0368', N'/Uploads/Images/2058aecf-5b88-4bb0-a1de-dc07d18e0c62.jpg', NULL, 0, CAST(N'2022-08-21T15:35:31.7614245' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b78eace5-b2f3-4591-ef3e-08da837a0368', N'/Uploads/Images/ac867b37-9856-4256-b497-53f552888f4c.jpg', NULL, 0, CAST(N'2022-08-21T15:35:39.8097323' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'2883d5ed-c099-451e-ef3f-08da837a0368', N'/Uploads/Images/e6c26afd-d4b4-41e5-abbd-768d6ab95ee1.jpg', NULL, 0, CAST(N'2022-08-21T15:35:41.9495677' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'6c596008-2beb-4435-ef40-08da837a0368', N'/Uploads/Images/008e83b0-2c61-4203-9086-9c6cf7bbb393.jpg', NULL, 0, CAST(N'2022-08-21T15:35:46.0574513' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'bfeb5b63-441b-432d-ef41-08da837a0368', N'/Uploads/Images/e5bb7df2-990b-49b8-8fee-949c2c457773.jpg', N'f24e8df0-b5c7-461e-13d7-08da73d9df28', 0, CAST(N'2022-08-21T15:35:46.3361094' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'be5b661a-f9ab-4370-28e4-08da83878464', N'/Uploads/Images/ecd870e7-20ec-498c-a398-b73030838a83.jpg', NULL, 0, CAST(N'2022-08-21T17:12:09.0494183' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'd879d136-10dd-4b9a-28e5-08da83878464', N'/Uploads/Images/e7b4fbe4-1eb2-447b-8e71-336f93f87c81.jpg', NULL, 0, CAST(N'2022-08-21T17:12:16.4436026' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'd2fb3ca7-2a7f-4201-28e6-08da83878464', N'/Uploads/Images/ad882503-c1c4-4ac6-9afa-e73444ae2659.jpg', NULL, 0, CAST(N'2022-08-21T17:12:19.0057286' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'73e8c25f-a229-43b2-28e7-08da83878464', N'/Uploads/Images/3d022645-96fb-4f34-94b7-53b23c0efe69.jpg', NULL, 0, CAST(N'2022-08-21T17:12:21.9144407' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f31377fc-a78d-4496-28e8-08da83878464', N'/Uploads/Images/db788bc4-8101-40d4-92da-d21f6f3eb6e7.jpg', NULL, 0, CAST(N'2022-08-21T17:12:25.4435334' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'4c4821dc-bd7c-415a-28e9-08da83878464', N'/Uploads/Images/9c36c314-9644-47d1-9947-357e15096a18.jpg', N'ceece2e6-4966-4652-a711-08da6b3bd31e', 0, CAST(N'2022-08-21T18:18:19.8344272' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Images] ([Id], [Path], [UserProfilePictureId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ab00d1d6-bd6a-4bcc-8004-f91aa7dc76ec', N'/Uploads/Images/e5bb7df2-990b-49b8-8fee-949c2c457773.jpg', N'210516d0-474a-4db6-a5a5-08da6e72cec8', 0, CAST(N'2022-08-21T15:35:46.3361094' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'9d845502-b31d-4200-ae3c-08da6a8b06c1', N'17.8078° E', N'43.3438° N', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 1, CAST(N'2022-07-20T22:04:15.8526815' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'85831210-e3ed-48c4-3e65-08da6ca9a30c', N'53', N'21', N'6f431bc6-8702-4be6-bee5-08da6a88f16d', 0, CAST(N'2022-07-23T14:48:21.6703076' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'be60e157-057e-407a-a6b7-08da6ca9d56f', N'string', N'string', N'6f431bc6-8702-4be6-bee5-08da6a88f16d', 0, CAST(N'2022-07-23T14:49:49.8785947' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ad5dee1b-b1b4-4660-a6b8-08da6ca9d56f', N'string', N'string', N'6f431bc6-8702-4be6-bee5-08da6a88f16d', 0, CAST(N'2022-07-23T14:50:20.4187871' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'69d54c9e-f5e4-476f-a6b9-08da6ca9d56f', N'string', N'string', N'6f431bc6-8702-4be6-bee5-08da6a88f16d', 0, CAST(N'2022-07-23T14:52:47.9018229' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'949d9129-0a9c-435c-167f-08da73293185', N'17.8078° E', N'43.3438° N', N'a9dea3ae-391e-4cbb-bee6-08da6a88f16d', 0, CAST(N'2022-07-31T21:16:38.7650031' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'fafa176c-004e-44d7-1680-08da73293185', N'17.8078° E', N'43.3438° N', N'a9dea3ae-391e-4cbb-bee6-08da6a88f16d', 0, CAST(N'2022-07-31T21:16:45.8323550' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f42d0fc5-0827-4822-1681-08da73293185', N'17.8078° E', N'43.3438° N', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-07-31T21:18:29.5593013' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'61360405-441e-4f3d-09af-08da73d8643f', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:10:45.8064507' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b59a9532-2b0b-47a0-3cfd-08da73d8a03b', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:12:26.4232502' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'55184f2f-4a26-4fc4-b049-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:13:36.0444485' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'4609ef7d-8fed-4d44-b04a-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:15:14.0672511' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'a1b8e392-27db-42eb-b04b-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:15:20.4562951' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'57d06e1c-98b3-419f-b04c-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:16:11.5130467' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'cea1c7db-f9c2-49cb-b04d-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:17:24.2282045' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'c64c9356-b919-4186-b04e-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:18:38.6950473' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'5e891ccb-5fe2-4eba-b04f-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:18:47.8464099' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b95610b7-c1e9-4447-b050-08da73d8c9b8', N'43.346117', N'17.814212', N'f3e4f9b1-95d3-4505-bee4-08da6a88f16d', 0, CAST(N'2022-08-01T18:19:33.7255563' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'7cc8e1f5-39cb-47d3-faf4-08da78b2b08c', N'17.8078°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-07T22:23:28.7065456' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b01cb2b8-5b42-46da-adc8-08da78b4450d', N'17.8078°N', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-07T22:34:47.3592174' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'588f908c-a993-4620-adc9-08da78b4450d', N'17.8078° N', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-07T22:34:56.3959803' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'1cd04dad-e0fe-4f67-4c26-08da7a1a31a7', N'17.80781°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:16:54.7086323' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'ec1ed41e-77a1-4f86-4c27-08da7a1a31a7', N'17.807812°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:17:26.6066068' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b55049b4-2918-4a34-56dd-08da7a1a9856', N'17.807°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:19:46.9415472' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'945d489e-f402-4348-56de-08da7a1a9856', N'17.80°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:22:13.5307383' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'8fbf6f3c-c73b-45b7-7801-08da7a1b2ffd', N'17.8°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:24:01.3792824' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'39e4d9a1-c8c8-4143-7802-08da7a1b2ffd', N'17.89999999°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:24:18.5012867' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b0fbb9ba-e72d-4964-7803-08da7a1b2ffd', N'17.5555555555°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:24:36.0271854' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f28a601d-d450-44d6-7804-08da7a1b2ffd', N'17.55555255555°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:24:50.4593480' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'3ab21fdb-a03f-4b05-7805-08da7a1b2ffd', N'17.5555°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T17:25:21.7777344' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'4e179d21-2b11-47be-61f2-08da7a203a40', N'17.55535°', N'43.3438°', N'9102cf68-a2be-4165-bee8-08da6a88f16d', 0, CAST(N'2022-08-09T18:00:06.0864683' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Locations] ([Id], [Longitude], [Latitude], [CityId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f1c3e424-12dc-4a17-61f3-08da7a203a40', N'17.552535°', N'43.3438°', N'6f431bc6-8702-4be6-bee5-08da6a88f16d', 0, CAST(N'2022-08-09T18:00:13.9868996' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Reservations] ([Id], [DateFrom], [DateTo], [TotalAmount], [GuestId], [RoomId], [IsFinished], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'31ba1cd9-be26-48ac-51b1-08da82b1833b', CAST(N'2022-08-03T00:00:00.0000000' AS DateTime2), CAST(N'2022-08-19T00:00:00.0000000' AS DateTime2), 870, N'5cbe5299-5c9f-40af-301d-08da7d6132c9', N'67e655ca-51f1-4c1e-c501-08da6e590a4b', 1, 0, CAST(N'2022-08-20T13:40:14.8840220' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Reservations] ([Id], [DateFrom], [DateTo], [TotalAmount], [GuestId], [RoomId], [IsFinished], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'd3253e07-0bc2-4bb0-24a9-08da837ba934', CAST(N'2022-08-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-08-04T00:00:00.0000000' AS DateTime2), 30, N'5cbe5299-5c9f-40af-301d-08da7d6132c9', N'acd1dee3-8290-4190-75c5-08da6b570e3d', 1, 0, CAST(N'2022-08-21T15:47:16.9283682' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Reservations] ([Id], [DateFrom], [DateTo], [TotalAmount], [GuestId], [RoomId], [IsFinished], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'e02dd515-33a4-49d3-f751-08da83911452', CAST(N'2022-08-24T00:00:00.0000000' AS DateTime2), CAST(N'2022-10-20T00:00:00.0000000' AS DateTime2), 2525, N'5cbe5299-5c9f-40af-301d-08da7d6132c9', N'67e655ca-51f1-4c1e-c501-08da6e590a4b', 0, 1, CAST(N'2022-08-21T18:20:36.0821348' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Reservations] ([Id], [DateFrom], [DateTo], [TotalAmount], [GuestId], [RoomId], [IsFinished], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'01d2d1ec-7184-4623-f752-08da83911452', CAST(N'2022-08-24T00:00:00.0000000' AS DateTime2), CAST(N'2022-10-20T00:00:00.0000000' AS DateTime2), 2315, N'5cbe5299-5c9f-40af-301d-08da7d6132c9', N'67e655ca-51f1-4c1e-c501-08da6e590a4b', 1, 0, CAST(N'2022-08-21T18:21:10.1063564' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'd6a6ee9e-7277-423a-a894-08da82b189ca', N'99ef14e2-7904-4760-27e4-08da6ca3a58a', N'31ba1cd9-be26-48ac-51b1-08da82b1833b', 12, 0, CAST(N'2022-08-20T13:40:25.9259850' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'469aa705-bdd4-4cd1-a895-08da82b189ca', N'99ef14e2-7904-4760-27e4-08da6ca3a58a', N'31ba1cd9-be26-48ac-51b1-08da82b1833b', 1, 0, CAST(N'2022-08-20T13:40:45.9661884' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'76ca6098-6874-4062-a896-08da82b189ca', N'0346fe3c-a608-4bfa-1555-08da6e78368e', N'31ba1cd9-be26-48ac-51b1-08da82b1833b', 1, 0, CAST(N'2022-08-20T13:40:46.0097015' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'a589831d-6c83-45a0-a897-08da82b189ca', N'99ef14e2-7904-4760-27e4-08da6ca3a58a', N'31ba1cd9-be26-48ac-51b1-08da82b1833b', 1, 0, CAST(N'2022-08-20T13:40:46.0528596' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'c6560f94-196a-40ce-27cc-08da83911fb2', N'690ed142-dee7-4423-4ada-08da8390f757', N'e02dd515-33a4-49d3-f751-08da83911452', 15, 0, CAST(N'2022-08-21T18:20:55.1718243' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'6ad24c70-fa63-4729-27cd-08da83911fb2', N'564c62c0-f3f2-4a6e-4adb-08da8390f757', N'e02dd515-33a4-49d3-f751-08da83911452', 1, 0, CAST(N'2022-08-21T18:20:55.9349622' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'f82f183d-4ea8-4aad-27ce-08da83911fb2', N'690ed142-dee7-4423-4ada-08da8390f757', N'01d2d1ec-7184-4623-f752-08da83911452', 1, 0, CAST(N'2022-08-21T18:21:25.3798929' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[ReservationServices] ([Id], [ServiceId], [ReservationId], [Quantity], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b77c373f-cc69-4e72-27cf-08da83911fb2', N'564c62c0-f3f2-4a6e-4adb-08da8390f757', N'01d2d1ec-7184-4623-f752-08da83911452', 1, 0, CAST(N'2022-08-21T18:21:26.1976103' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Reviews] ([Id], [Grade], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'bea1591a-c206-4c3d-1c31-08da77945d72', 10, N'a2930258-a0f3-4416-ba96-08da6a9528da', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[Reviews] ([Id], [Grade], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'2ffaba88-f4af-4841-8ce2-08da7e18da13', 6, N'd79371b5-6649-4106-802f-ba36d5bb425f', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[Reviews] ([Id], [Grade], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'4179e3c6-1148-4705-8ce3-08da7e18da13', 6, N'd79371b5-6649-4106-802f-ba36d5bb425f', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[Reviews] ([Id], [Grade], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'deccc0db-f620-4cca-8ce4-08da7e18da13', 9, N'd79371b5-6649-4106-802f-ba36d5bb425f', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[Reviews] ([Id], [Grade], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'fcb3cd32-434c-4cbc-8ce5-08da7e18da13', 10, N'd79371b5-6649-4106-802f-ba36d5bb425f', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[Reviews] ([Id], [Grade], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'27263520-2a8b-4c15-5cb2-08da7e1c72ab', 10, N'd79371b5-6649-4106-802f-ba36d5bb425f', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[Reviews] ([Id], [Grade], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'b322516a-0d0e-4254-60b0-08da838f6ad1', 7, N'a2930258-a0f3-4416-ba96-08da6a9528da', 0, CAST(N'2022-08-21T18:08:42.1953967' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8', NULL)
GO
INSERT [dbo].[Rooms] ([Id], [Title], [Description], [Price], [Color], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'acd1dee3-8290-4190-75c5-08da6b570e3d', N'404', N'Četverokrevetna', 10, N'#FF56DA5D', N'a2930258-a0f3-4416-ba96-08da6a9528da', 0, CAST(N'2022-07-21T22:24:45.5813540' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Rooms] ([Id], [Title], [Description], [Price], [Color], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'67e655ca-51f1-4c1e-c501-08da6e590a4b', N'505', N'Petokrevetna', 40, N'#FF5C56DA', N'a2930258-a0f3-4416-ba96-08da6a9528da', 0, CAST(N'2022-07-25T18:16:31.5100883' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Rooms] ([Id], [Title], [Description], [Price], [Color], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'00c48535-db28-4924-da18-08da6e704299', N'string', N'string', 550, N'#FF0E3C', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-25T21:02:45.8788062' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Rooms] ([Id], [Title], [Description], [Price], [Color], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'8e81b65f-1e5d-4eaa-5140-08da722e0e94', N'709', N'Soba sa 7 kreveta', 251, N'#FFFFC300', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-30T15:18:56.4837942' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Rooms] ([Id], [Title], [Description], [Price], [Color], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'21af23bf-19d2-4fd5-5141-08da722e0e94', N'909', N'Soba', 75574, N'#FFF3FF00', N'a2930258-a0f3-4416-ba96-08da6a9528da', 1, CAST(N'2022-07-30T15:20:28.9884928' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Rooms] ([Id], [Title], [Description], [Price], [Color], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'59389573-5383-46e1-0a82-08da7231bd95', N'606', N'Soba sa pogledom na grad', 90, N'#FFFFF900', N'a2930258-a0f3-4416-ba96-08da6a9528da', 0, CAST(N'2022-07-30T15:45:18.5782925' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Rooms] ([Id], [Title], [Description], [Price], [Color], [CompanyId], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'7a2c79f5-7ba6-4e8c-dbbe-08da73d8d896', N'Apartman DADA', N'N/A', 30, N'#FF0066C0', N'd79371b5-6649-4106-802f-ba36d5bb425f', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'52c5b3b6-0b6a-47e3-3dd4-08da6b48ec08', N'Ručak', 30, 1, CAST(N'2022-07-21T20:43:35.3235131' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'9e7e197d-3d6a-414e-27e2-08da6ca3a58a', N'Doručak', 15, 1, CAST(N'2022-07-23T14:05:33.8556525' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'8ac96991-94d4-418c-27e3-08da6ca3a58a', N'Večera', 15, 0, CAST(N'2022-07-23T14:05:38.7298476' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'99ef14e2-7904-4760-27e4-08da6ca3a58a', N'Užina', 15, 0, CAST(N'2022-07-23T14:05:43.9495671' AS DateTime2), N'096af8d7-251b-4be5-581c-08da6a7be48a', N'096af8d7-251b-4be5-581c-08da6a7be48a')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'0346fe3c-a608-4bfa-1555-08da6e78368e', N'Doručak', 20, 0, CAST(N'2022-07-25T21:59:41.6412776' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'fb5e7e2c-9b3b-4384-6a0c-08da70001529', N'testsa', 5555555555555, 1, CAST(N'2022-07-27T20:44:48.3366394' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'a531af66-9666-4a5b-9fe9-08da73df1f3f', N'Doručak', 32, 0, CAST(N'2022-08-01T18:58:56.5498260' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'690ed142-dee7-4423-4ada-08da8390f757', N'Ručak', 15, 0, CAST(N'2022-08-21T18:19:47.4494009' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Services] ([Id], [Title], [Price], [IsDeleted], [CreatedDate], [CreatedByUserId], [ModifiedByUserId]) VALUES (N'564c62c0-f3f2-4a6e-4adb-08da8390f757', N'Večera', 20, 0, CAST(N'2022-08-21T18:19:56.6315422' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'53a57b1e-89a7-424b-94c4-0024406e666b', N'b390d9', 0, CAST(N'2022-08-01T20:21:11.8666854' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'4232997a-9a9f-4702-9cf0-12ede652dc08', N'87af97', 0, CAST(N'2022-08-20T13:02:08.3586278' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'f469fc03-6224-4cf3-8716-13f95000c8a5', N'5a39a1', 1, CAST(N'2022-08-21T16:12:54.4601877' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'48a09d33-6c2b-4824-90a1-1453a0438e03', N'709e72', 0, CAST(N'2022-08-20T13:09:21.6202546' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'987988a5-21b2-4cf8-94f5-1acf25afbb14', N'66eaa1', 1, CAST(N'2022-08-01T21:33:30.5069288' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'b21e0f72-8560-4ce6-b6f3-1d65e0a896c7', N'97b01d', 0, CAST(N'2022-08-20T12:57:31.8353345' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'764ef99b-1070-4c6b-b930-2a256e4f8a3a', N'4aa8b3', 0, CAST(N'2022-08-01T20:11:49.7803733' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'db1bcc37-91cb-4110-b40f-3040e8294584', N'e4346c', 0, CAST(N'2022-08-20T12:51:29.8724843' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'b9e497cc-28dd-4720-8f01-30ddf15728e2', N'aa4469', 1, CAST(N'2022-08-20T13:59:32.2459908' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'4143e6e3-ba8d-444a-8735-3140eaa4ef54', N'537b82', 1, CAST(N'2022-08-10T20:29:09.8482214' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'e19e7f6a-2a89-4f4a-87c9-36488ad94a20', N'560814', 0, CAST(N'2022-08-20T12:53:45.5170754' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'aa38d996-01cc-491a-bbe5-3aaf9a38083b', N'c78cb9', 0, CAST(N'2022-08-01T20:14:17.0393691' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'4a1d1797-9077-4780-8b92-3b1a6a05766e', N'11c67e', 0, CAST(N'2022-08-01T20:15:10.0982946' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'10b10e48-9698-404a-ae27-47e0b6d9c9fd', N'1293c3', 0, CAST(N'2022-08-01T20:06:37.8540614' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'b21185c8-ff78-4bd1-b587-4952a6bff7d0', N'3fef87', 0, CAST(N'2022-08-20T12:41:25.3142087' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'35c4052d-87cf-4516-962c-4a92622beb5b', N'642d6f', 0, CAST(N'2022-08-20T13:09:03.7126353' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'03df0459-be2b-434b-a6ea-4b245472cf1e', N'38557a', 1, CAST(N'2022-08-01T20:58:43.7963593' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'1025b773-8391-43c4-b7e6-4cad17342f39', N'5aed8f', 0, CAST(N'2022-08-20T13:18:34.1207042' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'23923c67-a6e0-4bc6-b28e-513a0ea64ee8', N'd4c634', 0, CAST(N'2022-08-01T20:56:52.1855094' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'948981b0-0536-4f10-a49a-52982389dadc', N'edf55e', 0, CAST(N'2022-08-10T21:02:57.6976610' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'db315933-6b0b-4248-b49b-56396b4609ce', N'c7057e', 1, CAST(N'2022-08-10T21:03:38.3886369' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'6ed53b32-2761-48db-b975-56fc373dd4c2', N'3f47f2', 0, CAST(N'2022-08-20T13:09:21.5100277' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'4df3725c-0070-43f2-b59e-648ec9eb1f27', N'e4c809', 1, CAST(N'2022-08-21T15:52:47.8629344' AS DateTime2), N'210516d0-474a-4db6-a5a5-08da6e72cec8')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'28d51332-6b01-42ea-b42b-657bf59d16b9', N'c45da3', 1, CAST(N'2022-08-10T21:08:58.2197305' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'a94c4f3a-881d-4050-9600-6c15e0c419c0', N'4cabe6', 0, CAST(N'2022-08-20T13:13:01.1273987' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'99a4b487-c78d-446f-8fe3-78416072785d', N'4bbab8', 1, CAST(N'2022-08-20T13:22:30.1884053' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'b238f7f9-a67f-4813-9c39-7fdb1569ea67', N'872c0f', 0, CAST(N'2022-08-20T13:17:34.9031732' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'35478d31-23c3-4e3e-94f8-8389e38f0acf', N'e894e7', 0, CAST(N'2022-08-01T20:32:47.9431156' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'fa629ac6-4c95-41ea-93ec-8d4df57596d8', N'65077c', 0, CAST(N'2022-08-20T13:08:43.7242479' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'76b333f3-fde8-4994-b339-97d04712de0f', N'75e80f', 0, CAST(N'2022-08-20T12:42:10.2634902' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'2cd96851-81b4-479f-b474-a9b076343fa2', N'691dc1', 0, CAST(N'2022-08-20T13:05:10.9827286' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'8afc9d12-06e8-475d-b825-ad45d7b70a6e', N'e1a1a2', 0, CAST(N'2022-08-20T12:51:21.8685403' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'c5634de7-64d3-49fb-a995-bce21d35ada7', N'01335a', 1, CAST(N'2022-08-10T21:09:45.1694333' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'ee26b1a9-5f6f-415a-8bc7-d123f6776c1a', N'ccf214', 0, CAST(N'2022-08-20T13:05:04.1617780' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'86fbceae-8cac-455d-8b57-e0df19d5087c', N'bd74e3', 0, CAST(N'2022-08-20T12:53:58.1256410' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'b74021b5-f13e-4d44-866b-e160531217bf', N'8b6194', 1, CAST(N'2022-08-20T14:01:36.2554133' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'043cbf0f-85c5-406e-b778-e77ee211330c', N'997ac7', 0, CAST(N'2022-08-01T20:37:00.2310703' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'7c5a04eb-2245-40c2-a745-edc92770f1e5', N'f9e9e5', 0, CAST(N'2022-08-20T13:06:31.5999464' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'0e203897-124a-4d7a-ba70-edc944f87ad9', N'a90db3', 0, CAST(N'2022-08-01T20:20:29.2167418' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'9c56e74c-4c71-4f71-8519-efb3743a2e14', N'03d3b7', 1, CAST(N'2022-08-14T15:37:12.9639142' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'75df22ab-6e53-4e59-9c65-f5204d1d2c57', N'1d45f0', 1, CAST(N'2022-08-01T21:36:05.0323335' AS DateTime2), N'f24e8df0-b5c7-461e-13d7-08da73d9df28')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'59c67aa5-9801-4d01-9ecc-f6eb5e69fdde', N'a4bcd9', 0, CAST(N'2022-08-01T20:17:02.5189130' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [dbo].[Verifications] ([Id], [Code], [IsConfirmed], [ExpireDate], [UserId]) VALUES (N'360fad2d-8048-423c-80aa-fcd51f4208df', N'774b9c', 0, CAST(N'2022-08-20T12:54:47.4897545' AS DateTime2), N'ceece2e6-4966-4652-a711-08da6b3bd31e')
GO
INSERT [identity].[asp_net_roles] ([Id], [Description], [IsDeleted], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2894bdee-64a3-40c3-9f93-08da6b322774', N'Admin', 0, N'Admin', N'ADMIN', N'82546337-954c-45d0-8dea-2f56e230aa6b')
GO
INSERT [identity].[asp_net_roles] ([Id], [Description], [IsDeleted], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9eb9f407-7459-49de-189c-08da6e6e173c', N'Desktop', 0, N'Desktop', N'DESKTOP', N'5ea69216-3e46-4c93-a26b-19147d8c6cac')
GO
INSERT [identity].[asp_net_roles] ([Id], [Description], [IsDeleted], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ca39052b-c121-4c58-189d-08da6e6e173c', N'Mobile', 0, N'Mobile', N'MOBILE', N'c94ba5ba-8dd1-4729-b1f7-26c77e6709e0')
GO
INSERT [identity].[asp_net_user_roles] ([UserId], [RoleId]) VALUES (N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'2894bdee-64a3-40c3-9f93-08da6b322774')
GO
INSERT [identity].[asp_net_user_roles] ([UserId], [RoleId]) VALUES (N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'9eb9f407-7459-49de-189c-08da6e6e173c')
GO
INSERT [identity].[asp_net_user_roles] ([UserId], [RoleId]) VALUES (N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'ca39052b-c121-4c58-189d-08da6e6e173c')
GO
INSERT [identity].[asp_net_user_roles] ([UserId], [RoleId]) VALUES (N'cf276b4c-9c1c-4112-af9a-08da8378861d', N'ca39052b-c121-4c58-189d-08da6e6e173c')
GO
INSERT [identity].[asp_net_users] ([Id], [FirstName], [LastName], [Gender], [IsDeleted], [CreatedDate], [RefreshToken], [RefreshTokenExpireDate], [CompanyId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'096af8d7-251b-4be5-581c-08da6a7be48a', N'string123', N'string123', 2, 0, CAST(N'2022-07-20T20:15:57.2116186' AS DateTime2), N'f4AB7v7OOUORYjhUWkpqkw==', CAST(N'2032-11-19T12:56:14.7965877' AS DateTime2), NULL, N'string', N'STRING', N'string123', N'STRING', 0, N'AQAAAAEAACcQAAAAEM3Q3Hec3PtmWHXzzQCVZ4c1aqPUyyUIcdxccgaQ+rOwdJYK6p6GF+E5azb13mb3aA==', N'EZAPJEQCWPYZIE5VD5BPWMQH5QAGI4I4', N'0b934053-54b8-4dce-9934-9c6efea29e33', N'string123', 0, 0, NULL, 1, 0)
GO
INSERT [identity].[asp_net_users] ([Id], [FirstName], [LastName], [Gender], [IsDeleted], [CreatedDate], [RefreshToken], [RefreshTokenExpireDate], [CompanyId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ceece2e6-4966-4652-a711-08da6b3bd31e', N'Adi', N'Hodzic', 1, 0, CAST(N'2022-07-21T19:10:05.8079432' AS DateTime2), N'FeMoPYlcjkadp0Kog9QUpw==', CAST(N'2032-11-26T16:07:44.1536119' AS DateTime2), NULL, N'adi', N'ADI', N'adihodzic94@gmail.com', N'ADIHODZIC94@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEDa1Zm+99qKqq+HljfWu+iaEKk5XSExIAv/BPX601aatad9i4SKT3qIgDBqHDikfSA==', N'GA6TR7STEZP7NNAPO7SY77P5ZDL7CYII', N'e79f99a6-c73a-4079-a4ed-751e82405e15', N'0626100682', 0, 0, NULL, 1, 0)
GO
INSERT [identity].[asp_net_users] ([Id], [FirstName], [LastName], [Gender], [IsDeleted], [CreatedDate], [RefreshToken], [RefreshTokenExpireDate], [CompanyId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'210516d0-474a-4db6-a5a5-08da6e72cec8', N'mobile', N'mobile', 2, 0, CAST(N'2022-07-25T21:20:59.9909083' AS DateTime2), N'lFocxSF3/0CEfEEGPxblQg==', CAST(N'2032-11-26T16:12:57.3361009' AS DateTime2), NULL, N'mobile', N'MOBILE', N'sgakls94@gmail.com', N'SGAKLS94@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAECsLyHp+7AMtWBpG69rlGzEvPat5uJB2YObPvb+6OH8DAAIGbPimP3IJICaW1BxvEw==', N'RSVETBUU65CUOZLKFVNJX2BRDJ7VXTUD', N'c6ae8462-bdaa-4cba-929e-8aa1bcb9e061', N'mobile', 0, 0, NULL, 1, 0)
GO
INSERT [identity].[asp_net_users] ([Id], [FirstName], [LastName], [Gender], [IsDeleted], [CreatedDate], [RefreshToken], [RefreshTokenExpireDate], [CompanyId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f24e8df0-b5c7-461e-13d7-08da73d9df28', N'adi_ap', N'adi_ap', 1, 0, CAST(N'2022-08-01T18:21:21.3249363' AS DateTime2), NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, N'adi_ap', N'ADI_AP', N't', N't', 0, N'AQAAAAEAACcQAAAAENGSSuML//qTkDVBw7Y04OszWzkQFew8RaMhcZy6uQIPUeeLAsHUxq7S8yADdiTelQ==', N'P4WFS3OBA34F25FOGRHBP6UUCGKTOUX5', N'8cc0b406-ea1c-4d30-9552-bc8dddf6d181', N'387288121', 0, 0, NULL, 1, 0)
GO
INSERT [identity].[asp_net_users] ([Id], [FirstName], [LastName], [Gender], [IsDeleted], [CreatedDate], [RefreshToken], [RefreshTokenExpireDate], [CompanyId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'cf276b4c-9c1c-4112-af9a-08da8378861d', N'testni', N'korisnik', 2, 0, CAST(N'2022-08-21T15:24:49.5440854' AS DateTime2), N'XXa6WDEiLEy4zRLyCsqL3w==', CAST(N'2032-11-26T13:41:28.6346582' AS DateTime2), NULL, N'test', N'TEST', N'test23@gmail.com', N'TEST2@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAENycCYaR5ar+esnY8y8GXV9sR5GDpqtFj5QDZcnJnRgjLfQT1wY3MyWGlqbNvhZ3ig==', N'K6OBAVKPZYOO4S3O4DMHDMS6N7XEXN5D', N'475ce3d9-0fbd-49c2-81ef-4bdce774f3af', N'38762610068', 0, 0, NULL, 1, 0)
GO

UPDATE  [identity].[asp_net_users]
SET CompanyId = 'A2930258-A0F3-4416-BA96-08DA6A9528DA'
WHERE Id = 'CEECE2E6-4966-4652-A711-08DA6B3BD31E'

UPDATE  [identity].[asp_net_users]
SET CompanyId = 'D79371B5-6649-4106-802F-BA36D5BB425F'
WHERE Id = 'F24E8DF0-B5C7-461E-13D7-08DA73D9DF28'
/****** Object:  Index [IX_Bills_CompanyId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Bills_CompanyId] ON [dbo].[Bills]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bills_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Bills_CreatedByUserId] ON [dbo].[Bills]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bills_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Bills_ModifiedByUserId] ON [dbo].[Bills]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bills_ReservationId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Bills_ReservationId] ON [dbo].[Bills]
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Chat_UserFromId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Chat_UserFromId] ON [dbo].[Chat]
(
	[UserFromId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Chat_UserToId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Chat_UserToId] ON [dbo].[Chat]
(
	[UserToId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cities_CountryId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Cities_CountryId] ON [dbo].[Cities]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cities_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Cities_CreatedByUserId] ON [dbo].[Cities]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cities_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Cities_ModifiedByUserId] ON [dbo].[Cities]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Companies_LocationId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Companies_LocationId] ON [dbo].[Companies]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Companies_LogoId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Companies_LogoId] ON [dbo].[Companies]
(
	[LogoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Countries_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Countries_CreatedByUserId] ON [dbo].[Countries]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Countries_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Countries_ModifiedByUserId] ON [dbo].[Countries]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Emails_UserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Emails_UserId] ON [dbo].[Emails]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Guests_CompanyId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Guests_CompanyId] ON [dbo].[Guests]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Guests_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Guests_CreatedByUserId] ON [dbo].[Guests]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Guests_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Guests_ModifiedByUserId] ON [dbo].[Guests]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Images_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Images_CreatedByUserId] ON [dbo].[Images]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Images_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Images_ModifiedByUserId] ON [dbo].[Images]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Images_UserProfilePictureId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Images_UserProfilePictureId] ON [dbo].[Images]
(
	[UserProfilePictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Locations_CityId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Locations_CityId] ON [dbo].[Locations]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Locations_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Locations_CreatedByUserId] ON [dbo].[Locations]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Locations_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Locations_ModifiedByUserId] ON [dbo].[Locations]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reservations_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Reservations_CreatedByUserId] ON [dbo].[Reservations]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reservations_GuestId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Reservations_GuestId] ON [dbo].[Reservations]
(
	[GuestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reservations_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Reservations_ModifiedByUserId] ON [dbo].[Reservations]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reservations_RoomId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Reservations_RoomId] ON [dbo].[Reservations]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReservationServices_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_ReservationServices_CreatedByUserId] ON [dbo].[ReservationServices]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReservationServices_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_ReservationServices_ModifiedByUserId] ON [dbo].[ReservationServices]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReservationServices_ReservationId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_ReservationServices_ReservationId] ON [dbo].[ReservationServices]
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReservationServices_ServiceId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_ReservationServices_ServiceId] ON [dbo].[ReservationServices]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_CompanyId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_CompanyId] ON [dbo].[Reviews]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_CreatedByUserId] ON [dbo].[Reviews]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_ModifiedByUserId] ON [dbo].[Reviews]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Rooms_CompanyId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Rooms_CompanyId] ON [dbo].[Rooms]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Rooms_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Rooms_CreatedByUserId] ON [dbo].[Rooms]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Rooms_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Rooms_ModifiedByUserId] ON [dbo].[Rooms]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Services_CreatedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Services_CreatedByUserId] ON [dbo].[Services]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Services_ModifiedByUserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Services_ModifiedByUserId] ON [dbo].[Services]
(
	[ModifiedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_Verifications_UserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_Verifications_UserId] ON [dbo].[Verifications]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_asp_net_role_claims_RoleId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_asp_net_role_claims_RoleId] ON [identity].[asp_net_role_claims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 21/08/2022 18:57:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [identity].[asp_net_roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_asp_net_user_claims_UserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_asp_net_user_claims_UserId] ON [identity].[asp_net_user_claims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_asp_net_user_logins_UserId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_asp_net_user_logins_UserId] ON [identity].[asp_net_user_logins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_asp_net_user_roles_RoleId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_asp_net_user_roles_RoleId] ON [identity].[asp_net_user_roles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [identity].[asp_net_users]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
/****** Object:  Index [IX_asp_net_users_CompanyId]    Script Date: 21/08/2022 18:57:24 ******/
CREATE NONCLUSTERED INDEX [IX_asp_net_users_CompanyId] ON [identity].[asp_net_users]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 21/08/2022 18:57:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [identity].[asp_net_users]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_Companies_CompanyId]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_Reservations_ReservationId] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[Reservations] ([Id])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_Reservations_ReservationId]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_asp_net_users_UserFromId] FOREIGN KEY([UserFromId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_asp_net_users_UserFromId]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_asp_net_users_UserToId] FOREIGN KEY([UserToId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_asp_net_users_UserToId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Countries_CountryId]
GO
ALTER TABLE [dbo].[Companies]  WITH CHECK ADD  CONSTRAINT [FK_Companies_Images_LogoId] FOREIGN KEY([LogoId])
REFERENCES [dbo].[Images] ([Id])
GO
ALTER TABLE [dbo].[Companies] CHECK CONSTRAINT [FK_Companies_Images_LogoId]
GO
ALTER TABLE [dbo].[Companies]  WITH CHECK ADD  CONSTRAINT [FK_Companies_Locations_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[Companies] CHECK CONSTRAINT [FK_Companies_Locations_LocationId]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Emails]  WITH CHECK ADD  CONSTRAINT [FK_Emails_asp_net_users_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Emails] CHECK CONSTRAINT [FK_Emails_asp_net_users_UserId]
GO
ALTER TABLE [dbo].[Guests]  WITH CHECK ADD  CONSTRAINT [FK_Guests_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Guests] CHECK CONSTRAINT [FK_Guests_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Guests]  WITH CHECK ADD  CONSTRAINT [FK_Guests_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Guests] CHECK CONSTRAINT [FK_Guests_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Guests]  WITH CHECK ADD  CONSTRAINT [FK_Guests_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Guests] CHECK CONSTRAINT [FK_Guests_Companies_CompanyId]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_asp_net_users_UserProfilePictureId] FOREIGN KEY([UserProfilePictureId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_asp_net_users_UserProfilePictureId]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_Cities_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_Cities_CityId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_Guests_GuestId] FOREIGN KEY([GuestId])
REFERENCES [dbo].[Guests] ([Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_Guests_GuestId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_Rooms_RoomId]
GO
ALTER TABLE [dbo].[ReservationServices]  WITH CHECK ADD  CONSTRAINT [FK_ReservationServices_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[ReservationServices] CHECK CONSTRAINT [FK_ReservationServices_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[ReservationServices]  WITH CHECK ADD  CONSTRAINT [FK_ReservationServices_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[ReservationServices] CHECK CONSTRAINT [FK_ReservationServices_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[ReservationServices]  WITH CHECK ADD  CONSTRAINT [FK_ReservationServices_Reservations_ReservationId] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[Reservations] ([Id])
GO
ALTER TABLE [dbo].[ReservationServices] CHECK CONSTRAINT [FK_ReservationServices_Reservations_ReservationId]
GO
ALTER TABLE [dbo].[ReservationServices]  WITH CHECK ADD  CONSTRAINT [FK_ReservationServices_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[ReservationServices] CHECK CONSTRAINT [FK_ReservationServices_Services_ServiceId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Companies_CompanyId]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_Companies_CompanyId]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_asp_net_users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_asp_net_users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_asp_net_users_ModifiedByUserId] FOREIGN KEY([ModifiedByUserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_asp_net_users_ModifiedByUserId]
GO
ALTER TABLE [dbo].[Verifications]  WITH CHECK ADD  CONSTRAINT [FK_Verifications_asp_net_users_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [dbo].[Verifications] CHECK CONSTRAINT [FK_Verifications_asp_net_users_UserId]
GO
ALTER TABLE [identity].[asp_net_role_claims]  WITH CHECK ADD  CONSTRAINT [FK_asp_net_role_claims_asp_net_roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [identity].[asp_net_roles] ([Id])
GO
ALTER TABLE [identity].[asp_net_role_claims] CHECK CONSTRAINT [FK_asp_net_role_claims_asp_net_roles_RoleId]
GO
ALTER TABLE [identity].[asp_net_user_claims]  WITH CHECK ADD  CONSTRAINT [FK_asp_net_user_claims_asp_net_users_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [identity].[asp_net_user_claims] CHECK CONSTRAINT [FK_asp_net_user_claims_asp_net_users_UserId]
GO
ALTER TABLE [identity].[asp_net_user_logins]  WITH CHECK ADD  CONSTRAINT [FK_asp_net_user_logins_asp_net_users_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [identity].[asp_net_user_logins] CHECK CONSTRAINT [FK_asp_net_user_logins_asp_net_users_UserId]
GO
ALTER TABLE [identity].[asp_net_user_roles]  WITH CHECK ADD  CONSTRAINT [FK_asp_net_user_roles_asp_net_roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [identity].[asp_net_roles] ([Id])
GO
ALTER TABLE [identity].[asp_net_user_roles] CHECK CONSTRAINT [FK_asp_net_user_roles_asp_net_roles_RoleId]
GO
ALTER TABLE [identity].[asp_net_user_roles]  WITH CHECK ADD  CONSTRAINT [FK_asp_net_user_roles_asp_net_users_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [identity].[asp_net_user_roles] CHECK CONSTRAINT [FK_asp_net_user_roles_asp_net_users_UserId]
GO
ALTER TABLE [identity].[asp_net_user_tokens]  WITH CHECK ADD  CONSTRAINT [FK_asp_net_user_tokens_asp_net_users_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[asp_net_users] ([Id])
GO
ALTER TABLE [identity].[asp_net_user_tokens] CHECK CONSTRAINT [FK_asp_net_user_tokens_asp_net_users_UserId]
GO
ALTER TABLE [identity].[asp_net_users]  WITH CHECK ADD  CONSTRAINT [FK_asp_net_users_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [identity].[asp_net_users] CHECK CONSTRAINT [FK_asp_net_users_Companies_CompanyId]
GO
USE [master]
GO
ALTER DATABASE [ERes] SET  READ_WRITE 
GO
