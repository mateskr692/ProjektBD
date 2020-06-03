CREATE PROCEDURE [dbo].[AddUser]
	@fName NVARCHAR(50),
	@lName NVARCHAR(50),
	@username NVARCHAR(20),
	@password NVARCHAR(50),
	@role CHAR(3)
AS
	DECLARE @passwordSalt BINARY(4);
	DECLARE @hashedPassword BINARY(32);

	SET @passwordSalt = CRYPT_GEN_RANDOM(4);
	SET @hashedPassword = HASHBYTES('SHA2_256', CONCAT( CONVERT( VARBINARY(MAX), @password ), @passwordSalt) );

	INSERT INTO [Personel] (username, first_name, last_name)
		VALUES (@username, @fName, @lName);

	INSERT INTO [Users] ([username], [password], [password_salt], [role], [active]) 
		VALUES( @username, @hashedPassword, @passwordSalt, @role, 'T' );


RETURN 0
