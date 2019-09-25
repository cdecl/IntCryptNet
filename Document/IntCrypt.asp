<%
Option Explicit 

Response.AddHeader "Pragma","no-cache"
Response.AddHeader "cache-control", "no-store"
Response.Expires    = -1 


Sub Run()
	Dim s, key
	Dim o

	s = Request("q")

	Set o = Server.CreateObject("IntCryptNetSC.Secure")
	Call o.Initialize()

	key = "http://xdb.interpark.com/key/interpark.key"

	Response.Write "<table style='font-size:9pt' border='1''>"
	Response.Write "<tr><td>¿ø¹®</td><td>" & s & "</td></tr>"
	Response.Write "<tr><td>&nbsp;</td><td>&nbsp;</td></tr>"

	s = o.AES_Encrypt(key, "_0", s)
	Response.Write "<tr><td>AES_Encrypt</td><td>" & s & "</td></tr>"

	s = o.AES_Decrypt(key, "_0", s)
	Response.Write "<tr><td>AES_Decrypt</td><td>" & s & "</td></tr>"


	s = o.AES_Encrypt_UTF8(key, "_0", s)
	Response.Write "<tr><td>AES_Encrypt_UTF8</td><td>" & s & "</td></tr>"

	s = o.AES_Decrypt_UTF8(key, "_0", s)
	Response.Write "<tr><td>AES_Decrypt_UTF8</td><td>" & s & "</td></tr>"


	s = o.Base64_Encoding(s)
	Response.Write "<tr><td>Base64_Encoding</td><td>" & s & "</td></tr>"

	s = o.Base64_Decoding(s)
	Response.Write "<tr><td>Base64_Decoding</td><td>" & s & "</td></tr>"


	s = o.Base64_Encoding_UTF8(s)
	Response.Write "<tr><td>Base64_Encoding_UTF8</td><td>" & s & "</td></tr>"

	s = o.Base64_Decoding_UTF8(s)
	Response.Write "<tr><td>Base64_Decoding_UTF8</td><td>" & s & "</td></tr>"


	s = o.MD5_Encoding(s)
	Response.Write "<tr><td>MD5_Encoding</td><td>" & s & "</td></tr>"

	s = o.MD5_Encoding_UTF8(s)
	Response.Write "<tr><td>MD5_Encoding_UTF8</td><td>" & s & "</td></tr>"

	s = o.SHA1_Encoding(s)
	Response.Write "<tr><td>SHA1_Encoding</td><td>" & s & "</td></tr>"

	s = o.SHA1_Encoding_UTF8(s)
	Response.Write "<tr><td>SHA1_Encoding_UTF8</td><td>" & s & "</td></tr>"


	Response.Write " </table>"



	Set o = Nothing
End Sub 
%>


<!doctype html public "-//w3c//dtd html 4.0 transitional//en">
<html>
 <head>
  <meta http-equiv="Content-Type" content="text/html; charset=euc-kr">
  <title></title>
 </head>

 <body style="font-size:9pt">
	<% Call Run() %>
 </body>
</html>
