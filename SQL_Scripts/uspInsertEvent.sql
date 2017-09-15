CREATE PROCEDURE [uspInsertEvent]
	@creator int,
	@title nvarchar(255),
	@description nvarchar(1500),
	@startdateandtime datetime,
	@enddateandtime datetime,
	@thumbnail nvarchar(255),
	@price decimal,
	@longitude float,
	@latitude float
AS
	INSERT INTO [Event] ([CreatorID], [Title], [Description], [StartDateAndTime], [EndDateAndTime], [Price], [Thumbnail], [Longitude], [Latitude])
				VALUES (@creator, @title, @description, @startdateandtime, @enddateandtime, @price, @thumbnail, @longitude, @latitude);