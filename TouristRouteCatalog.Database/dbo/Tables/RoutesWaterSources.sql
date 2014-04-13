CREATE TABLE [dbo].[RoutesWaterSources] (
    [RouteId]       INT NOT NULL,
    [WaterSourceId] INT NOT NULL,
    CONSTRAINT [PK_RoutesWaterSources] PRIMARY KEY CLUSTERED ([RouteId] ASC, [WaterSourceId] ASC),
    CONSTRAINT [FK_RoutesWaterSources_Routes] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([Id]),
    CONSTRAINT [FK_RoutesWaterSources_WaterSources] FOREIGN KEY ([WaterSourceId]) REFERENCES [dbo].[WaterSources] ([Id])
);

