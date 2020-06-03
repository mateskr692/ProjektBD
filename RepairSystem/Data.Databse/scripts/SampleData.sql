/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


--Admin user
EXEC AddUser @fName = N'Tony',		@lName = N'Odell',		@username = N'Admin1',	@password = N'Admin',   @role = 'ADM';
EXEC AddUser @fName = N'Marie',		@lName = N'Thompson',	@username = N'Admin2',	@password = N'Admin',   @role = 'ADM';
EXEC AddUser @fName = N'Lois',		@lName = N'Rose',		@username = N'Admin3',	@password = N'Admin',   @role = 'ADM';
EXEC AddUser @fName = N'Timothy',	@lName = N'Chabot',		@username = N'Admin4',	@password = N'Admin',   @role = 'ADM';

EXEC AddUser @fName = N'Charlotte',	@lName = N'Norris',		@username = N'Worker1',	@password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Joshua',	@lName = N'Depasquale',	@username = N'Worker2', @password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Darrin',	@lName = N'Smith',		@username = N'Worker3', @password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Alicia',	@lName = N'Balas',		@username = N'Worker4', @password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Faye',		@lName = N'Howells',	@username = N'Worker5', @password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Dewitt',	@lName = N'Stephenson',	@username = N'Worker6', @password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Eric',		@lName = N'Smith',		@username = N'Worker7', @password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Natasha',	@lName = N'Bonnett',	@username = N'Worker8', @password = N'Worker', @role = 'WRK';
EXEC AddUser @fName = N'Karen',		@lName = N'Packard',	@username = N'Worker9', @password = N'Worker', @role = 'WRK';

EXEC AddUser @fName = N'John',		@lName = N'Burdette',	@username = N'Manager1', @password = N'Manager', @role = 'MAN';
EXEC AddUser @fName = N'Fernanda',	@lName = N'Nelson',		@username = N'Manager2', @password = N'Manager', @role = 'MAN';
EXEC AddUser @fName = N'John',		@lName = N'Gorman',		@username = N'Manager3', @password = N'Manager', @role = 'MAN';
EXEC AddUser @fName = N'Andrew',	@lName = N'Nowak',		@username = N'Manager4', @password = N'Manager', @role = 'MAN';
EXEC AddUser @fName = N'Cris',		@lName = N'Altman',		@username = N'Manager5', @password = N'Manager', @role = 'MAN';

INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('NAPRW', N'Naprawa przedmiou');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('WYMIN', N'Wymiana przedmiou');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('REKLM', N'Zlozenie reklamacji');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('ZWROT', N'Zwrot na podstawie gwarancji');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('PRZEG', N'Przeglad techiniczny');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('KONTR', N'Kontrola jakosci');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('GWARN', N'Nowy przedmiot na podstawie gwarancji');



INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('ZEGME', N'Zegarek mechaniczy');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('ZEGEL', N'Zegarek elektroniczny');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('ZEGSC', N'Zegarek scienny');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('KLAWI', N'Klawiatura');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('MSZKA', N'Myszka do komputera');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('SLUCH', N'Sluchawki');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('SLUBP', N'Sluchawki bezprzewodowe');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('ADPTR', N'Adapter');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('SAMOS', N'Samochod osobowy');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('SAMCZ', N'Samoczhod ciezarowy');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('MOTCK', N'MOtocykl');



INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Melinda',	N'Shipton',		N'Waves Music',		'914-513-0309');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Angela',	N'Nagy',		N'Gamma Grays',		'325-200-0043');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Arthur',	N'Ryan',		N'ArthurGRyan',		'314-482-0855');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Toby',		N'Hilliard',	N'TobyNHilliard',	'508-314-5648');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Donald',	N'Ewen',		N'Techo Solutions',	'708-596-0496');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Refugio',	N'Turner',		N'Soned2000',		'212-942-7472');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Emmett',	N'Walker',		N'Liet1974',		'972-562-8380');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Mary',		N'Costello',	N'MaryACostello',	'408-401-8310');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Herman',	N'Garcia',		N'Reliable Garden',	'801-208-3868');
INSERT INTO [Clients] ([first_name], [last_name], [name], [phone_no]) VALUES (N'Linda',		N'McClain',		N'County Market',	'845-468-8872');



INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (1,	N'Volkswagen CC',		'SAMOS');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (1,	N'hand fan',			'SLUCH');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (1,	N'tea pot',				'KLAWI');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (2,	N'laser pointer',		'MOTCK');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (3,	N'lemon',				'SAMCZ');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (3,	N'dog',					'ADPTR');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (4,	N'blowdryer',			'SLUBP');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (4,	N'frying pan',			'SLUCH');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (4,	N'package of glitter',	'MSZKA');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (4,	N'book',				'KLAWI');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (4,	N'safety pin',			'ZEGSC');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (5,	N'tennis ball',			'ZEGEL');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (6,	N'egg beater',			'ZEGME');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (6,	N'lamp shade',			'ADPTR');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (7,	N'candy cane',			'SAMCZ');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (8,	N'jar of pickles',		'MOTCK');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (9,	N'toy soldier',			'ZEGSC');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (9,	N'street lights',		'ZEGME');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (9,	N'monitor',				'SLUBP');

INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (10,	N'plush rabbit',		'SLUCH');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (10,	N'trash bag',			'ZEGME');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (10,	N'fishing hook',		'ADPTR');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (10,	N'chain',				'KLAWI');
INSERT INTO [Objects] ([client_id], [name], [object_code]) VALUES (10,	N'bottle of soda',		'MOTCK');

