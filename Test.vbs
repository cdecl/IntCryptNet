Dim o, s, txt, msg
On Error Resume Next

Set o = CreateObject("IntCryptNetSC.Secure")

s = ""
txt = "±èº´±Ôabc"

s = o.Base64_Encoding(txt)
msg = msg &  "Base64_Encoding->" & s & chr(13) & chr(10)

s = o.Base64_Decoding(s)
msg = msg &  "Base64_Decoding->" & s & chr(13) & chr(10)


s = o.Base64_Encoding_UTF8(txt)
msg = msg &  "Base64_Encoding_UTF8->" & s & chr(13) & chr(10)

s = o.Base64_Decoding_UTF8(s)
msg = msg &  "Base64_Decoding_UTF8->" & s & chr(13) & chr(10)


s = o.MD5_Encoding(txt)
msg = msg &  "MD5_Encoding->" & s & chr(13) & chr(10)

s = o.MD5_Encoding_UTF8(s)
msg = msg &  "MD5_Encoding_UTF8->" & s & chr(13) & chr(10)

s = o.SHA1_Encoding(txt)
msg = msg &  "SHA1_Encoding->" & s & chr(13) & chr(10)

s = o.SHA1_Encoding_UTF8(txt)
msg = msg &  "SHA1_Encoding_UTF8->" & s & chr(13) & chr(10)


s = o.AES_Encrypt("https://cdecl.interpark.com/ent.txt", "_0", txt)
msg = msg &  "AES_Encrypt->" & s & chr(13) & chr(10)

s = o.AES_Decrypt("https://cdecl.interpark.com/ent.txt", "_0", txt)
msg = msg &  "AES_Decrypt->" & s & chr(13) & chr(10)



s = o.AES_Encrypt("ent", "_0", txt)
msg = msg &  "AES_Encrypt->" & s & chr(13) & chr(10)

s = o.AES_Decrypt("ent", "_0", txt)
msg = msg &  "AES_Decrypt->" & s & chr(13) & chr(10)


MsgBox msg

o.Dispose
Set o = Nothing

'MsgBox "END"

