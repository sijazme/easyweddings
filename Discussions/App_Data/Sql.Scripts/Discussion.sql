CREATE TABLE [dbo].[Discussion] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Date]     DATETIME      NOT NULL,
    [Location] VARCHAR (100) NOT NULL,
    [Subject]  VARCHAR (100) NOT NULL,
    [Outcome]  VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
