CREATE TABLE [dbo].[AsignacionMenu](
	[id_asignacion] [int] IDENTITY(1,1) NOT NULL,
	[id_rol] [int] NOT NULL,
	[id_menu] [int] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
	[bol_activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_asignacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AsignacionMenu]  WITH CHECK ADD FOREIGN KEY([id_menu])
REFERENCES [dbo].[Menu] ([id_menu])
GO
ALTER TABLE [dbo].[AsignacionMenu]  WITH CHECK ADD FOREIGN KEY([id_rol])
REFERENCES [dbo].[Roles] ([id_rol])