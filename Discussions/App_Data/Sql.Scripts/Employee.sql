IF OBJECT_ID('[dbo].[Employee]', 'U') IS NOT NULL
  DROP TABLE [dbo].[Employee]; 

CREATE TABLE [dbo].[Employee] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (100) NOT NULL,
    [LastName]  VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


