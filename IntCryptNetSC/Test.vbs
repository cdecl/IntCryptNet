Dim o, s

Set o = CreateObject("IntCryptNetSC.Secure")

s = "±èº´±Ôabc"

s = o.AES_Encrypt("", "", s)
MsgBox "AES_Encrypt->" & s

s = o.AES_Decrypt("", "", s)
MsgBox "AES_Decrypt->" & s

s = o.AES_Encrypt_UTF8("", "", s)
MsgBox "AES_Encrypt_UTF8->" & s

s = o.AES_Decrypt_UTF8("", "", s)
MsgBox "AES_Decrypt_UTF8->" & s


s = o.Base64_Encoding(s)
MsgBox "Base64_Encoding->" & s

s = o.Base64_Decoding(s)
MsgBox "Base64_Decoding->" & s


s = o.Base64_Encoding_UTF8(s)
MsgBox "Base64_Encoding_UTF8->" & s

s = o.Base64_Decoding_UTF8(s)
MsgBox "Base64_Decoding_UTF8->" & s


s = o.MD5_Encoding(s)
MsgBox "MD5_Encoding->" & s

s = o.MD5_Encoding_UTF8(s)
MsgBox "MD5_Encoding_UTF8->" & s

s = o.SHA1_Encoding(s)
MsgBox "SHA1_Encoding->" & s

s = o.SHA1_Encoding_UTF8(s)
MsgBox "SHA1_Encoding_UTF8->" & s


Set o = Nothing