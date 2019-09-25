Dim o, s, msg, key
s = "±èº´±Ôabc"

Set o = CreateObject("IntCryptNetSC.Secure")
Call o.Initialize()

key = "http://localhost/key/ent.key"
'key = "ent"

s = o.AES_Encrypt(key, "_0", s)
msg = msg &  "AES_Encrypt->" & s & chr(13) & chr(10)

s = o.AES_Decrypt(key, "_0", s)
msg = msg &  "AES_Decrypt->" & s & chr(13) & chr(10)


s = o.AES_Encrypt_UTF8(key, "_0", s)
msg = msg &  "AES_Encrypt_UTF8->" & s & chr(13) & chr(10)

s = o.AES_Decrypt_UTF8(key, "_0", s)
msg = msg &  "AES_Decrypt_UTF8->" & s & chr(13) & chr(10)


s = o.Base64_Encoding(s)
msg = msg &  "Base64_Encoding->" & s & chr(13) & chr(10)

s = o.Base64_Decoding(s)
msg = msg &  "Base64_Decoding->" & s & chr(13) & chr(10)


s = o.Base64_Encoding_UTF8(s)
msg = msg &  "Base64_Encoding_UTF8->" & s & chr(13) & chr(10)

s = o.Base64_Decoding_UTF8(s)
msg = msg &  "Base64_Decoding_UTF8->" & s & chr(13) & chr(10)


s = o.MD5_Encoding(s)
msg = msg &  "MD5_Encoding->" & s & chr(13) & chr(10)

s = o.MD5_Encoding_UTF8(s)
msg = msg &  "MD5_Encoding_UTF8->" & s & chr(13) & chr(10)

s = o.SHA1_Encoding(s)
msg = msg &  "SHA1_Encoding->" & s & chr(13) & chr(10)

s = o.SHA1_Encoding_UTF8(s)
msg = msg &  "SHA1_Encoding_UTF8->" & s & chr(13) & chr(10)

MsgBox msg

call o.dispose()

Set o = Nothing

'MsgBox "END"

