CREATE TABLE [dbo].[RouteDifficultyLevels] (
    [Difficulty] INT           NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_RouteDifficultyLevels] PRIMARY KEY CLUSTERED ([Difficulty] ASC)
);

