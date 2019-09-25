
--exec sp_addextendedproc 'xp_IntCrypt_Encrypt', 'IntCryptProc.dll'
--exec sp_addextendedproc 'xp_IntCrypt_Decrypt', 'IntCryptProc.dll'
--exec sp_addextendedproc 'xp_IntCrypt_Base64_Encoding', 'IntCryptProc.dll'
--exec sp_addextendedproc 'xp_IntCrypt_Base64_Decoding', 'IntCryptProc.dll'
--exec sp_addextendedproc 'xp_IntCrypt_MD5_Encoding', 'IntCryptProc.dll'
--exec sp_addextendedproc 'xp_IntCrypt_SHA1_Encoding', 'IntCryptProc.dll'


declare 
	@s varchar(1000),
	@r varchar(1000)

Set @s = '김병규abc'

exec master..xp_IntCrypt_Encrypt 'http://211.233.74.203/Key/interpark.key', '_0', @s, @r output
select  'aes 암호', @s, @r

Set @s = @r

exec master..xp_IntCrypt_Decrypt 'http://211.233.74.203/Key/interpark.key', '_0', @s, @r output
select  'aes 복호', @s, @r

Set @s = @r

exec master..xp_IntCrypt_Base64_Encoding @s, @r output
select  'base64 인코딩', @s, @r

Set @s = @r
exec master..xp_IntCrypt_Base64_Decoding @s, @r output
select  'base64 디코딩', @s, @r


Set @s = @r
exec master..xp_IntCrypt_MD5_Encoding @s, @r output
select  'md5 인코딩', @s, @r

exec master..xp_IntCrypt_SHA1_Encoding @s, @r output
select  'sha1 인코딩', @s, @r
