CREATE TABLE [dbo].[Activities]
(
	[activity_id]			BIGINT			NOT NULL	IDENTITY (1,1),
	[sequance_no]			INT				NULL,
	[decription]			NVARCHAR(2000)	NULL,
	[result]				NVARCHAR(2000)	NULL, 
	[status]				CHAR(3)			NOT NULL,
	[registration_date]		DATETIME		NOT NULL,
	[finish_cancel_date]	DATETIME		NULL,

	[activity_code]			CHAR(5)			NOT NULL,
	[request_id]			BIGINT			NOT NULL,
	[worker_id]				BIGINT			NOT NULL,

	CHECK( [status] IN('OPN', 'PRO', 'CAN', 'FIN') ),

	CONSTRAINT [PK_Activities]					PRIMARY KEY( [activity_id] ),
	CONSTRAINT [FK_Activities_ActivityTypes]	FOREIGN KEY( [activity_code] )	REFERENCES [ActivityTypes]( [activity_code] ),
	CONSTRAINT [FK_Activities_Requests]			FOREIGN KEY( [request_id] )		REFERENCES [Requests]( [request_id] ),
	CONSTRAINT [FK_Activities_Personel]			FOREIGN KEY( [worker_id] )		REFERENCES [Personel]( [personel_id] )

)
