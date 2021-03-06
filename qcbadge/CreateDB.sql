﻿USE [dcbadge]
GO

DROP TABLE [dbo].[qcbadge]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[qcbadge](
	[badgeid] [int] IDENTITY(1,1) NOT NULL,
	[0] [bit] DEFAULT 0 NOT NULL,
	[1] [bit] DEFAULT 0 NOT NULL,
	[2] [bit] DEFAULT 0 NOT NULL,
	[3] [bit] DEFAULT 0 NOT NULL,
	[4] [bit] DEFAULT 0 NOT NULL,
	[5] [bit] DEFAULT 0 NOT NULL,
	[6] [bit] DEFAULT 0 NOT NULL,
	[7] [bit] DEFAULT 0 NOT NULL,
	[8] [bit] DEFAULT 0 NOT NULL,
	[9] [bit] DEFAULT 0 NOT NULL,
	[10] [bit] DEFAULT 0 NOT NULL,
	[11] [bit] DEFAULT 0 NOT NULL,
	[12] [bit] DEFAULT 0 NOT NULL,
	[13] [bit] DEFAULT 0 NOT NULL,
	[14] [bit] DEFAULT 0 NOT NULL,
	[15] [bit] DEFAULT 0 NOT NULL,
	[16] [bit] DEFAULT 0 NOT NULL,
	[17] [bit] DEFAULT 0 NOT NULL,
	[18] [bit] DEFAULT 0 NOT NULL,
	[19] [bit] DEFAULT 0 NOT NULL,
	[20] [bit] DEFAULT 0 NOT NULL,
	[21] [bit] DEFAULT 0 NOT NULL,
	[22] [bit] DEFAULT 0 NOT NULL,
	[23] [bit] DEFAULT 0 NOT NULL,
	[24] [bit] DEFAULT 0 NOT NULL,
	[25] [bit] DEFAULT 0 NOT NULL,
	[26] [bit] DEFAULT 0 NOT NULL,
	[27] [bit] DEFAULT 0 NOT NULL,
	[28] [bit] DEFAULT 0 NOT NULL,
	[29] [bit] DEFAULT 0 NOT NULL,
	[30] [bit] DEFAULT 0 NOT NULL,
	[31] [bit] DEFAULT 0 NOT NULL,
	[32] [bit] DEFAULT 0 NOT NULL,
	[33] [bit] DEFAULT 0 NOT NULL,
	[34] [bit] DEFAULT 0 NOT NULL,
	[35] [bit] DEFAULT 0 NOT NULL,
	[36] [bit] DEFAULT 0 NOT NULL,
	[37] [bit] DEFAULT 0 NOT NULL,
	[38] [bit] DEFAULT 0 NOT NULL,
	[39] [bit] DEFAULT 0 NOT NULL,
	[40] [bit] DEFAULT 0 NOT NULL,
	[41] [bit] DEFAULT 0 NOT NULL,
	[42] [bit] DEFAULT 0 NOT NULL,
	[43] [bit] DEFAULT 0 NOT NULL,
	[44] [bit] DEFAULT 0 NOT NULL,
	[45] [bit] DEFAULT 0 NOT NULL,
	[46] [bit] DEFAULT 0 NOT NULL,
	[47] [bit] DEFAULT 0 NOT NULL,
	[curr] [int] DEFAULT NULL,
	[lastseen] [datetime] NULL

PRIMARY KEY CLUSTERED 
(
	[badgeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

WHILE (SELECT COUNT([badgeid]) FROM [qcbadge]) < 300 
BEGIN  
INSERT INTO [dbo].[qcbadge]
           ([0])
     VALUES
           (0) 
END 