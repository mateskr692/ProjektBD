CREATE TABLE [dbo].[Requests]
(
	[request_id]			BIGINT			NOT NULL	IDENTITY (1,1),
	[decription]			NVARCHAR(2000)	NULL,
	[result]				NVARCHAR(2000)	NULL, 
	[status]				CHAR(3)			NOT NULL,
	[registration_date]		DATETIME		NOT NULL,
	[finish_cancel_date]	DATETIME		NULL,

	[object_no]				BIGINT			NOT NULL,
	[manager_id]			BIGINT			NOT NULL,

	CHECK( [status] IN('OPN', 'PRO', 'CAN', 'FIN') ),
	 
	CONSTRAINT [PK_Requests]			PRIMARY KEY( [request_id] ),
	CONSTRAINT [FK_Requests_Objects]	FOREIGN KEY( [object_no] )		REFERENCES [Objects]( [object_no] ),
	CONSTRAINT [FK_Requests_Personel]	FOREIGN KEY( [manager_id] )		REFERENCES [Personel]( [personel_id] )


)
