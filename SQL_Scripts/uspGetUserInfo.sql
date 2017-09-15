CREATE PROCEDURE [uspGetUserInfo]
	@username nvarchar(20)
AS
	SELECT [User].[ID], [User].[FirstName], [User].[LastName], [User].[Email], [User].[DOB], [User].[RegistrationDate] as "RegDate", [User].[UserType], [User].[Location] FROM [User] 
	WHERE [User].[UserName] = @username;