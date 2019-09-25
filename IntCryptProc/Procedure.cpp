#include <stdafx.h>
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <tchar.h>

#include "COMDispatch.h"

#define XP_NOERROR              0
#define XP_ERROR                1
#define MAXCOLNAME				25
#define MAXNAME					25
#define MAXTEXT					255

enum { SZ_SIZE = 8000 };

static void Trace(LPCTSTR szFormat, ...)
{
    enum { BUFF_SIZE = 2048 };
	
    TCHAR szTempBuf[BUFF_SIZE] ;
    va_list vlMarker ;
	
    va_start(vlMarker,szFormat) ;
    _vstprintf(szTempBuf,szFormat,vlMarker) ;
    va_end(vlMarker) ;
	
    OutputDebugString(szTempBuf) ;
}




#ifdef __cplusplus
extern "C" {
#endif

RETCODE __declspec(dllexport) xp_IntCrypt_Encrypt(SRV_PROC *srvproc);
RETCODE __declspec(dllexport) xp_IntCrypt_Decrypt(SRV_PROC *srvproc);
RETCODE __declspec(dllexport) xp_IntCrypt_Base64_Encoding(SRV_PROC *srvproc);
RETCODE __declspec(dllexport) xp_IntCrypt_Base64_Decoding(SRV_PROC *srvproc);
RETCODE __declspec(dllexport) xp_IntCrypt_MD5_Encoding(SRV_PROC *srvproc);
RETCODE __declspec(dllexport) xp_IntCrypt_SHA1_Encoding(SRV_PROC *srvproc);


#ifdef __cplusplus
}
#endif

RETCODE __declspec(dllexport) xp_IntCrypt_Encrypt(SRV_PROC *srvproc)
{
	try {
		
		int nArgs = srv_rpcparams(srvproc);

		if (nArgs == 4) {

			std::string strDomain, strApp, strS;
			{
				int nLen = srv_paramlen(srvproc, 1);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 1);

				if (nLen > 0) {
					strDomain.assign((LPCSTR)pData, nLen);
				}
			}

			{
				int nLen = srv_paramlen(srvproc, 2);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 2);

				if (nLen > 0) {
					strApp.assign((LPCSTR)pData, nLen);
				}
			}

			{
				int nLen = srv_paramlen(srvproc, 3);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 3);

				if (nLen > 0) {
					strS.assign((LPCSTR)pData, nLen);
				}
			}

			::CoInitialize(NULL);
			{
				GLASS::COMDispatch disp;
				disp.CreateObject("IntCryptNetSC.Secure");
				_variant_t var = disp["AES_Encrypt"](strDomain.c_str(), strApp.c_str(), strS.c_str());
				std::string str = (LPCSTR)(_bstr_t)var;

				srv_paramset(srvproc, 4, (void*)str.c_str(), str.length());
			}
			::CoUninitialize();
			
		}
		else {

			srv_sendmsg(
				srvproc, SRV_MSG_ERROR, 20000, SRV_INFO, 1, NULL, 0,
				(DBUSMALLINT)__LINE__,
				"Usage: EXEC xp_IntCrypt_Encrypt", SRV_NULLTERM);

			srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);

			return XP_ERROR;
		}
	}
	catch (...) {
		srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);
		return XP_ERROR;
	}

	// Now return the number of rows processed
	srv_senddone(srvproc, SRV_DONE_FINAL, 0, 0);

	return XP_NOERROR ;
}



RETCODE __declspec(dllexport) xp_IntCrypt_Decrypt(SRV_PROC *srvproc)
{
	try {
		
		int nArgs = srv_rpcparams(srvproc);

		if (nArgs == 4) {

			std::string strDomain, strApp, strS;
			{
				int nLen = srv_paramlen(srvproc, 1);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 1);

				if (nLen > 0) {
					strDomain.assign((LPCSTR)pData, nLen);
				}
			}

			{
				int nLen = srv_paramlen(srvproc, 2);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 2);

				if (nLen > 0) {
					strApp.assign((LPCSTR)pData, nLen);
				}
			}

			{
				int nLen = srv_paramlen(srvproc, 3);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 3);

				if (nLen > 0) {
					strS.assign((LPCSTR)pData, nLen);
				}
			}

			::CoInitialize(NULL);
			{
				GLASS::COMDispatch disp;
				disp.CreateObject("IntCryptNetSC.Secure");
				_variant_t var = disp["AES_Decrypt"](strDomain.c_str(), strApp.c_str(), strS.c_str());
				std::string str = (LPCSTR)(_bstr_t)var;

				srv_paramset(srvproc, 4, (void*)str.c_str(), str.length());
			}
			::CoUninitialize();

		}
		else {

			srv_sendmsg(
				srvproc, SRV_MSG_ERROR, 20000, SRV_INFO, 1, NULL, 0,
				(DBUSMALLINT)__LINE__,
				"Usage: EXEC xp_IntCrypt_Decrypt", SRV_NULLTERM);

			srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);

			return XP_ERROR;
		}
	}
	catch (...) {
		srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);
		return XP_ERROR;
	}

	// Now return the number of rows processed
	srv_senddone(srvproc, SRV_DONE_FINAL, 0, 0);

	return XP_NOERROR ;
}




RETCODE __declspec(dllexport) xp_IntCrypt_Base64_Encoding(SRV_PROC *srvproc)
{
	try {
		
		int nArgs = srv_rpcparams(srvproc);

		if (nArgs == 2) {

			std::string strS;
			{
				int nLen = srv_paramlen(srvproc, 1);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 1);

				if (nLen > 0) {
					strS.assign((LPCSTR)pData, nLen);
				}
			}

			::CoInitialize(NULL);
			{
				GLASS::COMDispatch disp;
				disp.CreateObject("IntCryptNetSC.Secure");
				_variant_t var = disp["Base64_Encoding"](strS.c_str());
				std::string str = (LPCSTR)(_bstr_t)var;

				srv_paramset(srvproc, 2, (void*)str.c_str(), str.length());
			}
			::CoUninitialize();

		}
		else {

			srv_sendmsg(
				srvproc, SRV_MSG_ERROR, 20000, SRV_INFO, 1, NULL, 0,
				(DBUSMALLINT)__LINE__,
				"Usage: EXEC xp_IntCrypt_Base64_Encoding", SRV_NULLTERM);

			srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);

			return XP_ERROR;
		}
	}
	catch (...) {
		srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);
		return XP_ERROR;
	}

	// Now return the number of rows processed
	srv_senddone(srvproc, SRV_DONE_FINAL, 0, 0);

	return XP_NOERROR ;
}


RETCODE __declspec(dllexport) xp_IntCrypt_Base64_Decoding(SRV_PROC *srvproc)
{
	try {
		
		int nArgs = srv_rpcparams(srvproc);

		if (nArgs == 2) {

			std::string strS;
			{
				int nLen = srv_paramlen(srvproc, 1);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 1);

				if (nLen > 0) {
					strS.assign((LPCSTR)pData, nLen);
				}
			}

			::CoInitialize(NULL);
			{
				GLASS::COMDispatch disp;
				disp.CreateObject("IntCryptNetSC.Secure");
				_variant_t var = disp["Base64_Decoding"](strS.c_str());
				std::string str = (LPCSTR)(_bstr_t)var;

				srv_paramset(srvproc, 2, (void*)str.c_str(), str.length());
			}
			::CoUninitialize();

		}
		else {

			srv_sendmsg(
				srvproc, SRV_MSG_ERROR, 20000, SRV_INFO, 1, NULL, 0,
				(DBUSMALLINT)__LINE__,
				"Usage: EXEC xp_IntCrypt_Base64_Decoding", SRV_NULLTERM);

			srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);

			return XP_ERROR;
		}
	}
	catch (...) {
		srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);
		return XP_ERROR;
	}

	// Now return the number of rows processed
	srv_senddone(srvproc, SRV_DONE_FINAL, 0, 0);

	return XP_NOERROR ;
}




RETCODE __declspec(dllexport) xp_IntCrypt_MD5_Encoding(SRV_PROC *srvproc)
{
	try {
		
		int nArgs = srv_rpcparams(srvproc);

		if (nArgs == 2) {

			std::string strS;
			{
				int nLen = srv_paramlen(srvproc, 1);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 1);

				if (nLen > 0) {
					strS.assign((LPCSTR)pData, nLen);
				}
			}

			::CoInitialize(NULL);
			{
				GLASS::COMDispatch disp;
				disp.CreateObject("IntCryptNetSC.Secure");
				_variant_t var = disp["MD5_Encoding"](strS.c_str());
				std::string str = (LPCSTR)(_bstr_t)var;

				srv_paramset(srvproc, 2, (void*)str.c_str(), str.length());
			}
			::CoUninitialize();

		}
		else {

			srv_sendmsg(
				srvproc, SRV_MSG_ERROR, 20000, SRV_INFO, 1, NULL, 0,
				(DBUSMALLINT)__LINE__,
				"Usage: EXEC xp_IntCrypt_MD5_Encoding", SRV_NULLTERM);

			srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);

			return XP_ERROR;
		}
	}
	catch (...) {
		srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);
		return XP_ERROR;
	}

	// Now return the number of rows processed
	srv_senddone(srvproc, SRV_DONE_FINAL, 0, 0);

	return XP_NOERROR ;
}



RETCODE __declspec(dllexport) xp_IntCrypt_SHA1_Encoding(SRV_PROC *srvproc)
{
	try {
		
		int nArgs = srv_rpcparams(srvproc);

		if (nArgs == 2) {

			std::string strS;
			{
				int nLen = srv_paramlen(srvproc, 1);
				BYTE* pData = (BYTE*)srv_paramdata(srvproc, 1);

				if (nLen > 0) {
					strS.assign((LPCSTR)pData, nLen);
				}
			}

			::CoInitialize(NULL);
			{
				GLASS::COMDispatch disp;
				disp.CreateObject("IntCryptNetSC.Secure");
				_variant_t var = disp["SHA1_Encoding"](strS.c_str());
				std::string str = (LPCSTR)(_bstr_t)var;

				srv_paramset(srvproc, 2, (void*)str.c_str(), str.length());
			}
			::CoUninitialize();

		}
		else {

			srv_sendmsg(
				srvproc, SRV_MSG_ERROR, 20000, SRV_INFO, 1, NULL, 0,
				(DBUSMALLINT)__LINE__,
				"Usage: EXEC xp_IntCrypt_SHA1_Encoding", SRV_NULLTERM);

			srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);

			return XP_ERROR;
		}
	}
	catch (...) {
		srv_senddone(srvproc, SRV_DONE_ERROR | SRV_DONE_MORE, 0, 0);
		return XP_ERROR;
	}

	// Now return the number of rows processed
	srv_senddone(srvproc, SRV_DONE_FINAL, 0, 0);

	return XP_NOERROR ;
}


