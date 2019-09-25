Dim o, s, r, msg

Set o = CreateObject("IntCryptNetSC.Secure")

s = "±èº´±Ôabc"

r = o.AES_Encrypt("http://211.233.74.203/Key/interpark.key", "_0", s)
msg = msg & "AES_Encrypt->" & r & vbCrLf

s = r
r = o.AES_Decrypt("http://211.233.74.203/Key/interpark.key", "_0", s)
msg = msg & "AES_Decrypt->" & r & vbCrLf

s = r
r = o.Base64_Encoding(s)
msg = msg & "Base64_Encoding->" & r & vbCrLf

s = r
r = o.Base64_Decoding(s)
msg = msg & "Base64_Decoding->" & r & vbCrLf

s = r
r = o.MD5_Encoding(s)
msg = msg & "MD5_Encoding->" & r & vbCrLf

r = o.SHA1_Encoding(s)
msg = msg & "SHA1_Encoding->" & r & vbCrLf

Set o = Nothing

MsgBox msg