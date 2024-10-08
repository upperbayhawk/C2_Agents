
/****** Object:  Database UpperbayAgents    Script Date: 2/24/2008 PM ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'UpperbayAgents')
	DROP DATABASE [UpperbayAgents]
GO

CREATE DATABASE [UpperbayAgents]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'UpperbayAgents', N'autoclose', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'bulkcopy', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'trunc. log', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'torn page detection', N'true'
GO

exec sp_dboption N'UpperbayAgents', N'read only', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'dbo use', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'single', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'autoshrink', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'ANSI null default', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'recursive triggers', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'ANSI nulls', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'concat null yields null', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'cursor close on commit', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'default to local cursor', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'quoted identifier', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'ANSI warnings', N'false'
GO

exec sp_dboption N'UpperbayAgents', N'auto create statistics', N'true'
GO

exec sp_dboption N'UpperbayAgents', N'auto update statistics', N'true'
GO

use [UpperbayAgents]
GO

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Registry */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/****** Object:  Stored Procedure dbo.AddAgent    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddAgent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddAgent]
GO

/****** Object:  Stored Procedure dbo.UpdateAgent    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateAgent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateAgent]
GO

/****** Object:  Stored Procedure dbo.FlushAgents    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FlushAgents]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FlushAgents]
GO

/****** Object:  Stored Procedure dbo.GetAgentCount    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetAgentCount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetAgentCount]
GO

/****** Object:  Stored Procedure dbo.LoadAgents    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LoadAgents]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[LoadAgents]
GO

/****** Object:  Stored Procedure dbo.RemoveAgent    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RemoveAgent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RemoveAgent]
GO

/****** Object:  Stored Procedure dbo.UpdateLastUpdateTime    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateLastUpdateTime]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateLastUpdateTime]
GO

/****** Object:  Table [dbo].[Registry]    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Registry]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Registry]
GO

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Current Values */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/****** Object:  Stored Procedure dbo.AddValue    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddValue]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddValue]
GO

/****** Object:  Stored Procedure dbo.UpdateAgent    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateAgent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateAgent]
GO

/****** Object:  Stored Procedure dbo.GetValue    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetValue]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetValue]
GO

/****** Object:  Stored Procedure dbo.RemoveValue    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RemoveValue]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RemoveValue]
GO

/****** Object:  Table [dbo].[CurrentValues]    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrentValues]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CurrentValues]
GO

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* History */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

/****** Object:  Table [dbo].[History]    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[History]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[History]
GO
/****** Object:  Stored Procedure dbo.AddHistory    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddHistory]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddHistory]
GO

/****** Object:  Stored Procedure dbo.RemoveHistory    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RemoveHistory]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RemoveHistory]
GO

/****** Object:  Stored Procedure dbo.FlushHistory    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FlushHistory]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FlushHistory]
GO

/****** Object:  Table [dbo].[History]    Script Date: 2/24/2008 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[History]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[History]
GO

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

/**********************************************************************************/
/* Registry */
/**********************************************************************************/
/****** Object:  Table [dbo].[Registry]    Script Date: 2/24/2008 PM ******/
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Registry]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
 BEGIN
CREATE TABLE [dbo].[Registry] (
	[AgentStorageKey] [int] NOT NULL ,
	[QualifiedAgentName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Collective] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Carrier] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Colony] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Cell] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Name] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Description] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Datatype] [varchar] (32) NULL,
	[LastUpdatedTime] datetime NULL
) ON [PRIMARY]
END

GO

ALTER TABLE [dbo].[Registry] WITH NOCHECK ADD 
	CONSTRAINT [PK_Registry] PRIMARY KEY  CLUSTERED 
	(
		[AgentStorageKey],
		[QualifiedAgentName]
	)  ON [PRIMARY] 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



/**********************************************************************************/
/****** Object:  Stored Procedure dbo.AddAgent    Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.AddAgent
(
	@agentStorageKey int,
	@qualifiedAgentName varchar(256),
	@collective varchar(128),
	@carrier varchar(256),
	@colony varchar(256),
	@cell varchar(256),
	@name varchar(256),
	@description varchar(128),
	@datatype varchar(32),
	@lastUpdatedTime DateTime
)
 AS
		delete from Registry where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName
		
		insert into Registry (AgentStorageKey, QualifiedAgentName, Collective, Carrier, Colony, Cell, [Name], Description, Datatype, LastUpdatedTime)
		values (@agentStorageKey, @qualifiedAgentName, @collective, @carrier, @colony, @cell, @name, @description, @datatype, @lastUpdatedTime) 

GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



/**********************************************************************************/
/****** Object:  Stored Procedure dbo.UpdateItem    Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.UpdateAgent
(
	@agentStorageKey int,
	@qualifiedAgentName varchar(256),
	@description varchar(128),
	@datatype varchar(32),
	@lastUpdatedTime DateTime
)
 AS
		

		Update Registry Set Description = @description
		where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName

		Update Registry Set Datatype = @datatype 
		where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName

		Update Registry Set LastUpdatedTime= @lastUpdatedTime
		where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName


GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/**********************************************************************************/
/****** Object:  Stored Procedure dbo.FlushAgents    Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.FlushAgents

AS
	SET NOCOUNT ON

	DELETE [dbo].[Registry]
	DELETE [dbo].[CurrentValues]
 

GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Stored Procedure dbo.GetAgentCount Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.GetAgentCount

 AS 
	SET NOCOUNT ON

	SELECT COUNT(AgentStorageKey) 
	  FROM [dbo].[Registry]
	 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Stored Procedure dbo.LoadAgents    Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.LoadAgents

AS
	select 
		QualifiedAgentName,
		Collective,
		Carrier,
		Colony,
		Cell,
		[Name], 
		Description,
		Datatype,
		LastUpdatedTime
	from Registry
	
	SET NOCOUNT ON
	RETURN 

GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Stored Procedure dbo.RemoveAgent    Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.RemoveAgent
	(
		@agentStorageKey int,
		@qualifiedAgentName varchar(256)
	)
AS
	delete from Registry 
	where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName
	
	SET NOCOUNT ON 
	RETURN 

GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Stored Procedure dbo.UpdateLastUpdateTime    Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.UpdateLastUpdatedTime
	(
		@agentStorageKey int,
		@qualifiedAgentName varchar(256),
		@lastUpdatedTime DateTime
	)
AS
	update Registry 
	set LastUpdatedTime = @lastUpdatedTime 
	where [AgentStorageKey] = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName
	
	SET NOCOUNT ON
	RETURN 


GO

/**********************************************************************************/
/* CurrentValues */
/**********************************************************************************/


/****** Object:  Table [dbo].[CurrentValues]    Script Date: 2/24/2008 PM ******/
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrentValues]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
 BEGIN
CREATE TABLE [dbo].[CurrentValues] (
	[AgentStorageKey] [int] NOT NULL ,
	[PropertyStorageKey] [int] NOT NULL ,
	[QualifiedAgentName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[QualifiedPropertyName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PropertyName] [varchar] (48) NOT NULL,
	[Value] [varchar] (32) NULL,
	[Quality] [varchar] (32) NULL,
	[LastUpdatedTime] datetime NULL
) ON [PRIMARY]
END

GO

ALTER TABLE [dbo].[CurrentValues] WITH NOCHECK ADD 
	CONSTRAINT [PK_CurrentValues] PRIMARY KEY  CLUSTERED 
	(
		[PropertyStorageKey],
		[QualifiedAgentName],
		[QualifiedPropertyName],
		[PropertyName]
	)  ON [PRIMARY]
GO



ALTER TABLE [dbo].[CurrentValues]
	ADD CONSTRAINT [FK_CurrentValues] FOREIGN KEY 
	(
		[AgentStorageKey],
		[QualifiedAgentName]
	) REFERENCES [dbo].[Registry] ([AgentStorageKey],[QualifiedAgentName])

GO

ALTER TABLE [dbo].[CurrentValues] NOCHECK CONSTRAINT FK_CurrentValues

GO


SET QUOTED_IDENTIFIER ON 
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Stored Procedure dbo.AddValue    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.AddValue
(
	@agentStorageKey int,
	@propertyStorageKey int,
	@qualifiedAgentName varchar(256),
	@qualifiedPropertyName varchar(256),
	@propertyName varchar(48),
	@lastUpdatedTime DateTime
)
AS
	
		insert into CurrentValues (AgentStorageKey,PropertyStorageKey, QualifiedAgentName, QualifiedPropertyName, PropertyName, LastUpdatedTime)
		values (@agentStorageKey, @propertyStorageKey, @qualifiedAgentName, @qualifiedPropertyName, @propertyName, @lastUpdatedTime) 

GO

SET QUOTED_IDENTIFIER ON 
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Stored Procedure dbo.UpdateValue    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.UpdateValue
(
	@propertyStorageKey int,
	@qualifiedPropertyName varchar(256),
	@value varchar(32),
	@quality varchar(32),
	@lastUpdatedTime DateTime
)
 AS
		
		Update CurrentValues Set Value = @value, Quality = @quality, LastUpdatedTime= @lastUpdatedTime
		where PropertyStorageKey = @propertyStorageKey and QualifiedPropertyName= @qualifiedPropertyName



GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


/**********************************************************************************/
/****** Object:  Stored Procedure dbo.GetValue    Script Date: 2/24/2008 PM ******/

CREATE PROCEDURE dbo.GetValue
(
	@propertyStorageKey int,
	@qualifiedPropertyName varchar(256),
	@itemValue varchar(32) OUTPUT
)
 AS
	
	SET @itemValue = (select Value from CurrentValues where PropertyStorageKey = @propertyStorageKey AND QualifiedPropertyName = @qualifiedPropertyName)


GO




SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
/**********************************************************************************/
/****** Object:  Stored Procedure dbo.RemoveValue    Script Date: 2/24/2008 PM ******/


CREATE PROCEDURE dbo.RemoveValue
	(
		@propertyStorageKey int,
		@qualifiedPropertyName varchar(256)
	)
AS
	delete from CurrentValues
	where PropertyStorageKey = @propertyStorageKey and QualifiedPropertyName= @qualifiedPropertyName
	
	SET NOCOUNT ON 
	RETURN 


GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/* History */
/**********************************************************************************/



/**********************************************************************************/
/****** Object:  Table [dbo].[History]    Script Date: 2/24/2008 PM ******/
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[History]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[History] (
	[AgentStorageKey] [int] NOT NULL ,
	[PropertyStorageKey] [int] NOT NULL ,
	[QualifiedAgentName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[QualifiedPropertyName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PropertyName] [varchar] (48) NOT NULL,
	[Value] [varchar] (32) NULL,
	[Quality] [varchar] (32) NULL,
	[LastUpdatedTime] datetime NULL
) ON [PRIMARY]
END

GO



/*
ALTER TABLE [dbo].[History] WITH NOCHECK ADD 
	CONSTRAINT [PK_History] PRIMARY KEY  CLUSTERED 
	(
		[PropertyStorageKey],
		[QualifiedPropertyName],
		[PropertyName]
	)  ON [PRIMARY]
GO


ALTER TABLE [dbo].[History]
	ADD CONSTRAINT [FK_History] FOREIGN KEY 
	(
		[AgentStorageKey],
		[QualifiedAgentName]
	) REFERENCES [dbo].[Registry] ([AgentStorageKey],[QualifiedAgentName])

GO
ALTER TABLE [dbo].[History] NOCHECK CONSTRAINT FK_History

GO
*/


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


/**********************************************************************************/
/****** Object:  Table [dbo].[AddHistory]    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.AddHistory
(
	@agentStorageKey int,
	@propertyStorageKey int,
	@qualifiedAgentName varchar(256),
	@qualifiedPropertyName varchar(256),
	@propertyName varchar(48),
	@value varchar(32),
	@quality varchar(32),
	@lastUpdatedTime DateTime
)
 AS
	
	insert into History (AgentStorageKey, PropertyStorageKey, QualifiedAgentName,  QualifiedPropertyName, PropertyName, Value, Quality, LastUpdatedTime)
	values (@agentStorageKey, @propertyStorageKey, @qualifiedAgentName, @qualifiedPropertyName, @propertyName, @value, @quality, @lastUpdatedTime) 

GO


SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




/**********************************************************************************/
/****** Object:  Table [dbo].[RemoveHistory]    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.RemoveHistsory
	(
		@propertyStorageKey int,
		@qualifiedPropertyName varchar(256)
	)
AS
	delete from History
	where PropertyStorageKey = @propertyStorageKey and QualifiedPropertyName= @qualifiedPropertyName
	
	SET NOCOUNT ON 
	RETURN 


GO


SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Table [dbo].[FlushHistory]    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.FlushHistory

AS
	SET NOCOUNT ON

	DELETE [dbo].[History]
	 

GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



/**********************************************************************************/
/********************************************************************************/