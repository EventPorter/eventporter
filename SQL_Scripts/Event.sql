CREATE TABLE [dbo].[Event] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [CreatorID]        INT             NOT NULL,
    [Title]            NVARCHAR (255)  NOT NULL,
    [Description]      NVARCHAR (1500) NOT NULL,
    [StartDateAndTime] DATETIME        NOT NULL,
    [EndDateAndTime]   DATETIME        NULL,
    [Thumbnail]        NVARCHAR (255)  NULL,
    [Price]            DECIMAL (18)    NULL,
    [Longitude]        FLOAT (53)      NULL,
    [Latitude]         FLOAT (53)      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Creator] FOREIGN KEY ([CreatorID]) REFERENCES [dbo].[User] ([ID])
);

