CREATE TABLE [dbo].[User] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (55)  NOT NULL,
    [LastName]         NVARCHAR (55)  NOT NULL,
    [DOB]              DATETIME       NOT NULL,
    [Email]            NVARCHAR (55)  NOT NULL,
    [UserName]         NVARCHAR (20)  NOT NULL,
    [Password]         NVARCHAR (255) NOT NULL,
    [UserType]         INT            NOT NULL,
    [RegistrationDate] DATETIME       NOT NULL,
    [Location]         NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

