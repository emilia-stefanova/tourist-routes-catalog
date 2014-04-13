CREATE TABLE [dbo].[RoutesCampsites] (
    [RouteId]    INT NOT NULL,
    [CampsiteId] INT NOT NULL,
    CONSTRAINT [PK_RoutesCampsites] PRIMARY KEY CLUSTERED ([RouteId] ASC, [CampsiteId] ASC),
    CONSTRAINT [FK_RoutesCampsites_Campsites] FOREIGN KEY ([CampsiteId]) REFERENCES [dbo].[Campsites] ([Id]),
    CONSTRAINT [FK_RoutesCampsites_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id])
);

