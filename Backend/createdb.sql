USE [BoneMets]
GO

/****** Object:  Table [dbo].[MasterList]    Script Date: 21/10/2012 6:10:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MasterList](
	[MRN] [varchar](50) NOT NULL,
	[Firstname] [varchar](50) NULL,
	[Lastname] [varchar](50) NULL,
	[TreatmentDate] [datetime] NULL,
	[TreatmentField] [varchar](50) NULL,
	[Dose] [float] NOT NULL,
	[Fractions] [int] NOT NULL,
	[Site] [varchar](50) NOT NULL,
	[Facility] [varchar](50) NULL,
	[Machine] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

