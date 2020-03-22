CREATE TABLE [dbo].[ActivityTypes]
(
	[activity_code]		CHAR(5)			NOT NULL,
	[activity_name]		NVARCHAR(50)	NOT NULL

	CONSTRAINT [PK_ActivityTypes]		PRIMARY KEY( [activity_code] )
)
