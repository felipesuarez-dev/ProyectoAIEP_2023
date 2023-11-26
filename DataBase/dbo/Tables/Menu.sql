CREATE TABLE [dbo].[Menu](
	[id_menu] [int] IDENTITY(1,1) NOT NULL,
	[nombre_menu] [nvarchar](50) NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
	[bol_activo] [bit] NULL,
	[id_tipo_menu] [int] NULL,
	[id_menu_principal] [int] NULL,
	[libreria] [nvarchar](50) NULL,
	[componente] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_menu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([id_menu_principal])
REFERENCES [dbo].[Menu] ([id_menu])
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([id_tipo_menu])
REFERENCES [dbo].[TipoMenu] ([id_tipo_menu])
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([id_menu_principal])
REFERENCES [dbo].[Menu] ([id_menu])
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([id_tipo_menu])
REFERENCES [dbo].[TipoMenu] ([id_tipo_menu])