CREATE TABLE [dbo].[RouteGeoPoints] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [RouteId]    INT        NOT NULL,
    [Longitude]  FLOAT (53) NOT NULL,
    [Latitude]   FLOAT (53) NOT NULL,
    [OrderIndex] INT        NOT NULL,
    CONSTRAINT [PK_RouteGeoPoints] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RouteGeoPoints_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id])
);

