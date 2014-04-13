CREATE TABLE [dbo].[Routes] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [CreatorId]       INT            NOT NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [DifficultyLevel] INT            NOT NULL,
    [Duration]        BIGINT         NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [Seasons]         NVARCHAR (50)  NULL,
    [PublicTransport] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Routes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Routes_RouteDifficultyLevels1] FOREIGN KEY ([DifficultyLevel]) REFERENCES [dbo].[RouteDifficultyLevels] ([Difficulty]),
    CONSTRAINT [FK_Routes_Users] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[Users] ([Id])
);

