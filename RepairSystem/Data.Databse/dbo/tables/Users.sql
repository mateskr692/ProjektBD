CREATE TABLE [dbo].[Users]
(
	[username]			NVARCHAR(20)			NOT NULL,
	[password]			BINARY(32)				NOT NULL,
	[password_salt]		BINARY(4)				NOT NULL,
	[role]				CHAR(3)					NOT NULL,
	[active]			CHAR(1)					NOT NULL,

	CHECK( [role] IN('WRK', 'MAN', 'ADM') ),
	CHECK( [active] IN('T', 'F') ),

	CONSTRAINT [PK_Users]			PRIMARY KEY( [username] ),
	CONSTRAINT [FK_Users_Personel]	FOREIGN KEY( [username] )	 REFERENCES [Personel]( [username] )

)
