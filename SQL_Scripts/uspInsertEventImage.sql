CREATE PROCEDURE [uspInsertEventImage]
	@event_id int,
	@filepath varchar(255)
AS
	INSERT INTO [EventImage] ([EventID], [FilePath]) 
	VALUES(@event_id, @filepath);