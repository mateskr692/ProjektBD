CREATE TABLE [dbo].[Objects]
(
	[object_no]		BIGINT			NOT NULL	IDENTITY (1,1),
	[name]			NVARCHAR(50)	NOT NULL,

	[client_id]		BIGINT			NOT NULL,
	[object_code]		CHAR(5)			NOT NULL,

	CONSTRAINT [PK_Objects]					PRIMARY KEY( [object_no] ),
	CONSTRAINT [FK_Objects_Clients]			FOREIGN KEY( [client_id] )		REFERENCES [Clients]( [client_id] ),
	CONSTRAINT [FK_Objects_ObjectTypes]		FOREIGN KEY( [object_code] )	REFERENCES [ObjectTypes]( [object_code] ),
)
