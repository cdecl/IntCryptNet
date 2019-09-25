using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Diagnostics;

using IntCrypt;


public partial class UserDefinedFunctions
{
    private static IntCrypt.KeyManager GetKeyManager()
    {
        object oKeyManager = AppDomain.CurrentDomain.GetData("KeyManager");
        if (oKeyManager == null)
        {
            AppDomain.CurrentDomain.SetData("KeyManager", new IntCrypt.KeyManager());
        }

        return (IntCrypt.KeyManager)oKeyManager;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Initialize()
    {
        try
        {
            object oKeyManager = AppDomain.CurrentDomain.GetData("KeyManager");
            if (oKeyManager == null)
            {
                AppDomain.CurrentDomain.SetData("KeyManager", new IntCrypt.KeyManager());
                oKeyManager = AppDomain.CurrentDomain.GetData("KeyManager");
            }

            IntCrypt.KeyManager keymanager = (IntCrypt.KeyManager)oKeyManager;
            keymanager.Initialize();

        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "SUCCESS";
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Encrypt(SqlString strDomain, SqlString strApp, SqlString s)
    {
        try
        {
            byte[] key = null;
            byte[] iv = null;
            GetKeyManager().GetKeyIv(strDomain.ToString(), strApp.ToString(), ref key, ref iv);

            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.AES_Encrypt(key, iv, s.ToString());

            return strResult;
        }
        catch (Exception) 
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Decrypt(SqlString strDomain, SqlString strApp, SqlString s)
    {
        try
        {
            byte[] key = null;
            byte[] iv = null;
            GetKeyManager().GetKeyIv(strDomain.ToString(), strApp.ToString(), ref key, ref iv);

            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.AES_Decrypt(key, iv, s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Encrypt_UTF8(SqlString strDomain, SqlString strApp, SqlString s)
    {
        try
        {
            byte[] key = null;
            byte[] iv = null;
            GetKeyManager().GetKeyIv(strDomain.ToString(), strApp.ToString(), ref key, ref iv);

            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.AES_Encrypt(key, iv, s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Decrypt_UTF8(SqlString strDomain, SqlString strApp, SqlString s)
    {
        try
        {
            byte[] key = null;
            byte[] iv = null;
            GetKeyManager().GetKeyIv(strDomain.ToString(), strApp.ToString(), ref key, ref iv);

            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.AES_Decrypt(key, iv, s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Base64_Encoding(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.Base64_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Base64_Decoding(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.Base64_Decoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Base64_Encoding_UTF8(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.Base64_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }



    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_Base64_Decoding_UTF8(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.Base64_Decoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }


    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_MD5_Encoding(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.MD5_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_MD5_Encoding_UTF8(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.MD5_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA1_Encoding(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.SHA1_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA1_Encoding_UTF8(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.SHA1_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA256_Encoding(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.SHA256_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA256_Encoding_UTF8(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.SHA256_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }


    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA384_Encoding(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.SHA384_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA384_Encoding_UTF8(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.SHA384_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA512_Encoding(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib();
            SqlString strResult = crypt.SHA512_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString xfn_IntCrypt_SHA512_Encoding_UTF8(SqlString s)
    {
        try
        {
            IntCryptLib crypt = new IntCryptLib(Encoding.UTF8);
            SqlString strResult = crypt.SHA512_Encoding(s.ToString());

            return strResult;
        }
        catch (Exception)
        {
        }

        return null;
    }
};

