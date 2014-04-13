CREATE TABLE [dbo].[RouteImages] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ImageLocation] NVARCHAR (50)  NOT NULL,
    [RouteId]       INT            NOT NULL,
    [Longitude]     FLOAT (53)     NULL,
    [Latitude]      FLOAT (53)     NULL,
    [Name]          NVARCHAR (50)  NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_RouteImages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RouteImages_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id])
);

