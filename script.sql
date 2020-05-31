USE [dentaldoctor]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 31/05/2020 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[CodeClient] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[LastName] [nchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Sexe] [nchar](50) NOT NULL,
	[Adresse] [nchar](50) NOT NULL,
	[Profession] [nchar](50) NOT NULL,
	[TelNumber] [nchar](50) NOT NULL,
	[Email] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[CodeClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dent]    Script Date: 31/05/2020 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dent](
	[CodeDent] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Dent] PRIMARY KEY CLUSTERED 
(
	[CodeDent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History]    Script Date: 31/05/2020 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[CodeHist] [nchar](10) NOT NULL,
	[CauseOFVisite] [nchar](50) NOT NULL,
	[DateVisite] [timestamp] NOT NULL,
	[CodeClient] [nchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interventions]    Script Date: 31/05/2020 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interventions](
	[CodeInt] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Cout] [decimal](18, 0) NOT NULL,
	[CodeClient] [int] NOT NULL,
	[CodeDent] [int] NOT NULL,
	[Nbre] [int] NOT NULL,
	[Acte] [nchar](40) NULL,
	[observation] [nchar](40) NULL,
 CONSTRAINT [PK_Interventions] PRIMARY KEY CLUSTERED 
(
	[CodeInt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RDV]    Script Date: 31/05/2020 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RDV](
	[CodeRDV] [int] IDENTITY(1,1) NOT NULL,
	[DateRDV] [date] NOT NULL,
	[Time] [varchar](50) NOT NULL,
	[Comments] [nvarchar](50) NOT NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_RDV] PRIMARY KEY CLUSTERED 
(
	[CodeRDV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31/05/2020 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[CodeUser] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[LastName] [nchar](10) NOT NULL,
	[Login] [nchar](10) NOT NULL,
	[Password] [nchar](10) NOT NULL,
	[role] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Interventions]  WITH CHECK ADD  CONSTRAINT [FK_Interventions_Client] FOREIGN KEY([CodeClient])
REFERENCES [dbo].[Client] ([CodeClient])
GO
ALTER TABLE [dbo].[Interventions] CHECK CONSTRAINT [FK_Interventions_Client]
GO
ALTER TABLE [dbo].[Interventions]  WITH CHECK ADD  CONSTRAINT [FK_Interventions_Dent] FOREIGN KEY([CodeDent])
REFERENCES [dbo].[Dent] ([CodeDent])
GO
ALTER TABLE [dbo].[Interventions] CHECK CONSTRAINT [FK_Interventions_Dent]
GO
ALTER TABLE [dbo].[RDV]  WITH CHECK ADD  CONSTRAINT [FK_RDV_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([CodeClient])
GO
ALTER TABLE [dbo].[RDV] CHECK CONSTRAINT [FK_RDV_Client]
GO
