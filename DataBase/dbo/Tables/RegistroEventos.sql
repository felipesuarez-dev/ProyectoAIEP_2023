CREATE TABLE [dbo].[RegistroEventos](
	[id_evento] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[evento] [nvarchar](100) NOT NULL,
	[fecha_evento] [datetime] NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RegistroEventos]  WITH CHECK ADD FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[RegistroEventos] ADD  DEFAULT (getdate()) FOR [fecha_creacion]