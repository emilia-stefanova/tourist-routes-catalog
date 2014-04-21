﻿/*
Deployment script for TouristRouteCatalog

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TouristRouteCatalog"
:setvar DefaultFilePrefix "TouristRouteCatalog"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[Campsites]...';


GO
CREATE TABLE [dbo].[Campsites] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [TypeId]      INT            NOT NULL,
    [Longitude]   FLOAT (53)     NOT NULL,
    [Latitude]    FLOAT (53)     NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Campsites] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[RouteDifficultyLevels]...';


GO
CREATE TABLE [dbo].[RouteDifficultyLevels] (
    [Difficulty] INT           NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_RouteDifficultyLevels] PRIMARY KEY CLUSTERED ([Difficulty] ASC)
);


GO
PRINT N'Creating [dbo].[RouteGeoPoints]...';


GO
CREATE TABLE [dbo].[RouteGeoPoints] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [RouteId]    INT        NOT NULL,
    [Longitude]  FLOAT (53) NOT NULL,
    [Latitude]   FLOAT (53) NOT NULL,
    [OrderIndex] INT        NOT NULL,
    CONSTRAINT [PK_RouteGeoPoints] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[RouteImages]...';


GO
CREATE TABLE [dbo].[RouteImages] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ImageLocation] NVARCHAR (50)  NOT NULL,
    [RouteId]       INT            NOT NULL,
    [Longitude]     FLOAT (53)     NULL,
    [Latitude]      FLOAT (53)     NULL,
    [Name]          NVARCHAR (50)  NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_RouteImages] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Routes]...';


GO
CREATE TABLE [dbo].[Routes] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [CreatorId]       INT            NOT NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [DifficultyLevel] INT            NOT NULL,
    [Duration]        BIGINT         NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [Seasons]         NVARCHAR (50)  NULL,
    [PublicTransport] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Routes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[RoutesCampsites]...';


GO
CREATE TABLE [dbo].[RoutesCampsites] (
    [RouteId]    INT NOT NULL,
    [CampsiteId] INT NOT NULL,
    CONSTRAINT [PK_RoutesCampsites] PRIMARY KEY CLUSTERED ([RouteId] ASC, [CampsiteId] ASC)
);


GO
PRINT N'Creating [dbo].[RoutesWaterSources]...';


GO
CREATE TABLE [dbo].[RoutesWaterSources] (
    [RouteId]       INT NOT NULL,
    [WaterSourceId] INT NOT NULL,
    CONSTRAINT [PK_RoutesWaterSources] PRIMARY KEY CLUSTERED ([RouteId] ASC, [WaterSourceId] ASC)
);


GO
PRINT N'Creating [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50)  NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [ImageLocation] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[WaterSources]...';


GO
CREATE TABLE [dbo].[WaterSources] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Longitude]   FLOAT (53)     NULL,
    [Latitude]    FLOAT (53)     NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_WaterSources] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating FK_RouteGeoPoints_Routes...';


GO
ALTER TABLE [dbo].[RouteGeoPoints]
    ADD CONSTRAINT [FK_RouteGeoPoints_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id]);


GO
PRINT N'Creating FK_RouteImages_Routes...';


GO
ALTER TABLE [dbo].[RouteImages]
    ADD CONSTRAINT [FK_RouteImages_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id]);


GO
PRINT N'Creating FK_Routes_RouteDifficultyLevels1...';


GO
ALTER TABLE [dbo].[Routes]
    ADD CONSTRAINT [FK_Routes_RouteDifficultyLevels1] FOREIGN KEY ([DifficultyLevel]) REFERENCES [dbo].[RouteDifficultyLevels] ([Difficulty]);


GO
PRINT N'Creating FK_Routes_Users...';


GO
ALTER TABLE [dbo].[Routes]
    ADD CONSTRAINT [FK_Routes_Users] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[Users] ([Id]);


GO
PRINT N'Creating FK_RoutesCampsites_Campsites...';


GO
ALTER TABLE [dbo].[RoutesCampsites]
    ADD CONSTRAINT [FK_RoutesCampsites_Campsites] FOREIGN KEY ([CampsiteId]) REFERENCES [dbo].[Campsites] ([Id]);


GO
PRINT N'Creating FK_RoutesCampsites_Routes...';


GO
ALTER TABLE [dbo].[RoutesCampsites]
    ADD CONSTRAINT [FK_RoutesCampsites_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id]);


GO
PRINT N'Creating FK_RoutesWaterSources_Routes...';


GO
ALTER TABLE [dbo].[RoutesWaterSources]
    ADD CONSTRAINT [FK_RoutesWaterSources_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id]);


GO
PRINT N'Creating FK_RoutesWaterSources_WaterSources...';


GO
ALTER TABLE [dbo].[RoutesWaterSources]
    ADD CONSTRAINT [FK_RoutesWaterSources_WaterSources] FOREIGN KEY ([WaterSourceId]) REFERENCES [dbo].[WaterSources] ([Id]);


GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
