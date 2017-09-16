CREATE PROCEDURE [uspGetUserCreatedEvents]
	@username nvarchar(20)
AS
	SELECT * FROM [Event] WHERE [Event].[CreatorUserName] = @username;