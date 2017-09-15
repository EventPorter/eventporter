CREATE PROCEDURE [uspGetEvent]
	@eventid int
AS
	SELECT * FROM [Event] WHERE [Event].[ID] = @eventid;
