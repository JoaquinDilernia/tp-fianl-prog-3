USE [master]
GO
/****** Object:  Database [Prog3_Integrador]    Script Date: 2/7/2021 10:29:39 ******/
CREATE DATABASE [Prog3_Integrador]
 
ALTER DATABASE [Prog3_Integrador] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Prog3_Integrador].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Prog3_Integrador] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET ARITHABORT OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Prog3_Integrador] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Prog3_Integrador] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Prog3_Integrador] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Prog3_Integrador] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Prog3_Integrador] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Prog3_Integrador] SET  MULTI_USER 
GO
ALTER DATABASE [Prog3_Integrador] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Prog3_Integrador] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Prog3_Integrador] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Prog3_Integrador] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Prog3_Integrador] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Prog3_Integrador] SET QUERY_STORE = OFF
GO
USE [Prog3_Integrador]
GO
/****** Object:  Table [dbo].[Articulo]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Cat] [int] NOT NULL,
	[PrecioUnitario] [decimal](8, 2) NULL,
	[Nombre_Art] [nvarchar](200) NOT NULL,
	[Estado] [bit] NULL,
	[Url_Imagen] [varchar](50) NOT NULL,
	[StockArticulo] [int] NOT NULL,
 CONSTRAINT [PK__Articulo__3214EC07C61A2D2C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFacturas]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFacturas](
	[NumOrden] [int] IDENTITY (1, 1) NOT NULL,
	[Id_Factura] [int] NOT NULL,
	[Id_Articulo] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[DescripcionArticulo] [varchar](100) NOT NULL,
	[PrecioUnitario] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK__FactCabe__3214EC07DCB49AF4] PRIMARY KEY CLUSTERED 
(
	[NumOrden] ASC,
	[Id_Factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[Id_Factura] [int] IDENTITY(1,1) NOT NULL,
	[Dni_Usuario] [varchar](15) NOT NULL,
	[Fecha_Venta] [date] NOT NULL,
	[MontoFinal] [decimal](30, 2) NOT NULL,
 CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
(
	[Id_Factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[DniUsuario] [varchar](15) NOT NULL,
	[Rol] [bit] NOT NULL,
	[Mail] [nvarchar](60) NOT NULL,
	[Contraseña] [nvarchar](20) NOT NULL,
	[Estado] [bit] NOT NULL,
	[NombreUsuario] [varchar](100) NOT NULL,
	[ApellidoUsuario] [varchar](100) NOT NULL,
	[DireccionUsuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Usuario__3214EC07C7EEFC37] PRIMARY KEY CLUSTERED 
(
	[DniUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuariosXcarrito]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuariosXcarrito](
	[dni_Usuario] [varchar](15) NOT NULL,
	[id_articulo] [int] NOT NULL,
	[descripcionArticulo] [varchar](50) NOT NULL,
	[cantidad] [int] NOT NULL,
 CONSTRAINT [PK_usuariosXcarrito] PRIMARY KEY CLUSTERED 
(
	[dni_Usuario] ASC,
	[id_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_DetalleFacturas]    Script Date: 2/7/2021 10:29:39 ******/
CREATE NONCLUSTERED INDEX [IX_DetalleFacturas] ON [dbo].[DetalleFacturas]
(
	[NumOrden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[usuariosXcarrito]  WITH CHECK ADD  CONSTRAINT [FK_articulo] FOREIGN KEY([id_articulo])
REFERENCES [dbo].[Articulo] ([Id])
GO
ALTER TABLE [dbo].[usuariosXcarrito] CHECK CONSTRAINT [FK_articulo]
GO
ALTER TABLE [dbo].[usuariosXcarrito]  WITH CHECK ADD  CONSTRAINT [FK_dni] FOREIGN KEY([dni_Usuario])
REFERENCES [dbo].[Usuario] ([DniUsuario])
GO
ALTER TABLE [dbo].[usuariosXcarrito] CHECK CONSTRAINT [FK_dni]
GO
/****** Object:  StoredProcedure [dbo].[AgregarArticulo]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarArticulo]

	@idCat int,
	@Descripcion varchar(50),
	@precio decimal (8,2),
	@stock int,
	@urlImg varchar(50)

AS
BEGIN
	SET NOCOUNT ON;

	insert into Articulo (Id_Cat, Nombre_Art, PrecioUnitario, StockArticulo, Url_Imagen, Estado)
	values (@idCat,@Descripcion,@precio,@stock,@urlImg,1)

END
GO
/****** Object:  StoredProcedure [dbo].[AgregarArticuloCarrito]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarArticuloCarrito]

@dniUsuario varchar(15),
@IdArticulo int,
@Descripcion varchar(100)

AS
BEGIN
	SET NOCOUNT ON;

	insert into usuariosXcarrito(dni_Usuario, id_articulo, descripcionArticulo, cantidad)
	values(@dniUsuario, @IdArticulo, @Descripcion, 1)

END
GO
/****** Object:  StoredProcedure [dbo].[AgregarCategoria]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarCategoria]
	@Descripcion nvarchar(100)
AS
BEGIN
	insert into Categoria (Descripcion)
values  (@Descripcion)

END

	
GO
/****** Object:  StoredProcedure [dbo].[AgregarFactura]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarFactura]

	@dniUsuario varchar(15),
	@montoFinal decimal(30,2)

AS
BEGIN
	
	SET NOCOUNT ON;

   insert into Facturas (Dni_Usuario, Fecha_Venta, MontoFinal)
	values (@dniUsuario,(SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [DD/MM/YYYY]),@montoFinal)
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarUsuario]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarUsuario]
	@dni varchar(15),
	@nombre varchar(30),
	@apellido varchar(30),
	@mail varchar(50),
	@direccion varchar(50),
	@contra varchar(25)
AS
BEGIN
	
	SET NOCOUNT ON;

insert into Usuario (DniUsuario, NombreUsuario, ApellidoUsuario, Mail, Contraseña, Estado, Rol, DireccionUsuario)
values (@dni,@nombre,@apellido,@mail,@contra,1,1, @direccion)

END
GO
/****** Object:  StoredProcedure [dbo].[BajaLogicaArticulo]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BajaLogicaArticulo]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	update Articulo set estado = 0 where Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[BajaLogicaUsuario]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BajaLogicaUsuario]
	
	@Dni varchar(15)
AS
BEGIN

	SET NOCOUNT ON;

    update Usuario set Estado = 0 where DniUsuario = @Dni
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarArticulo]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarArticulo]
	@id int
AS
BEGIN
	SET NOCOUNT ON;

   delete from Articulo where Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[eliminarArticuloXcarrito]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[eliminarArticuloXcarrito]
@dni varchar(15),
@idArticulo int
as
delete from usuariosXcarrito where id_articulo = @idArticulo and dni_Usuario = @dni
GO
/****** Object:  StoredProcedure [dbo].[EliminarCategoria]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarCategoria]
	@Id INT

AS
BEGIN
	SET NOCOUNT ON;

   Delete from Categoria where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarUsuario] 
	@Dni varchar(15)
AS
BEGIN
	SET NOCOUNT ON;
	
	Delete from Usuario where DniUsuario = @Dni
END
GO
/****** Object:  StoredProcedure [dbo].[modificarArticulo]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE procedure [dbo].[modificarArticulo]
	@id int,
	@idCat int,
	@descripcion varchar(50),
	@precio decimal(8,2),
	@stock int
	as
	update Articulo set Nombre_Art = @descripcion, Id_Cat = @idCat, PrecioUnitario = @precio, StockArticulo = @stock where Id = @id
GO
/****** Object:  StoredProcedure [dbo].[ModificarCantidadCarrito]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ModificarCantidadCarrito] 
	@dni varchar(15),
	@idArticulo int,
	@cantidad int
AS
BEGIN
	SET NOCOUNT ON;
	
	update usuariosXcarrito set cantidad = @cantidad where id_articulo=@idArticulo and dni_Usuario = @dni
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarCategoria]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ModificarCategoria]
@IdCat int,
@DescripcionCat varchar(50)
as
update Categoria set Descripcion = @DescripcionCat where Id = @IdCat
GO
/****** Object:  StoredProcedure [dbo].[modificarRolUsuario]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[modificarRolUsuario]
@dni varchar(15),
@admin bit
as
update Usuario set Rol = @admin where DniUsuario = @dni 
GO
/****** Object:  StoredProcedure [dbo].[ModificarUsuario]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ModificarUsuario]
	@Dni varchar(15),
	@NuevaDireccion VARCHAR(50),
	@NuevaContra nvarchar(50),
	@NuevoNombre varchar(100),
	@NuevoApellido varchar(100),
	@NuevoMail nvarchar (60)
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE Usuario set DireccionUsuario = @NuevaDireccion, Contraseña = @NuevaContra, 
		    NombreUsuario = @NuevoNombre, ApellidoUsuario = @NuevoApellido, Mail = @NuevoMail
	where DniUsuario = @Dni
END
GO
/****** Object:  StoredProcedure [dbo].[restarStockArticulo]    Script Date: 2/7/2021 10:29:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	Create procedure [dbo].[restarStockArticulo]
	@id int,
	@stockVendido int
	as
	update Articulo set StockArticulo = StockArticulo - @stockVendido where Id = @id

/****** Object:  StoredProcedure [dbo].[AgregarDetalleFacturas]    Script Date: 3/7/2021 11:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[AgregarDetalleFacturas]
@IdFactura int,
@IdArticulo int,
@PrecioUnitario decimal (8,2),
@descripcionProducto varchar (50),
@cantidad int
as
INSERT INTO DetalleFacturas (Id_Factura, Id_Articulo, PrecioUnitario, DescripcionArticulo, Cantidad)
values (@IdFactura, @IdArticulo, @PrecioUnitario, @descripcionProducto, @cantidad)

UPDATE Articulo set StockArticulo = StockArticulo - @cantidad where Id = @IdArticulo

GO


INSERT INTO Categoria (Descripcion)
select 'Buzos y Sweaters' union
select 'Camperas' union
select 'Remeras' union
select 'Pantalones' union
select 'Bermudas y Trajes DeBaño' union
select 'Zapatillas' union
select 'Camisas' union
select 'Zapatos Y Tacones'

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)
select 1, 1790, 'Buzo lizo Negro', 'True', '~\Imagenes\BuzosYSweaters\1.jpg', 58 union
select 1, 11790, 'Buzo Jordan Gris','True',	'~\Imagenes\BuzosYSweaters\2.jpg',	42 union
select 1, 5630, 'Pullover lizo Color Crema', 'True', '~\Imagenes\BuzosYSweaters\3.jpg',	73 union
select 1, 8612, 'Buzo Nike', 'True', '~\Imagenes\BuzosYSweaters\4.jpg',	74 union
select 1, 3595, 'Buzo Drew Smile', 'True', '~\Imagenes\BuzosYSweaters\5.jpg', 46 union
select 1, 6790,	'Buzo USA',	'True',	'~\Imagenes\BuzosYSweaters\6.jpg', 37 union
select 1, 7859,	'Buzo Adidas Negro Mujer', 'True', '~\Imagenes\BuzosYSweaters\7.jpg', 46 union
select 1, 10790, 'Buzo Adidas Rojo', 'True', '~\Imagenes\BuzosYSweaters\8.jpg',	63 union
select 1, 7098,	'Buzo negro con Rosas Blancas', 'True',	'~\Imagenes\BuzosYSweaters\9.jpg', 50 union
select 1, 4897,	'Buzo Lisa Simpson', 'True', '~\Imagenes\BuzosYSweaters\10.jpg', 24 union
select 1, 7894,	'Sweater amarillo',	'True',	'~\Imagenes\BuzosYSweaters\11.jpg', 50 union
select 1, 8652,	'Sweater Marron Clarito',	'True',	'~\Imagenes\BuzosYSweaters\12.jpg',	47 union
select 1, 12236, 'Sweater Azul Oscuro',	'True',	'~\Imagenes\BuzosYSweaters\13.jpg',	50 union
select 1, 10430, 'Sweater para Ambos Sexos','True',	'~\Imagenes\BuzosYSweaters\14.jpg',	40 union
select 1, 7412,	'Sweater Rayas Azul y Blancas',	'True',	'~\Imagenes\BuzosYSweaters\15.jpg',	63

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)

select 2, 7859, 'Campera Montagne Negra', 'True', '~\Imagenes\Camperas\16.jpg', 65 union
select 2, 12369, 'Campera Columbia Rosa','True', '~\Imagenes\Camperas\17.jpg', 79 union
select 2, 17859, 'Campera Montagne', 'True', '~\Imagenes\Camperas\18.jpg',	65  union
select 2, 10254, 'Campera Nike Air', 'True', '~\Imagenes\Camperas\19.jpg',	50  union
select 2, 11123, 'Campera Nike RompeViento', 'True', '~\Imagenes\Camperas\20.jpg',	46 union
select 2, 25000, 'Campera Adidas Boca',	'True','~\Imagenes\Camperas\21.jpg', 65 union
select 2, 15000, 'Campera Montagne Negra con Mangas Naranja', 'True', '~\Imagenes\Camperas\22.jpg',	65 union
select 2, 14236, 'Campera Salomon Verde Marino', 'True', '~\Imagenes\Camperas\23.jpg', 65 union
select 2, 18236, 'Campera Beige', 'True', '~\Imagenes\Camperas\24.jpg',	65 union
select 2, 17236, 'Campera Nike Blanca y Negra',	'True', '~\Imagenes\Camperas\25.jpg', 85 union
select 2, 12236, 'Campera Alpha Marron Claro', 'True', '~\Imagenes\Camperas\26.jpg', 70 union
select 2, 19236, 'Campera New Balance Negra', 'True',	'~\Imagenes\Camperas\27.jpg', 55 union
select 2, 11236, 'Campera Verde Oscuro', 'True', '~\Imagenes\Camperas\28.jpg',	65 union
select 2, 17236, 'Campera Nike Negra', 'True', '~\Imagenes\Camperas\29.jpg', 65 union 
select 2, 17500, 'Campera Puma Bordo', 'True', '~\Imagenes\Camperas\30.jpg', 41

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)

select 3, 1456, 'Remera Adidas Blanca', 'True',	'~\Imagenes\Remeras\31.jpg', 70 union
select 3, 2100,	'Remera Montagne Gris Oscuro', 'True',	'~\Imagenes\Remeras\32.jpg', 46 union
select 3, 4520,	'Remera Star Wars', 'True', '~\Imagenes\Remeras\33.jpg', 70 union
select 3, 1756, 'Remera F.R.I.E.N.D.S Blanca', 'True', '~\Imagenes\Remeras\34.jpg',	70 union
select 3, 1456,	'Remera Negra',	'True',	'~\Imagenes\Remeras\35.jpg', 80 union
select 3, 1230,	'Remera Blanca', 'True', '~\Imagenes\Remeras\36.jpg', 94 union
select 3, 1456,	'Remera Violeta', 'True', '~\Imagenes\Remeras\37.jpg', 87 union
select 3, 1456,	'Remera Roja','True', '~\Imagenes\Remeras\38.jpg', 85 union
select 3, 2000,	'Remera Blanca Colorida', 'True', '~\Imagenes\Remeras\39.jpg',	63 union
select 3, 1500,	'Remera Nike Blanca', 'True', '~\Imagenes\Remeras\40.jpg',	41 union
select 3, 1752,	'Remera Perrito', 'True', '~\Imagenes\Remeras\41.jpg',	52 union
select 3, 2210,	'Remera Back To Future', 'True', '~\Imagenes\Remeras\42.jpg', 80 union
select 3, 1500,	'Remera Adidas Cebra',	'True', '~\Imagenes\Remeras\44.jpg', 40 union 
select 3, 1700,	'Remera Hollister Azul', 'True', '~\Imagenes\Remeras\45.jpg', 52

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)

select 4, 3859,	'Jean Claro', 'True', '~\Imagenes\Pantalones\46.jpg', 65 union
select 4, 2585,	'Pantalon True Naranja', 'True', '~\Imagenes\Pantalones\47.jpg', 79 union
select 4, 2200,	'Pantalon Gris', 'True', '~\Imagenes\Pantalones\48.jpg', 58 union 
select 4, 3000,	'Pantalon Lewis Naranja', 'True', '~\Imagenes\Pantalones\49.jpg', 65 union
select 4, 3500,	'Pantalon Deportivo Nike Negro'	,'True', '~\Imagenes\Pantalones\50.jpg', 65 union
select 4, 4562,	'Jean Roto Claro',	'True',	'~\Imagenes\Pantalones\51.jpg',	65 union
select 4, 3265, 'Jean Negro', 'True', '~\Imagenes\Pantalones\52.jpg', 23 union
select 4, 2412,	'Pantalon Azul', 'True', '~\Imagenes\Pantalones\53.jpg', 78 union
select 4, 4563,	'Jean Montagne Negro',	'True',	'~\Imagenes\Pantalones\54.jpg', 45 union
select 4, 2585,	'Pantalon Montagne Negro','True',	'~\Imagenes\Pantalones\55.jpg', 68 union
select 4, 2636,	'Pantalon True Beige Claro', 'True', '~\Imagenes\Pantalones\56.jpg', 48 union
select 4, 2585,	'Pantalon Campana Coral', 'True', '~\Imagenes\Pantalones\59.jpg', 68 union
select 4, 2100,	'Pantalon Nike Gris Oscuro', 'True', '~\Imagenes\Pantalones\60.jpg', 70 

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)

select 5, 4120, 'Traje de Baño Montagne Amarillo Hombre', 'True', '~\Imagenes\BermudasYTrajesDeBaño\61.jpg',	99 union
select 5, 5630,	'Traje de Baño Celeste con Palmeras Hombre', 'True', '~\Imagenes\BermudasYTrajesDeBaño\63.jpg',	73 union
select 5, 3612,	'Traje de baño Rojo liso Hombre', 'True', '~\Imagenes\BermudasYTrajesDeBaño\64.jpg', 74 union
select 5, 6790,	'Traje de baño Multicolor Hombre',	'True',	'~\Imagenes\BermudasYTrajesDeBaño\66.jpg', 50 union 
select 5, 9410,	'Traje de Baño Enterizo Negro con Rayas Naranjas Mujer', 'True', '~\Imagenes\BermudasYTrajesDeBaño\67.jpg',	30 union
select 5, 10790, 'Traje de Baño Adidas Negro Enterizo', 'True',	'~\Imagenes\BermudasYTrajesDeBaño\68.jpg',	64 union
select 5, 7098,	'Traje de Baño Blanco Mujer', 'True', '~\Imagenes\BermudasYTrajesDeBaño\69.jpg', 50 union
select 5, 4897,	'Traje de Baño Negro Una Pieza Mujer','True', '~\Imagenes\BermudasYTrajesDeBaño\70.jpg',24 union
select 5, 7894, 'Bermuda color Crema Hombre', 'True', '~\Imagenes\BermudasYTrajesDeBaño\71.jpg',50 union
select 5, 8652,	'Traje de Baño Negro Hombre', 'True', '~\Imagenes\BermudasYTrajesDeBaño\72.jpg',47 union
select 5, 12236, 'Bermuda Naranja Hombre', 'True',	'~\Imagenes\BermudasYTrajesDeBaño\73.jpg',	50 union
select 5, 10430, 'Short Montagne Mujer', 'True', '~\Imagenes\BermudasYTrajesDeBaño\74.jpg',	40 union
select 5, 7412,	'Bermuda Beige Mujer', 'True', '~\Imagenes\BermudasYTrajesDeBaño\75.jpg', 63 union
select 5, 7412,	'Bermuda Verde Oscuro Mujer', 'True', '~\Imagenes\BermudasYTrajesDeBaño\76.jpg', 63 union
select 5, 7412,	'Bermuda Jean Azul Clarito Mujer', 'True', '~\Imagenes\BermudasYTrajesDeBaño\77.jpg', 63 union
select 5, 7412,	'Bermuda Jean Azul Oscuro Mujer', 'True', '~\Imagenes\BermudasYTrajesDeBaño\78.jpg', 63 union
select 5, 7412,	'Bermuda Azul Mujer', 'True',	'~\Imagenes\BermudasYTrajesDeBaño\79.jpg',	63 union 
select 5, 7412,	'Bermuda Negra', 'True', '~\Imagenes\BermudasYTrajesDeBaño\80.jpg',	63

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)

select 6, 4000,	'Zapatilla Adidas Azul Oscuro',	'True',	'~\Imagenes\Zapatillas\81.jpg',	69 union
select 6, 3620,	'Zapatilla Fila Blanca', 'True', '~\Imagenes\Zapatillas\82.jpg', 52 union
select 6, 3800,	'Zapatilla Nike Air Max' ,'True', '~\Imagenes\Zapatillas\83.jpg',	62 union
select 6, 3546,	'Zapatilla Adidas Supertar', 'True', '~\Imagenes\Zapatillas\84.jpg', 70 union
select 6, 3999,	'Zapatilla Negra', 'True', '~\Imagenes\Zapatillas\86.jpg', 76 union
select 6, 3620,	'Zapatilla Adidas Negras con Rayas Azul y Verde', 'True',	'~\Imagenes\Zapatillas\87.jpg',	41 union
select 6, 3800,	'Zapatilla Adidas Negras y Blanca' ,'True',	'~\Imagenes\Zapatillas\88.jpg',	85 union
select 6, 3620,	'Zapatilla Puma Blanca', 'True', '~\Imagenes\Zapatillas\89.jpg', 29 union
select 6, 3620,	'Zapatilla Nike Oscuro', 'True', '~\Imagenes\Zapatillas\90.jpg', 70 union
select 6, 3500,	'Zapatilla New Balance' ,'True', '~\Imagenes\Zapatillas\92.jpg', 53 union
select 6, 3200,	'Zapatilla Adidas Rosa' ,'True', '~\Imagenes\Zapatillas\93.jpg', 69 union
select 6, 3963,	'Zapatilla Nike Negro y Gris', 'True', '~\Imagenes\Zapatillas\94.jpg',	68 union
select 6, 4200,	'Zapatilla Sarkany', 'True', '~\Imagenes\Zapatillas\95.jpg', 70 

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)

select 7, 4563,	'Camisa Azul claro Hombre',	'True',	'~/Imagenes/Camisas/97.jpg',36 union
select 7, 5412,	'Camisa Verde Agua Hombre',	'True',	'~/Imagenes/Camisas/98.jpg', 50 union 
select 7, 4635,	'Camisa Azul Floreada Hombre',	'True',	'~/Imagenes/Camisas/100.jpg',29 union
select 7, 10111, 'Camisa Blanca con Botones Negros Hombre',	'True',	'~/Imagenes/Camisas/101.jpg', 46 union
select 7, 12365, 'Camisa Azul Opaco Hombre', 'True', '~/Imagenes/Camisas/102.jpg',	46 union
select 7, 9514,	'Camisa Marron Claro y Oscuro Hombre',	'True',	'~/Imagenes/Camisas/103.jpg',46 union
select 7, 7896,	'Camisa Gris Cuadriculada Hombre',	'True',	'~/Imagenes/Camisas/104.jpg', 46 union
select 7, 6987,	'Camisa Naranja Mujer',	'True',	'~/Imagenes/Camisas/106.jpg', 46 union
select 7, 5852,	'Camisa Celeste Mujer','True', '~/Imagenes/Camisas/107.jpg',50 union
select 7, 4563,	'Camisa Rosa Mujer', 'True', '~/Imagenes/Camisas/108.jpg',74 union
select 7, 7532,	'Camisa Azul Oscuro', 'True', '~/Imagenes/Camisas/109.jpg',	64 union
select 7, 10456, 'Camisa Celeste Hombre', 'True', '~/Imagenes/Camisas/110.jpg',	62 

Insert into Articulo(Id_Cat, PrecioUnitario, Nombre_Art, Estado, Url_Imagen, StockArticulo)

select 8, 3600,	'Zapatos Negro', 'True', '~\Imagenes\ZapatosYTacones\111.jpg',70 union
select 8, 2300,	'Zapatos Rojos', 'True', '~\Imagenes\ZapatosYTacones\112.jpg',	94 union
select 8, 9636,	'Zapato Negro con suela Naranja', 'True', '~\Imagenes\ZapatosYTacones\113.jpg',	74 union
select 8, 9500,	'Zapato Negro con Punta Redonda', 'True', '~\Imagenes\ZapatosYTacones\114.jpg', 45 union 
select 8, 9636,	'Tacon Negro', 'True',	'~\Imagenes\ZapatosYTacones\115.jpg', 56 union
select 8, 3200,	'Tacon Rojo', 'True', '~\Imagenes\ZapatosYTacones\116.jpg', 70 union
select 8, 3200,	'Tacon Rosa', 'True', '~\Imagenes\ZapatosYTacones\117.jpg', 40 union
select 8, 2300,	'Tacon Rosa Opaco',	'True',	'~\Imagenes\ZapatosYTacones\118.jpg', 40 union 
select 8, 2785,	'Zapatos Marrones Oscuro', 'True', '~\Imagenes\ZapatosYTacones\119.jpg', 75 union
select 8, 3200,	'Zapatos Palermo Negro','True',	'~\Imagenes\ZapatosYTacones\120.jpg', 40 union
select 8, 2300,	'Tacon de Punta Rosa', 'True', '~\Imagenes\ZapatosYTacones\121.jpg', 40 union
select 8, 8700,	'Zapato Basico Negro', 'True', '~\Imagenes\ZapatosYTacones\122.jpg', 48 union
select 8, 9250,	'Tacon Vizzano Rojo', 'True', '~\Imagenes\ZapatosYTacones\123.jpg', 40 union
select 8, 3300,	'Tacon Punta Redonda Rosa', 'True',	'~\Imagenes\ZapatosYTacones\124.jpg', 40

update Articulo set Estado = 1

Insert into Usuario (NombreUsuario, ApellidoUsuario, Rol, Mail, Contraseña, DniUsuario, Estado, DireccionUsuario)

select 'Claudio', 'Fernandez', 'False', 'Claudio@gmail.com', '1234', '478596', 'True', 'Buenos Aires' union
Select 'Tamara', 'Herrera', 'False', 'Tamara@gmail.com', '4321', '695874', 'True', 'Buenos Aires' union
Select 'Juan Ignacio', 'Bellavitis', 'True', 'Juan.Bellavitis@gmail.com','1234', '42643178', 'True', 'Buenos Aires' union
select 'Julian','Riscala','True','JulianRiscala@hotmail.com','1234','40095822','True','Buenos Aires'