CREATE TABLE [dbo].[TipoMenu](
	[id_tipo_menu] [int] IDENTITY(1,1) NOT NULL,
	[descripcion_menu] [nvarchar](max) NOT NULL,
	[fec_registro] [datetime] NOT NULL,
	[fec_modificacion] [datetime] NOT NULL,
	[bol_activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tipo_menu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TipoMenu] ADD  DEFAULT (getdate()) FOR [fec_registro]
GO
ALTER TABLE [dbo].[TipoMenu] ADD  DEFAULT (getdate()) FOR [fec_modificacion]