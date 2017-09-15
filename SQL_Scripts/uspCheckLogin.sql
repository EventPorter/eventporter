CREATE PROCEDURE [uspCheckLogin]
	@username nvarchar(20)
AS
	SELECT [User].[Password] as "Password", [User].[ID] as "UserId", [User].[UserType] as "UserType" FROM [User] WHERE [User].[UserName] = @username;