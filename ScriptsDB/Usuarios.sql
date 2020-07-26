USE [InventarioGamma]
GO

/****** Object:  Table [db_accessadmin].[Usuarios]    Script Date: 4/12/2016 11:02:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [db_accessadmin].[Usuarios](
	[NombreUsuario] [varchar](50) NOT NULL,
	[Llave] [varchar](512) NOT NULL,
	[Almacen] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


