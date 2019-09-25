

-- DB 복원 
RESTORE Database SecureDB From disk='D:\SecureDB.Bak' WITH  Replace,
	Move 'SecureDB' To 'H:\DB\Data\SecureDB.Mdf',
	Move 'SecureDB_log' To 'I:\DB\log\SecureDB_log.ldf'


	
-- CLR 적용 
sp_configure
sp_configure 'show advanced options', 1;

sp_configure 'clr enabled'
sp_configure 'clr enabled', 1
RECONFIGURE


-- 외부데이터 접근 
ALTER DATABASE SecureDB SET TRUSTWORTHY ON;


--소유권도 변경 해야 함
use SecureDB
EXEC sp_changedbowner 'sa'


select SecureDB.dbo.xfn_IntCrypt_SHA512_Encoding_UTF8('AAA')
--8d708d18b54df3962d696f069ad42dad7762b5d4d3c97ee5fa2dae0673ed46545164c078b8db3d59c4b96020e4316f17bb3d91bf1f6bc0896bbe75416eb8c385
select securedb.dbo.xfn_IntCrypt_encrypt('http://key-server/key/ent.key', '_1', 'AAA')
select securedb.dbo.xfn_IntCrypt_encrypt('ent', '_1', 'AAA')
--PyCfILMLOTPwz/tJ7qVG6g==