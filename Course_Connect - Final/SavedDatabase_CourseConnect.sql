USE [master]
GO
/****** Object:  Database [ConnectCourse]    Script Date: 11/05/2023 11:11:15 ******/
CREATE DATABASE [ConnectCourse] ON  PRIMARY 
( NAME = N'ConnectCourse', FILENAME = N'D:\KGU Documents\HK2 - NLCS\Database - ConnectCourse\ConnectCourse.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ConnectCourse_log', FILENAME = N'D:\KGU Documents\HK2 - NLCS\Database - ConnectCourse\ConnectCourse_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ConnectCourse] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ConnectCourse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ConnectCourse] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [ConnectCourse] SET ANSI_NULLS OFF
GO
ALTER DATABASE [ConnectCourse] SET ANSI_PADDING OFF
GO
ALTER DATABASE [ConnectCourse] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [ConnectCourse] SET ARITHABORT OFF
GO
ALTER DATABASE [ConnectCourse] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [ConnectCourse] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [ConnectCourse] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [ConnectCourse] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [ConnectCourse] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [ConnectCourse] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [ConnectCourse] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [ConnectCourse] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [ConnectCourse] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [ConnectCourse] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [ConnectCourse] SET  DISABLE_BROKER
GO
ALTER DATABASE [ConnectCourse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [ConnectCourse] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [ConnectCourse] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [ConnectCourse] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [ConnectCourse] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [ConnectCourse] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [ConnectCourse] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [ConnectCourse] SET  READ_WRITE
GO
ALTER DATABASE [ConnectCourse] SET RECOVERY SIMPLE
GO
ALTER DATABASE [ConnectCourse] SET  MULTI_USER
GO
ALTER DATABASE [ConnectCourse] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [ConnectCourse] SET DB_CHAINING OFF
GO
USE [ConnectCourse]
GO
/****** Object:  Table [dbo].[TaiKhoanQTV]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaiKhoanQTV](
	[MaTK] [varchar](10) NOT NULL,
	[TenTK] [varchar](50) NULL,
	[MatKhau] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaiKhoanHocVien]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaiKhoanHocVien](
	[maTK] [varchar](10) NOT NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[maTK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaiKhoanGiangVien]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaiKhoanGiangVien](
	[maTK] [varchar](10) NOT NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[maTK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HocPhan]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HocPhan](
	[MaHP] [varchar](10) NOT NULL,
	[TenHP] [nvarchar](100) NULL,
	[SoTC] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ThongTinHocVien]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ThongTinHocVien](
	[MaHV] [varchar](10) NOT NULL,
	[HoTen] [nvarchar](100) NULL,
	[GioiTinh] [nvarchar](5) NULL,
	[NgaySinh] [date] NULL,
	[Lop] [nvarchar](10) NULL,
	[Email] [varchar](100) NULL,
	[SoDT] [varchar](11) NULL,
 CONSTRAINT [PK__ThongTin__2725A6D20F975522] PRIMARY KEY CLUSTERED 
(
	[MaHV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ThongTinGiangVien]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ThongTinGiangVien](
	[MaGV] [varchar](10) NOT NULL,
	[HoTen] [nvarchar](100) NULL,
	[GioiTinh] [nvarchar](5) NULL,
	[NgaySinh] [date] NULL,
	[HocVi] [nvarchar](50) NULL,
	[Email] [varchar](100) NULL,
	[SoDT] [varchar](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GiangVien_HocPhan]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GiangVien_HocPhan](
	[MaGV] [varchar](10) NOT NULL,
	[MaHP] [varchar](10) NOT NULL,
 CONSTRAINT [PK_GiangVien_HocPhan] PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC,
	[MaHP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DangKyHocPhan]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DangKyHocPhan](
	[MaHV] [varchar](10) NOT NULL,
	[MaHP] [varchar](10) NOT NULL,
	[MaGV] [varchar](10) NOT NULL,
	[NgayDK] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHV] ASC,
	[MaHP] ASC,
	[MaGV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhomHoc]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhomHoc](
	[MaNhom] [varchar](10) NOT NULL,
	[MaHP] [varchar](10) NULL,
	[GiangVienDay] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhom] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LichHoc]    Script Date: 11/05/2023 11:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LichHoc](
	[MaNhom] [varchar](10) NOT NULL,
	[ThoiGianBatDau] [datetime] NULL,
	[ThoiGianKetThuc] [datetime] NULL,
	[GhiChuThem] [nvarchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_ThongTinHocVien_TaiKhoanHocVien]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[ThongTinHocVien]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinHocVien_TaiKhoanHocVien] FOREIGN KEY([MaHV])
REFERENCES [dbo].[TaiKhoanHocVien] ([maTK])
GO
ALTER TABLE [dbo].[ThongTinHocVien] CHECK CONSTRAINT [FK_ThongTinHocVien_TaiKhoanHocVien]
GO
/****** Object:  ForeignKey [FK__ThongTinGi__MaGV__08EA5793]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[ThongTinGiangVien]  WITH CHECK ADD FOREIGN KEY([MaGV])
REFERENCES [dbo].[TaiKhoanGiangVien] ([maTK])
GO
/****** Object:  ForeignKey [FK__GiangVien___MaGV__1DE57479]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[GiangVien_HocPhan]  WITH CHECK ADD FOREIGN KEY([MaGV])
REFERENCES [dbo].[ThongTinGiangVien] ([MaGV])
GO
/****** Object:  ForeignKey [FK__GiangVien___MaHP__1ED998B2]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[GiangVien_HocPhan]  WITH CHECK ADD FOREIGN KEY([MaHP])
REFERENCES [dbo].[HocPhan] ([MaHP])
GO
/****** Object:  ForeignKey [FK__DangKyHocP__MaGV__1B0907CE]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[DangKyHocPhan]  WITH CHECK ADD FOREIGN KEY([MaGV])
REFERENCES [dbo].[ThongTinGiangVien] ([MaGV])
GO
/****** Object:  ForeignKey [FK__DangKyHocP__MaHP__1A14E395]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[DangKyHocPhan]  WITH CHECK ADD FOREIGN KEY([MaHP])
REFERENCES [dbo].[HocPhan] ([MaHP])
GO
/****** Object:  ForeignKey [FK__DangKyHocP__MaHV__1920BF5C]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[DangKyHocPhan]  WITH CHECK ADD  CONSTRAINT [FK__DangKyHocP__MaHV__1920BF5C] FOREIGN KEY([MaHV])
REFERENCES [dbo].[ThongTinHocVien] ([MaHV])
GO
ALTER TABLE [dbo].[DangKyHocPhan] CHECK CONSTRAINT [FK__DangKyHocP__MaHV__1920BF5C]
GO
/****** Object:  ForeignKey [FK__NhomHoc__GiangVi__239E4DCF]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[NhomHoc]  WITH CHECK ADD FOREIGN KEY([GiangVienDay])
REFERENCES [dbo].[ThongTinGiangVien] ([MaGV])
GO
/****** Object:  ForeignKey [FK__NhomHoc__MaHP__24927208]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[NhomHoc]  WITH CHECK ADD FOREIGN KEY([MaHP])
REFERENCES [dbo].[HocPhan] ([MaHP])
GO
/****** Object:  ForeignKey [FK__LichHoc__MaNhom__267ABA7A]    Script Date: 11/05/2023 11:11:16 ******/
ALTER TABLE [dbo].[LichHoc]  WITH CHECK ADD FOREIGN KEY([MaNhom])
REFERENCES [dbo].[NhomHoc] ([MaNhom])
GO
