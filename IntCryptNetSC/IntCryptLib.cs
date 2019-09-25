using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.Win32;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.IO;
using System.Net;


namespace IntCrypt
{
    using KeyValueType = KeyValuePair<byte[], byte[]>;
    using KeyType = Dictionary<string, KeyValuePair<byte[], byte[]>>;
    using KeyManagerType = Dictionary<string, Dictionary<string, KeyValuePair<byte[], byte[]>>>;

    class KeyManager
    {
        public static readonly string SUBKEYROOT = @"SOFTWARE\IntCrypt\";
        public static readonly string KEY_PROTO = "INTCRYPT://";

        private static readonly byte[] default_key =
            { 0x93, 0x33, 0x6B, 0x82, 0xD6, 0x64, 0xB2, 0x46,
          0x95, 0xAB, 0x89, 0x91, 0xD3, 0xE5, 0xDC, 0xB0 };

        private static readonly byte[] default_iv =
            { 0x61, 0x4D, 0xCA, 0x6F, 0xB2, 0x56, 0xF1, 0xDB,
          0x0B, 0x24, 0x5D, 0xCF, 0xB4, 0xBD, 0xB6, 0xD3 };

        private KeyManagerType keyman_ = new KeyManagerType();

        private string GetKeyRepository(string strUrl)
        {
            WebClient wc = new WebClient();
            StreamReader r = new StreamReader(wc.OpenRead(strUrl), Encoding.Default);

            return r.ReadToEnd();
        }

        private bool CacheKeyRepo(string strDomain)
        {
            bool bSuccess = false;

            try
            {
                string sType = "SERIALIZE";
                string strSerialize = GetKeyRepository(strDomain);

                if (strSerialize.Substring(0, KEY_PROTO.Length) == KEY_PROTO)
                {
                    strSerialize = strSerialize.Substring(KEY_PROTO.Length, strSerialize.Length - KEY_PROTO.Length);

                    // default key로 키 복호화 
                    IntCrypt.IntCryptLib crypt = new IntCrypt.IntCryptLib();
                    strSerialize = crypt.AES_Decrypt(default_key, default_iv, strSerialize);

                    sType = KEY_PROTO;                    
                }
                                
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strSerialize));
                BinaryFormatter fmt = new BinaryFormatter();

                KeyType repo = (KeyType)fmt.Deserialize(stream);
                keyman_.Add(strDomain, repo);

                Trace.WriteLine(string.Format("IntCryptLib::CacheKeyRepo : [{0}] {1}", sType, strDomain));
                bSuccess = true;
            }
            catch (Exception) 
            {
                Trace.WriteLine(string.Format("IntCryptLib::CacheKeyRepo [Failed]: {0}", strDomain));
            }

            return bSuccess;
        }

        private bool CacheKeyRepoFromReg(string strDomain)
        {
            bool bSuccess = false;

            try
            {
                RegistryKey SubKeyDefault = Registry.LocalMachine.OpenSubKey(SUBKEYROOT, true);
                if (SubKeyDefault == null)
                {
                    throw new Exception();
                }

                KeyType repo = new KeyType();

                
                RegistryKey KeyDomain = SubKeyDefault.OpenSubKey(strDomain);
                string[] arrApp = KeyDomain.GetSubKeyNames();

                foreach (string strApp in arrApp)
                {
                    RegistryKey Key = KeyDomain.OpenSubKey(strApp);

                    byte[] key = (byte[])Key.GetValue("key");
                    byte[] iv = (byte[])Key.GetValue("iv");

                    repo.Add(strApp, new KeyValueType(key, iv));
                }

                keyman_.Add(strDomain, repo);

                Trace.WriteLine(string.Format("IntCryptLib::CacheKeyRepoFromReg : {0}", strDomain));
                bSuccess = true;
            }
            catch (Exception) 
            {
                Trace.WriteLine(string.Format("IntCryptLib::CacheKeyRepoFromReg [Failed] : {0}", strDomain));
            }

            return bSuccess;
        }

        public void Initialize()
        {
            keyman_.Clear();
        }

        public bool GetKeyIv(string strDomain, string strApp, ref byte[] key, ref byte[] iv)
        {
            bool bSuccess = false;
            const string HTTP_PRE = "http://";
            const string HTTPS_PRE = "https://";

            try
            {
                if (strDomain.Length == 0 && strApp.Length == 0)
                {
                    key = default_key;
                    iv = default_iv;
                }
                else
                {
                    if (!keyman_.ContainsKey(strDomain))
                    {
                        //if (strDomain.IndexOf(HTTP_PRE) == 0)
                        if (strDomain.IndexOf(HTTP_PRE) == 0 || strDomain.IndexOf(HTTPS_PRE) == 0)
                        {
                            CacheKeyRepo(strDomain);
                        }
                        else
                        {
                            CacheKeyRepoFromReg(strDomain);
                        }
                    }

                    KeyType domainKey = new KeyType();
                    if (!keyman_.TryGetValue(strDomain, out domainKey))
                    {
                        throw new Exception();
                    }

                    KeyValueType keyv = new KeyValueType();
                    if (!domainKey.TryGetValue(strApp, out keyv))
                    {
                        throw new Exception();
                    }

                    key = keyv.Key;
                    iv = keyv.Value;

                    if (key == null || iv == null) throw new Exception();
                }

                bSuccess = true;
            }
            catch (Exception)
            {

            }

            return bSuccess;
        }
    }


    class IntCryptLib
    {
        private Encoding Ecd_ = null;

        public IntCryptLib()
        {
            Ecd_ = Encoding.GetEncoding("euc-kr");
        }

        public IntCryptLib(Encoding e)
        {
            Ecd_ = e;
        }


        public string AES_Encrypt(byte[] key, byte[] iv, string s)
        {
            try
            {
                RijndaelManaged Aes = new RijndaelManaged();

                Aes.Mode = CipherMode.CBC;
                Aes.KeySize = 128;
                Aes.BlockSize = 128;
                Aes.Key = key;
                Aes.IV = iv;

                ICryptoTransform transform = Aes.CreateEncryptor();
                byte[] plainText = Ecd_.GetBytes(s);
                byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

                return Convert.ToBase64String(cipherBytes);
            }
            catch (Exception)
            {

            }

            return null;
        }


        public string AES_Decrypt(byte[] key, byte[] iv, string s)
        {
            try
            {
                RijndaelManaged Aes = new RijndaelManaged();

                Aes.Mode = CipherMode.CBC;
                Aes.KeySize = 128;
                Aes.BlockSize = 128;

                byte[] encryptedData = Convert.FromBase64String(s);
                Aes.Key = key;
                Aes.IV = iv;

                ICryptoTransform transform = Aes.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                return Ecd_.GetString(plainText);
            }
            catch (Exception)
            {

            }

            return null;
        }

        public string Base64_Encoding(string s)
        {
            try
            {
                byte[] plainText = Ecd_.GetBytes(s);
                return Convert.ToBase64String(plainText);
            }
            catch (Exception)
            {

            }

            return null;
        }

        public string Base64_Decoding(string s)
        {
            try
            {
                byte[] encodingData = Convert.FromBase64String(s);
                return Ecd_.GetString(encodingData);
            }
            catch (Exception)
            {
            }

            return null;
        }

        public string MD5_Encoding(string s)
        {
            try
            {
                String strData = "";

                byte[] plainTextBytes = Ecd_.GetBytes(s);

                HashAlgorithm Hash;
                Hash = new MD5CryptoServiceProvider();

                byte[] hashed = Hash.ComputeHash(plainTextBytes);
                for (int i = 0; i < hashed.Length; ++i)
                {
                    strData += string.Format("{0:x2}", hashed[i]);
                }

                //MD 5 변환
                return strData;
            }
            catch (Exception) { }

            return null;
        }

        public string SHA1_Encoding(string s)
        {
            try
            {
                String strData = "";

                byte[] plainTextBytes = Ecd_.GetBytes(s);

                HashAlgorithm Hash;
                Hash = new SHA1CryptoServiceProvider();

                byte[] hashed = Hash.ComputeHash(plainTextBytes);
                for (int i = 0; i < hashed.Length; ++i)
                {
                    strData += string.Format("{0:x2}", hashed[i]);
                }

                //MD 5 변환
                return strData;
            }
            catch (Exception) { }

            return null;
        }

        public string SHA256_Encoding(string s)
        {
            try
            {
                String strData = "";

                byte[] plainTextBytes = Ecd_.GetBytes(s);

                HashAlgorithm Hash;
                Hash = new SHA256CryptoServiceProvider();

                byte[] hashed = Hash.ComputeHash(plainTextBytes);
                for (int i = 0; i < hashed.Length; ++i)
                {
                    strData += string.Format("{0:x2}", hashed[i]);
                }

                //MD 5 변환
                return strData;
            }
            catch (Exception) { }

            return null;
        }

        public string SHA384_Encoding(string s)
        {
            try
            {
                String strData = "";

                byte[] plainTextBytes = Ecd_.GetBytes(s);

                HashAlgorithm Hash;
                Hash = new SHA384CryptoServiceProvider();

                byte[] hashed = Hash.ComputeHash(plainTextBytes);
                for (int i = 0; i < hashed.Length; ++i)
                {
                    strData += string.Format("{0:x2}", hashed[i]);
                }

                //MD 5 변환
                return strData;
            }
            catch (Exception) { }

            return null;
        }

        public string SHA512_Encoding(string s)
        {
            try
            {
                String strData = "";

                byte[] plainTextBytes = Ecd_.GetBytes(s);

                HashAlgorithm Hash;
                Hash = new SHA512CryptoServiceProvider();

                byte[] hashed = Hash.ComputeHash(plainTextBytes);
                for (int i = 0; i < hashed.Length; ++i)
                {
                    strData += string.Format("{0:x2}", hashed[i]);
                }

                //MD 5 변환
                return strData;
            }
            catch (Exception) { }

            return null;
        }
    }
}
