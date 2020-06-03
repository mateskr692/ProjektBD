CREATE TABLE [dbo].[Personel]
(
	[username]			NVARCHAR(20)	NOT NULL,
	[first_name]		NVARCHAR(50)	NOT NULL,
	[last_name]			NVARCHAR(50)	NOT NULL

	CONSTRAINT [PK_Personel]		PRIMARY KEY( [username] )
)
