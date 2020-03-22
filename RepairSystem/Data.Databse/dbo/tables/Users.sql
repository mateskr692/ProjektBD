CREATE TABLE [dbo].[Users]
(
	[personel_id]		BIGINT					NOT NULL,
	[username]			NVARCHAR(50)			NOT NULL,
	[password]			BINARY(32)				NOT NULL,
	[password_salt]		BINARY(4)				NOT NULL,
	[role]				CHAR(3)					NOT NULL,
	[active]			CHAR(1)					NOT NULL,

	CHECK( [role] IN('WRK', 'MAN', 'ADM') ),
	CHECK( [active] IN('T', 'F') ),

	CONSTRAINT [PK_Users]			PRIMARY KEY( [personel_id] ),
	CONSTRAINT [FK_Users_Personel]	FOREIGN KEY( [personel_id] )	 REFERENCES [Personel]( [personel_id] )

)
