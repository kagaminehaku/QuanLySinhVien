USE [master]
GO
/****** Object:  Database [QuanlySV]    Script Date: 10/12/2023 3:31:36 PM ******/
CREATE DATABASE [QuanlySV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanlySV', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanlySV.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanlySV_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanlySV_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuanlySV] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanlySV].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanlySV] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanlySV] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanlySV] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanlySV] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanlySV] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanlySV] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanlySV] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanlySV] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanlySV] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanlySV] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanlySV] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanlySV] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanlySV] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanlySV] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanlySV] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanlySV] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanlySV] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanlySV] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanlySV] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanlySV] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanlySV] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanlySV] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanlySV] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanlySV] SET  MULTI_USER 
GO
ALTER DATABASE [QuanlySV] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanlySV] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanlySV] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanlySV] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanlySV] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanlySV] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanlySV', N'ON'
GO
ALTER DATABASE [QuanlySV] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuanlySV] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuanlySV]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 10/12/2023 3:31:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[MaLop] [char](3) NOT NULL,
	[TenLop] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sinhvien]    Script Date: 10/12/2023 3:31:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sinhvien](
	[MaSV] [char](6) NOT NULL,
	[HotenSV] [nvarchar](40) NULL,
	[BDay] [date] NULL,
	[MaLop] [char](3) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Sinhvien]  WITH CHECK ADD FOREIGN KEY([MaLop])
REFERENCES [dbo].[Lop] ([MaLop])
GO
USE [master]
GO
ALTER DATABASE [QuanlySV] SET  READ_WRITE 
GO
