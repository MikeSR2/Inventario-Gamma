USE [InventarioGamma]
GO

/****** Object:  Table [dbo].[Historial]    Script Date: 4/12/2016 11:02:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Historial](
	[IdHistorial] [int] IDENTITY(1,1) NOT NULL,
	[Producto] [int] NOT NULL,
	[Fecha_Movimiento] [date] NOT NULL,
	[Tipo_Movimiento] [varchar](50) NOT NULL,
	[Origen] [varchar](50) NULL,
	[Destino] [varchar](50) NULL,
	[Cantidad] [float] NOT NULL,
	[Cantidad_Anterior] [float] NOT NULL,
	[Cantidad_Actual] [float] NOT NULL,
 CONSTRAINT [PK_Historial] PRIMARY KEY CLUSTERED 
(
	[IdHistorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Historial]  WITH CHECK ADD  CONSTRAINT [FK_Historial_Productos] FOREIGN KEY([Producto])
REFERENCES [dbo].[Productos] ([IdProducto])
GO

ALTER TABLE [dbo].[Historial] CHECK CONSTRAINT [FK_Historial_Productos]
GO


