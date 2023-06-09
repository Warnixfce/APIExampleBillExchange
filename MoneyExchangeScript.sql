USE [master]
GO
/****** Object:  Database [MoneyExchange]    Script Date: 5/9/2023 11:01:32 AM ******/
CREATE DATABASE [MoneyExchange]
 CONTAINMENT = NONE
GO
ALTER DATABASE [MoneyExchange] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MoneyExchange].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MoneyExchange] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MoneyExchange] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MoneyExchange] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MoneyExchange] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MoneyExchange] SET ARITHABORT OFF 
GO
ALTER DATABASE [MoneyExchange] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MoneyExchange] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MoneyExchange] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MoneyExchange] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MoneyExchange] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MoneyExchange] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MoneyExchange] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MoneyExchange] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MoneyExchange] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MoneyExchange] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MoneyExchange] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MoneyExchange] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MoneyExchange] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MoneyExchange] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MoneyExchange] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MoneyExchange] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MoneyExchange] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MoneyExchange] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MoneyExchange] SET  MULTI_USER 
GO
ALTER DATABASE [MoneyExchange] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MoneyExchange] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MoneyExchange] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MoneyExchange] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MoneyExchange] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MoneyExchange] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MoneyExchange] SET QUERY_STORE = OFF
GO
USE [MoneyExchange]
GO
/****** Object:  Table [dbo].[Divisa]    Script Date: 5/9/2023 11:01:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Divisa](
	[ID_Divisa] [int] IDENTITY(1,1) NOT NULL,
	[ID_Tipo_Divisa] [int] NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Divisa] PRIMARY KEY CLUSTERED 
(
	[ID_Divisa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operacion]    Script Date: 5/9/2023 11:01:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operacion](
	[ID_Operacion] [int] IDENTITY(1,1) NOT NULL,
	[MontoAPagar] [decimal](20, 2) NOT NULL,
	[MontoPagado] [decimal](20, 2) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
 CONSTRAINT [PK_Operacion] PRIMARY KEY CLUSTERED 
(
	[ID_Operacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Divisa]    Script Date: 5/9/2023 11:01:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Divisa](
	[ID_Tipo_Divisa] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tipo_Divisa] PRIMARY KEY CLUSTERED 
(
	[ID_Tipo_Divisa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaccion_Cambio]    Script Date: 5/9/2023 11:01:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaccion_Cambio](
	[ID_Transaccion_Cambio] [int] IDENTITY(1,1) NOT NULL,
	[ID_Divisa] [int] NOT NULL,
	[CantidadDivisa] [int] NOT NULL,
	[ID_Operacion] [int] NOT NULL,
 CONSTRAINT [PK_Transaccion_Cambio] PRIMARY KEY CLUSTERED 
(
	[ID_Transaccion_Cambio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Divisa] ON 

INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (1, 1, CAST(10.00 AS Decimal(18, 2)), N'Billete de AR$10')
INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (2, 1, CAST(20.00 AS Decimal(18, 2)), N'Billete de AR$20')
INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (3, 1, CAST(50.00 AS Decimal(18, 2)), N'Billete de AR$50')
INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (4, 1, CAST(100.00 AS Decimal(18, 2)), N'Billete de AR$100')
INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (5, 2, CAST(0.01 AS Decimal(18, 2)), N'Moneda de AR$0,01')
INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (6, 2, CAST(0.05 AS Decimal(18, 2)), N'Moneda de AR$0,05')
INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (7, 2, CAST(0.10 AS Decimal(18, 2)), N'Moneda de AR$0,10')
INSERT [dbo].[Divisa] ([ID_Divisa], [ID_Tipo_Divisa], [Valor], [Descripcion]) VALUES (8, 2, CAST(0.50 AS Decimal(18, 2)), N'Moneda de AR$0,50')
SET IDENTITY_INSERT [dbo].[Divisa] OFF
GO
SET IDENTITY_INSERT [dbo].[Operacion] ON 

INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (1, CAST(10.00 AS Decimal(20, 2)), CAST(40.00 AS Decimal(20, 2)), CAST(N'2023-04-10T18:45:18.970' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (2, CAST(20.00 AS Decimal(20, 2)), CAST(100.00 AS Decimal(20, 2)), CAST(N'2023-04-21T17:12:45.730' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (4, CAST(60.00 AS Decimal(20, 2)), CAST(120.00 AS Decimal(20, 2)), CAST(N'2023-05-04T10:37:20.630' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (5, CAST(25.00 AS Decimal(20, 2)), CAST(50.00 AS Decimal(20, 2)), CAST(N'2023-05-04T15:45:14.130' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (6, CAST(40.00 AS Decimal(20, 2)), CAST(50.00 AS Decimal(20, 2)), CAST(N'2023-05-04T16:26:39.750' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (7, CAST(500.00 AS Decimal(20, 2)), CAST(600.00 AS Decimal(20, 2)), CAST(N'2023-05-04T17:21:34.070' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (8, CAST(130.00 AS Decimal(20, 2)), CAST(200.00 AS Decimal(20, 2)), CAST(N'2023-05-04T18:18:04.183' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (9, CAST(14.00 AS Decimal(20, 2)), CAST(20.00 AS Decimal(20, 2)), CAST(N'2023-05-04T18:38:37.503' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (11, CAST(30.00 AS Decimal(20, 2)), CAST(50.00 AS Decimal(20, 2)), CAST(N'2023-05-07T18:03:18.410' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (12, CAST(90.00 AS Decimal(20, 2)), CAST(100.00 AS Decimal(20, 2)), CAST(N'2023-05-07T18:05:08.720' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (14, CAST(100.00 AS Decimal(20, 2)), CAST(165.00 AS Decimal(20, 2)), CAST(N'2023-05-08T10:29:33.570' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (15, CAST(18.00 AS Decimal(20, 2)), CAST(30.00 AS Decimal(20, 2)), CAST(N'2023-05-08T11:00:27.423' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (16, CAST(33.00 AS Decimal(20, 2)), CAST(50.00 AS Decimal(20, 2)), CAST(N'2023-05-08T16:34:18.503' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (17, CAST(1500.00 AS Decimal(20, 2)), CAST(1550.00 AS Decimal(20, 2)), CAST(N'2023-05-08T16:35:37.967' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (18, CAST(15.00 AS Decimal(20, 2)), CAST(25.00 AS Decimal(20, 2)), CAST(N'2023-05-08T16:36:11.640' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (19, CAST(10.30 AS Decimal(20, 2)), CAST(15.00 AS Decimal(20, 2)), CAST(N'2023-05-08T16:37:19.663' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (20, CAST(16.50 AS Decimal(20, 2)), CAST(50.00 AS Decimal(20, 2)), CAST(N'2023-05-08T17:39:39.853' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (21, CAST(98.99 AS Decimal(20, 2)), CAST(100.00 AS Decimal(20, 2)), CAST(N'2023-05-08T17:40:53.107' AS DateTime))
INSERT [dbo].[Operacion] ([ID_Operacion], [MontoAPagar], [MontoPagado], [FechaHora]) VALUES (22, CAST(39.99 AS Decimal(20, 2)), CAST(50.00 AS Decimal(20, 2)), CAST(N'2023-05-08T19:06:57.833' AS DateTime))
SET IDENTITY_INSERT [dbo].[Operacion] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_Divisa] ON 

INSERT [dbo].[Tipo_Divisa] ([ID_Tipo_Divisa], [Nombre]) VALUES (1, N'Billete')
INSERT [dbo].[Tipo_Divisa] ([ID_Tipo_Divisa], [Nombre]) VALUES (2, N'Moneda')
SET IDENTITY_INSERT [dbo].[Tipo_Divisa] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaccion_Cambio] ON 

INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (1, 2, 1, 1)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (2, 1, 1, 1)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (3, 3, 1, 2)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (4, 2, 1, 2)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (5, 1, 1, 2)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (6, 2, 1, 5)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (7, 8, 10, 5)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (8, 1, 1, 6)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (9, 4, 1, 7)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (10, 3, 1, 8)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (11, 2, 1, 8)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (12, 8, 12, 9)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (14, 2, 1, 11)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (15, 1, 1, 12)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (17, 3, 1, 14)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (18, 1, 1, 14)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (19, 8, 10, 14)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (20, 1, 1, 15)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (21, 8, 4, 15)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (22, 1, 1, 16)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (23, 8, 14, 16)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (24, 3, 1, 17)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (25, 1, 1, 18)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (26, 8, 9, 19)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (27, 7, 2, 19)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (28, 2, 1, 20)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (29, 1, 1, 20)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (30, 8, 7, 20)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (31, 8, 2, 21)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (32, 5, 1, 21)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (33, 1, 1, 22)
INSERT [dbo].[Transaccion_Cambio] ([ID_Transaccion_Cambio], [ID_Divisa], [CantidadDivisa], [ID_Operacion]) VALUES (34, 5, 1, 22)
SET IDENTITY_INSERT [dbo].[Transaccion_Cambio] OFF
GO
ALTER TABLE [dbo].[Divisa]  WITH CHECK ADD  CONSTRAINT [FK_Divisa_Tipo_Divisa] FOREIGN KEY([ID_Tipo_Divisa])
REFERENCES [dbo].[Tipo_Divisa] ([ID_Tipo_Divisa])
GO
ALTER TABLE [dbo].[Divisa] CHECK CONSTRAINT [FK_Divisa_Tipo_Divisa]
GO
ALTER TABLE [dbo].[Transaccion_Cambio]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Cambio_Divisa] FOREIGN KEY([ID_Divisa])
REFERENCES [dbo].[Divisa] ([ID_Divisa])
GO
ALTER TABLE [dbo].[Transaccion_Cambio] CHECK CONSTRAINT [FK_Transaccion_Cambio_Divisa]
GO
ALTER TABLE [dbo].[Transaccion_Cambio]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Cambio_Operacion] FOREIGN KEY([ID_Operacion])
REFERENCES [dbo].[Operacion] ([ID_Operacion])
GO
ALTER TABLE [dbo].[Transaccion_Cambio] CHECK CONSTRAINT [FK_Transaccion_Cambio_Operacion]
GO
USE [master]
GO
ALTER DATABASE [MoneyExchange] SET  READ_WRITE 
GO
