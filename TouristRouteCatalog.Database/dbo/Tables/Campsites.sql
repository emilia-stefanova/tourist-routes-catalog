CREATE TABLE [dbo].[Campsites] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [TypeId]      INT            NOT NULL,
    [Longitude]   FLOAT (53)     NOT NULL,
    [Latitude]    FLOAT (53)     NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Campsites] PRIMARY KEY CLUSTERED ([Id] ASC)
);

