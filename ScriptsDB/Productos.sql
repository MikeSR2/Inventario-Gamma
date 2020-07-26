USE [InventarioGamma]
GO

/****** Object:  Table [dbo].[Productos]    Script Date: 4/12/2016 11:02:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Productos](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Clave] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Marca] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[Presentacion] [varchar](100) NULL,
	[Precio_Costo] [float] NOT NULL,
	[Importe_Inventario] [float] NOT NULL,
	[Almacen] [varchar](50) NOT NULL,
	[Ubicacion] [varchar](50) NULL,
	[Existencia] [float] NOT NULL,
	[Estatus] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


