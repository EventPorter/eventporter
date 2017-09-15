CREATE PROCEDURE uspInsertUser
	@firstname nvarchar(55),
	@lastname nvarchar(55),
	@username nvarchar(20),
	@email nvarchar(55),
	@dob datetime,
	@regDate datetime,
	@userType int,
	@pass nvarchar(255)
AS
	INSERT INTO [User]([FirstName], [LastName], [UserName], [Email], [DOB], [RegistrationDate], [UserType], [Password])
	VALUES(@firstname, @lastname, @username, @email, @dob, @regDate, @userType, @pass);