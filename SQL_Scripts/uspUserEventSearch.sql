CREATE PROCEDURE [uspUserEventSearch]
	@searchstring NVARCHAR(30)
AS
	SELECT [Event].[Title], [Event].[Thumbnail], [Event].[Description], [Event].[ID] FROM [Event] WHERE [Event].[Title] LIKE '%@searchstring%';