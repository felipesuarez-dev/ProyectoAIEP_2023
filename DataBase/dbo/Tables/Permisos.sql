CREATE TABLE [dbo].[Permisos](
	[id_permiso] [int] IDENTITY(1,1) NOT NULL,
	[nombre_permiso] [nvarchar](50) NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
	[bol_activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permisos] ADD  DEFAULT (getdate()) FOR [fecha_creacion]