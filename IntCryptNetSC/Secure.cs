using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;
using System.EnterpriseServices;

namespace IntCryptNetSC
{
    [JustInTimeActivation(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [EventTrackingEnabled(true)]
    [ObjectPooling(Enabled=true, MinPoolSize=2)]
    public class Secure : ServicedComponent
    {
        private static IntCrypt.KeyManager keymanager_ = new IntCrypt.KeyManager();

        [AutoComplete(true)]
        public bool Initialize()
        {
            try
            {
                keymanager_.Initialize();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [AutoComplete(true)]
        public string AES_Encrypt(string strDomain, string strApp, string s)
        {
            string strResult = "";

            try
            {
                byte[] key = null;
                byte[] iv = null;
                keymanager_.GetKeyIv(strDomain, strApp, ref key, ref iv);

                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.AES_Encrypt(key, iv, s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string AES_Decrypt(string strDomain, string strApp, string s)
        {
            string strResult = "";

            try
            {
                byte[] key = null;
                byte[] iv = null;
                keymanager_.GetKeyIv(strDomain, strApp, ref key, ref iv);

                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.AES_Decrypt(key, iv, s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string AES_Encrypt_UTF8(string strDomain, string strApp, string s)
        {
            string strResult = "";

            try
            {
                byte[] key = null;
                byte[] iv = null;
                keymanager_.GetKeyIv(strDomain, strApp, ref key, ref iv);

                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.AES_Encrypt(key, iv, s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string AES_Decrypt_UTF8(string strDomain, string strApp, string s)
        {
            string strResult = "";

            try
            {
                byte[] key = null;
                byte[] iv = null;
                keymanager_.GetKeyIv(strDomain, strApp, ref key, ref iv);

                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.AES_Decrypt(key, iv, s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string Base64_Encoding(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.Base64_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string Base64_Decoding(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.Base64_Decoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string Base64_Encoding_UTF8(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.Base64_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string Base64_Decoding_UTF8(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.Base64_Decoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string MD5_Encoding(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.MD5_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string MD5_Encoding_UTF8(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.MD5_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA1_Encoding(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.SHA1_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA1_Encoding_UTF8(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.SHA1_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA256_Encoding(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.SHA256_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA256_Encoding_UTF8(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.SHA256_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA384_Encoding(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.SHA384_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA384_Encoding_UTF8(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.SHA384_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA512_Encoding(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                strResult = crypt.SHA512_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        [AutoComplete(true)]
        public string SHA512_Encoding_UTF8(string s)
        {
            string strResult = "";

            try
            {
                IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib(Encoding.UTF8);
                strResult = crypt.SHA512_Encoding(s);
            }
            catch (Exception)
            {
            }

            return strResult;
        }




        // SEED, ECB Mode, Zero Padding, Euc-kr
        [AutoComplete(true)]
        public string SEED_Encrypt(string key, string s)
        {
            string strResult = "";

            try
            {
                SEED_.SeedProvider seed = new SEED_.SeedProvider();
                seed.MasterKey = StrToByte16(key);

                strResult = Convert.ToBase64String(seed.EncryptToByteArray(s));
            }
            catch (Exception)
            {
            }

            return strResult;
        }

        // SEED, ECB Mode, Zero Padding, Euc-kr
        [AutoComplete(true)]
        public string SEED_Decrypt(string key, string s)
        {
            string strResult = "";

            try
            {
                SEED_.SeedProvider seed = new SEED_.SeedProvider();
                seed.MasterKey = StrToByte16(key);

                strResult = seed.DecryptFromByteArray(Convert.FromBase64String(s));
            }
            catch (Exception)
            {
            }

            // NULL 패딩이라 제거해줘야 함
            return strResult.TrimEnd(new char[] { '\0' });
        }

        private byte[] StrToByte16(string s)
        {
            byte[] key = new byte[16];
            byte[] src = Encoding.GetEncoding("euc-kr").GetBytes(s);

            Array.Copy(src, key, src.Length < 16 ? src.Length : 16);
            return key;
        }
    }
}
