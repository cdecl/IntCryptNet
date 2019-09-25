

Declare @s varchar(1024)
Set @s = '암호화 문자열 - abc'

Select dbo.xfn_IntCrypt_Encrypt('', '', @s), 
	dbo.xfn_IntCrypt_Decrypt('', '', dbo.xfn_IntCrypt_Encrypt('', '', @s))

Select dbo.xfn_IntCrypt_Encrypt_UTF8('', '', @s),
	dbo.xfn_IntCrypt_Decrypt_UTF8('', '', dbo.xfn_IntCrypt_Encrypt_UTF8('', '', @s))

Select dbo.xfn_IntCrypt_Base64_Encoding(@s), 
	dbo.xfn_IntCrypt_Base64_Decoding(dbo.xfn_IntCrypt_Base64_Encoding(@s))

Select dbo.xfn_IntCrypt_Base64_Encoding_UTF8(@s), 
	dbo.xfn_IntCrypt_Base64_Decoding_UTF8(dbo.xfn_IntCrypt_Base64_Encoding_UTF8(@s))

Select dbo.xfn_IntCrypt_Encrypt_UTF8('', '', @s), 
	dbo.xfn_IntCrypt_Decrypt_UTF8('', '', dbo.xfn_IntCrypt_Encrypt_UTF8('', '', @s))

Select dbo.xfn_IntCrypt_MD5_Encoding(@s)
Select dbo.xfn_IntCrypt_MD5_Encoding_UTF8(@s)

Select dbo.xfn_IntCrypt_SHA1_Encoding(@s)
Select dbo.xfn_IntCrypt_SHA1_Encoding_UTF8(@s)



-- AppKey : '_0' to '_50'
Select dbo.xfn_IntCrypt_Encrypt('', '_0', @s), 
	dbo.xfn_IntCrypt_Decrypt('', '_0', dbo.xfn_IntCrypt_Encrypt('', '_0', @s))


