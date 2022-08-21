RESTORE DATABASE [Eres] 
FROM DISK = '/tmp/Eres.bak'
WITH FILE = 1,
MOVE 'Eres_Data' TO '/var/opt/mssql/data/Eres.mdf',
MOVE 'Eres_Log' TO '/var/opt/mssql/data/Eres.ldf',
NOUNLOAD, REPLACE, STATS = 5
GO

USE Eres
GO

DBCC SHRINKFILE (Eres_Data, 1)
GO

ALTER DATABASE Eres
SET RECOVERY SIMPLE;  
GO  

DBCC SHRINKFILE (Eres_Log,1)
GO

ALTER DATABASE Eres
SET RECOVERY FULL;  
GO  

DBCC SHRINKDATABASE ([Eres])
GO