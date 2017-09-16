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

