CREATE TABLE [dbo].[ObjectTypes]
(
	[object_code]		CHAR(5)			NOT NULL,
	[object_name]		NVARCHAR(50)	NOT NULL

	CONSTRAINT [PK_ObjectTypes]		PRIMARY KEY( [object_code] )
)
