CREATE PROCEDURE [uspGetUsersCreatedEvents]
	@id int
AS
	SELECT * FROM [Event] WHERE [Event].[CreatorID] = @id;