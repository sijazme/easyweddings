IF OBJECT_ID('[dbo].[EmployeeDiscussion]', 'U') IS NOT NULL
  DROP TABLE [dbo].[EmployeeDiscussion]

GO

CREATE TABLE [dbo].[EmployeeDiscussion] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
	[IsObserver] BIT NOT NULL,
	[EmployeeId] INT NOT NULL REFERENCES Employee(Id),
	[DiscussionId] INT NOT NULL REFERENCES Discussion(Id),
    PRIMARY KEY CLUSTERED ([Id] ASC)
	)

GO

