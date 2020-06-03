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
EXEC AddUser @fName = N'hgfds',		@lName = N'jkhjgfaed',	@username = N'Admin',	@password = N'Admin',   @role = 'ADM';
EXEC AddUser @fName = N'hgfytgrf',	@lName = N'bhjgyuefs',	@username = N'Worker1',	@password = N'Worker1', @role = 'WRK';
EXEC AddUser @fName = N'bgfcdre',	@lName = N'zzzzzzz',	@username = N'Worker2', @password = N'Worker2', @role = 'WRK';
EXEC AddUser @fName = N'poiuyt',	@lName = N'adwawrt',	@username = N'Worker3', @password = N'Worker3', @role = 'WRK';
EXEC AddUser @fName = N'jguytrgr',	@lName = N'vcxdseq',	@username = N'Manager', @password = N'Manager', @role = 'MAN';

INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('NAPRW', N'Naprawa przedmiou');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('WYMIN', N'Wymiana przedmiou');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('REKLM', N'Zlozenie reklamacji');
INSERT INTO [ActivityTypes] (activity_code, activity_name) VALUES ('ZWROT', N'Zwrot na podstawie gwarancji');

INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('SAMOS', N'Samochod osobowy');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('SAMCZ', N'Samochod ciezarowy');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('MOTCK', N'motocykl');
INSERT INTO [ObjectTypes] ([object_code], [object_name]) VALUES ('ROWER', N'rower');