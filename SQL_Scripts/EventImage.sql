CREATE TABLE [dbo].[EventImage] (
    [EventID]  INT           NOT NULL,
    [FilePath] VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_EventImage] PRIMARY KEY CLUSTERED ([EventID] ASC, [FilePath] ASC),
    CONSTRAINT [FK_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID])
);

