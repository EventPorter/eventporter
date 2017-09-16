CREATE PROCEDURE [uspGetEventGallery]
	@eventID int
AS
	SELECT [EventImage].[FilePath] FROM [EventImage] WHERE [EventImage].[EventID] = @eventID;