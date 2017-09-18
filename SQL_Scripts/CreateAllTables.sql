CREATE TABLE [dbo].[Event] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [CreatorUserName]  NVARCHAR (20)   NOT NULL,
    [Title]            NVARCHAR (255)  NOT NULL,
    [Description]      NVARCHAR (1500) NOT NULL,
    [StartDateAndTime] DATETIME        NOT NULL,
    [EndDateAndTime]   DATETIME        NULL,
    [Thumbnail]        NVARCHAR (255)  NULL,
    [Price]            DECIMAL (18)    NULL,
    [Longitude]        FLOAT (53)      NULL,
    [Latitude]         FLOAT (53)      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [dbo].[User] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (55)  NOT NULL,
    [LastName]         NVARCHAR (55)  NOT NULL,
    [DOB]              DATETIME       NOT NULL,
    [Email]            NVARCHAR (55)  NOT NULL,
    [UserName]         NVARCHAR (20)  NOT NULL UNIQUE,
    [Password]         NVARCHAR (255) NOT NULL,
    [UserType]         INT            NOT NULL,
    [RegistrationDate] DATETIME       NOT NULL,
    [Location]         NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [dbo].[EventImage] (
    [EventID]  INT           NOT NULL,
    [FilePath] VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_EventImage] PRIMARY KEY CLUSTERED ([EventID] ASC, [FilePath] ASC),
    CONSTRAINT [FK_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID])
);




