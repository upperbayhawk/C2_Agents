/****** Object:  Database UpperbayAgents    Script Date: 8/25/2004 3:28:27 PM ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'UpperbayAgents')
	DROP DATABASE [UpperbayAgents]
GO
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'UpperbayAgentsToo')
	DROP DATABASE [UpperbayAgentsToo]
GO
