
/****** Object:  Database UpperbayAgents    Script Date: 2/24/2008 PM ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = UpperbayAgents)
	DROP DATABASE [UpperbayAgents]
GO

CREATE DATABASE [UpperbayAgents]
 COLLATE SQL_Latin1_General_CP1_CI_AS
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
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateTime]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateTime]
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
	[AgentNickName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Celestial] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Collective] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Community] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Cluster] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Carrier] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Colony] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Service] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Name] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Description] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Datatype] [varchar] (32) NULL,
	[UpdateTime] datetime NULL,
	UNIQUE (AgentStorageKey,AgentNickName)
) ON [PRIMARY]
END

GO

ALTER TABLE [dbo].[Registry] WITH NOCHECK ADD 
	CONSTRAINT [PK_Registry_QualifiedAgentName] PRIMARY KEY  CLUSTERED 
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
	@agentNickName varchar(256),
	@celestial varchar(128),
	@collective varchar(128),
	@community varchar(128),
	@cluster varchar(256),
	@carrier varchar(256),
	@colony varchar(256),
	@service varchar(256),
	@name varchar(256),
	@description varchar(128),
	@datatype varchar(32),
	@updatetime DateTime
)
 AS
		delete from Registry where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName
		
		insert into Registry (AgentStorageKey, QualifiedAgentName, AgentNickName, Celestial, Collective, Community, Cluster, Carrier, Colony, Service, [Name], Description, Datatype, UpdateTime)
		values (@agentStorageKey, @qualifiedAgentName, @agentNickName, @celestial, @collective, @community, @cluster, @carrier, @colony, @service, @name, @description, @datatype, @updatetime) 

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
	@updateTime DateTime
)
 AS
		

		Update Registry Set Description = @description
		where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName

		Update Registry Set Datatype = @datatype 
		where AgentStorageKey = @agentStorageKey and QualifiedAgentName= @qualifiedAgentName

		Update Registry Set UpdateTime= @updateTime
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
		Celestial,
		Collective,
		Community,
		Cluster,
		Carrier,
		Colony,
		Service,
		[Name], 
		Description,
		Datatype,
		UpdateTime
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
/****** Object:  Stored Procedure dbo.UpdateTime    Script Date: 2/24/2008 PM ******/



CREATE PROCEDURE dbo.UpdateTime
	(
		@agentStorageKey int,
		@qualifiedAgentName varchar(256),
		@updateTime DateTime
	)
AS
	update Registry 
	set UpdateTime = @updateTime 
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
	[QualifiedPropertyStorageKey] [int] NOT NULL ,
	[LocalPropertyStorageKey] [int] NOT NULL ,
	[QualifiedAgentName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[QualifiedPropertyName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LocalPropertyName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PropertyName] [varchar] (48) NOT NULL,
	[Value] [varchar] (32) NULL,
	[Quality] [varchar] (32) NULL,
	[UpdateTime] datetime NULL
) ON [PRIMARY]
END

GO

ALTER TABLE [dbo].[CurrentValues] WITH NOCHECK ADD 
	CONSTRAINT [PK_CurrentValues] PRIMARY KEY  CLUSTERED 
	(
		[QualifiedPropertyStorageKey],
		[LocalPropertyStorageKey],
		[QualifiedAgentName],
		[QualifiedPropertyName],
		[LocalPropertyName],
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



SET QUOTED_IDENTIFIER ON 
SET ANSI_NULLS ON 
GO
/**********************************************************************************/
/****** Object:  Stored Procedure dbo.AddValue    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.AddValue
(
	@agentStorageKey int,
	@qualifiedPropertyStorageKey int,
	@localPropertyStorageKey int,
	@qualifiedAgentName varchar(256),
	@qualifiedPropertyName varchar(256),
	@localPropertyName varchar(256),
	@propertyName varchar(48),
	@updateTime DateTime
)
AS
	
		insert into CurrentValues (AgentStorageKey,QualifiedPropertyStorageKey, LocalPropertyStorageKey, QualifiedAgentName, QualifiedPropertyName, LocalPropertyName, PropertyName, UpdateTime)
		values (@agentStorageKey, @qualifiedPropertyStorageKey, @localPropertyStorageKey, @qualifiedAgentName, @qualifiedPropertyName, @localPropertyName, @propertyName, @updateTime) 

GO

SET QUOTED_IDENTIFIER ON 
SET ANSI_NULLS ON 
GO

/**********************************************************************************/
/****** Object:  Stored Procedure dbo.UpdateValue    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.UpdateValue
(
	@qualifiedPropertyStorageKey int,
	@qualifiedPropertyName varchar(256),
	@value varchar(32),
	@quality varchar(32),
	@updateTime DateTime
)
 AS
		
		Update CurrentValues Set Value = @value, Quality = @quality, UpdateTime= @updateTime
		where QualifiedPropertyStorageKey = @qualifiedPropertyStorageKey and QualifiedPropertyName= @qualifiedPropertyName
		
	

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
	@propertyName varchar(256),
	@itemValue varchar(32) OUTPUT,
	@itemQuality varchar(32) OUTPUT,
	@itemTime DateTime OUTPUT
)
 AS
	
	SET @itemValue = (select Value from CurrentValues where (QualifiedPropertyStorageKey = @propertyStorageKey AND QualifiedPropertyName = @propertyName) OR (LocalPropertyStorageKey = @propertyStorageKey AND LocalPropertyName = @propertyName))
	SET @itemQuality = (select Quality from CurrentValues where (QualifiedPropertyStorageKey = @propertyStorageKey AND QualifiedPropertyName = @propertyName) OR (LocalPropertyStorageKey = @propertyStorageKey AND LocalPropertyName = @propertyName))
	SET @itemTime = (select updateTime from CurrentValues where (QualifiedPropertyStorageKey = @propertyStorageKey AND QualifiedPropertyName = @propertyName) OR (LocalPropertyStorageKey = @propertyStorageKey AND LocalPropertyName = @propertyName))


GO




SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
/**********************************************************************************/
/****** Object:  Stored Procedure dbo.RemoveValue    Script Date: 2/24/2008 PM ******/


CREATE PROCEDURE dbo.RemoveValue
	(
		@qualifiedPropertyStorageKey int,
		@qualifiedPropertyName varchar(256)
	)
AS
	delete from CurrentValues
	where QualifiedPropertyStorageKey = @qualifiedPropertyStorageKey and QualifiedPropertyName= @qualifiedPropertyName
	
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
	[QualifiedPropertyStorageKey] [int] NOT NULL ,
	[QualifiedAgentName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[QualifiedPropertyName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PropertyName] [varchar] (48) NOT NULL,
	[Value] [varchar] (32) NULL,
	[Quality] [varchar] (32) NULL,
	[UpdateTime] datetime NULL,
	UNIQUE (QualifiedPropertyName,UpdateTime)
) ON [PRIMARY]
END

GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


/**********************************************************************************/
/****** Object:  Table [dbo].[AddHistory]    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.AddHistory
(
	@agentStorageKey int,
	@qualifiedPropertyStorageKey int,
	@qualifiedAgentName varchar(256),
	@qualifiedPropertyName varchar(256),
	@propertyName varchar(48),
	@value varchar(32),
	@quality varchar(32),
	@updateTime DateTime
)
 AS
	
	insert into History (AgentStorageKey, QualifiedPropertyStorageKey, QualifiedAgentName,  QualifiedPropertyName, PropertyName, Value, Quality, UpdateTime)
	values (@agentStorageKey, @qualifiedPropertyStorageKey, @qualifiedAgentName, @qualifiedPropertyName, @propertyName, @value, @quality, @updateTime) 

GO


SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




/**********************************************************************************/
/****** Object:  Table [dbo].[RemoveHistory]    Script Date: 2/24/2008 PM ******/
CREATE PROCEDURE dbo.RemoveHistsory
	(
		@qualifiedPropertyStorageKey int,
		@qualifiedPropertyName varchar(256)
	)
AS
	delete from History
	where QualifiedPropertyStorageKey = @qualifiedPropertyStorageKey and QualifiedPropertyName= @qualifiedPropertyName
	
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
CREATE DATABASE [UpperbayAgentsToo]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

use [UpperbayAgentsToo]
GO
/**********************************************************************************/
/* Replicated CurrentValue Table */
/**********************************************************************************/


/****** Object:  Table [dbo].[CurrentValues]    Script Date: 2/24/2008 PM ******/
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrentValues]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
 BEGIN
CREATE TABLE [dbo].[CurrentValues] (
	[AgentStorageKey] [int] NOT NULL ,
	[QualifiedPropertyStorageKey] [int] NOT NULL ,
	[LocalPropertyStorageKey] [int] NOT NULL ,
	[QualifiedAgentName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[QualifiedPropertyName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LocalPropertyName] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PropertyName] [varchar] (48) NOT NULL,
	[Value] [varchar] (32) NULL,
	[Quality] [varchar] (32) NULL,
	[UpdateTime] datetime NULL
) ON [PRIMARY]
END

GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO