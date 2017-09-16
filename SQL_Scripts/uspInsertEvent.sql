CREATE PROCEDURE [uspInsertEvent]
	@creatorName nvarchar(20),
	@title nvarchar(255),
	@description nvarchar(1500),
	@startdateandtime datetime,
	@enddateandtime datetime,
	@thumbnail nvarchar(255),
	@price decimal,
	@longitude float,
	@latitude float
AS
	INSERT INTO [Event] ([CreatorUserName], [Title], [Description], [StartDateAndTime], [EndDateAndTime], [Price], [Thumbnail], [Longitude], [Latitude])
				VALUES (@creatorName, @title, @description, @startdateandtime, @enddateandtime, @price, @thumbnail, @longitude, @latitude);