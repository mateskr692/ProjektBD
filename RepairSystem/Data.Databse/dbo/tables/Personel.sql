CREATE TABLE [dbo].[Personel]
(
	[personel_id]			BIGINT			NOT NULL	IDENTITY (1,1),
	[first_name]			NVARCHAR(50)	NOT NULL,
	[last_name]				NVARCHAR(50)	NOT NULL

	CONSTRAINT [PK_Personel]		PRIMARY KEY( [personel_id] )
)
