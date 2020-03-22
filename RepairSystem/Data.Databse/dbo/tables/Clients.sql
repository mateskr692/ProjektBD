CREATE TABLE [dbo].[Clients]
(
	[client_id]			BIGINT			NOT NULL	IDENTITY (1,1),
	[name]				NVARCHAR(50)	NOT NULL,
	[first_name]		NVARCHAR(50)	NOT NULL,
	[last_name]			NVARCHAR(50)	NOT NULL,
	[phone_no]			VARCHAR(20)		NULL

	CONSTRAINT [PK_Clients]		PRIMARY KEY( [client_id] )

)
