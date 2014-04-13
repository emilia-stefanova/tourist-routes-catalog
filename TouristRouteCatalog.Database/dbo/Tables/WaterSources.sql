CREATE TABLE [dbo].[WaterSources] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Longitude]   FLOAT (53)     NULL,
    [Latitude]    FLOAT (53)     NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_WaterSources] PRIMARY KEY CLUSTERED ([Id] ASC)
);

