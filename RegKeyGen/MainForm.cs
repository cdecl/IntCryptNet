using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Security.Cryptography;
using Microsoft.Win32;

using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;




namespace RegKeyGen
{
    using KeyValueType = KeyValuePair<byte[], byte[]>;
    using KeyType = Dictionary<string, KeyValuePair<byte[], byte[]>>;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static readonly string SUBKEYROOT = @"SOFTWARE\IntCrypt\";
        public static readonly string KEY_PROTO = "INTCRYPT://";

        private static readonly byte[] default_key =
            { 0x93, 0x33, 0x6B, 0x82, 0xD6, 0x64, 0xB2, 0x46,
          0x95, 0xAB, 0x89, 0x91, 0xD3, 0xE5, 0xDC, 0xB0 };

        private static readonly byte[] default_iv =
            { 0x61, 0x4D, 0xCA, 0x6F, 0xB2, 0x56, 0xF1, 0xDB,
          0x0B, 0x24, 0x5D, 0xCF, 0xB4, 0xBD, 0xB6, 0xD3 };

        private static KeyType KeyRepo_ = new KeyType();

        private static string GetKeyRepository(string strUrl)
        {
            WebClient wc = new WebClient();
            StreamReader r = new StreamReader(wc.OpenRead(strUrl), Encoding.Default);

            return r.ReadToEnd();
        }

        private static bool CacheKeyRepo(string strDomain)
        {
            bool bSuccess = false;

            try
            {
                string strSerialize = GetKeyRepository(strDomain);

                if (strSerialize.Substring(0, KEY_PROTO.Length) == KEY_PROTO)
                {
                    using (IntCryptNetSC.Secure sc = new IntCryptNetSC.Secure())
                    {
                        strSerialize = strSerialize.Substring(KEY_PROTO.Length, strSerialize.Length - KEY_PROTO.Length);
                        // default key로 키 복호화 
                        strSerialize = sc.AES_Decrypt("", "", strSerialize);
                    }
                }

                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strSerialize));
                BinaryFormatter fmt = new BinaryFormatter();

                KeyRepo_ = (KeyType)fmt.Deserialize(stream);
                bSuccess = true;
            }
            catch (Exception) { }

            return bSuccess;
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
                    if (strDomain.IndexOf(HTTP_PRE) == 0 || strDomain.IndexOf(HTTPS_PRE) == 0)
                    {
                        if (!KeyRepo_.ContainsKey(strApp))
                        {
                            CacheKeyRepo(strDomain);
                        }

                        KeyValueType keyv = new KeyValueType();
                        if (!KeyRepo_.TryGetValue(strApp, out keyv))
                        {
                            throw new Exception();
                        }

                        key = keyv.Key;
                        iv = keyv.Value;
                    }
                    else
                    {
                        string strSubKey = SUBKEYROOT + strDomain;

                        RegistryKey SubKeyDefault = Registry.LocalMachine.OpenSubKey(strSubKey, false);
                        if (SubKeyDefault == null)
                        {
                            throw new Exception();
                        }

                        RegistryKey RKey = SubKeyDefault.OpenSubKey(strApp, false);
                        if (RKey == null)
                        {
                            throw new Exception();
                        }


                        key = (byte[])RKey.GetValue("key");
                        iv = (byte[])RKey.GetValue("iv");

                        if (key == null || iv == null) throw new Exception();
                    }
                }

                bSuccess = true;
            }
            catch (Exception)
            {

            }

            return bSuccess;
        }


        private void btnGen_Click(object sender, EventArgs e)
        {
            try
            {
                string strDomain = txtNewDomain.Text.Trim();
                if (string.IsNullOrEmpty(strDomain))
                {
                    MessageBox.Show("키 도메인 이름을 넣으세요");
                    return;
                }

                RegistryKey SubKeyDefault = Registry.LocalMachine.OpenSubKey(SUBKEYROOT + strDomain, true);
                if (SubKeyDefault != null)
                {
                    MessageBox.Show("이미 키가 존재합니다. ");
                    return;
                }

                RijndaelManaged Aes = new RijndaelManaged();
                Aes.KeySize = 128;

                SubKeyDefault = Registry.LocalMachine.CreateSubKey(SUBKEYROOT + strDomain);

                for (int i = 0; i <= 9; ++i)
                {
                    Aes.GenerateKey();
                    Aes.GenerateIV();

                    RegistryKey RKey = SubKeyDefault.CreateSubKey(string.Format("_{0}", i));
                    RKey.SetValue("key", Aes.Key);
                    RKey.SetValue("iv", Aes.IV);
                }

                MessageBox.Show("성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnGetKey_Click(object sender, EventArgs e)
        {
            byte[] key = new byte[16];
            byte[] iv = new byte[16];


            if (GetKeyIv(txtDomain.Text, txtApp.Text, ref key, ref iv))
            {
                string s = "key : ";
                foreach (byte b in key)
                {
                    s += string.Format("{0:x},", b);
                }

                s += "\n"; 
                s += "iv : ";
                foreach (byte b in iv)
                {
                    s += string.Format("{0:x},", b);
                }

                MessageBox.Show(s);
            }
            else
            {
                MessageBox.Show("실패");
            }
            
        }

        private void btnEnc_Click(object sender, EventArgs e)
        {
            try
            {
                using (IntCryptNetSC.Secure sc = new IntCryptNetSC.Secure())
                {
                    string strEnc = sc.AES_Encrypt(txtDomain.Text, txtApp.Text, txtSource.Text);
                    if (strEnc == null)
                    {
                        MessageBox.Show("실패");
                        return;
                    }

                    txtEncrypt.Text = strEnc;
                    txtRecover.Text = sc.AES_Decrypt(txtDomain.Text, txtApp.Text, txtEncrypt.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBase64_Click(object sender, EventArgs e)
        {
            try
            {
                using (IntCryptNetSC.Secure sc = new IntCryptNetSC.Secure())
                {
                    string strEnc = sc.Base64_Encoding(txtSource.Text);
                    if (strEnc == null)
                    {
                        MessageBox.Show("실패");
                        return;
                    }

                    txtEncrypt.Text = strEnc;
                    txtRecover.Text = sc.Base64_Decoding(txtEncrypt.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Hash_Click(object sender, EventArgs e)
        {
            using (IntCryptNetSC.Secure sc = new IntCryptNetSC.Secure())
            {
                txtEncrypt.Text = sc.MD5_Encoding(txtSource.Text);
                txtRecover.Text = sc.SHA1_Encoding(txtSource.Text);
            }
        }

        private void btnKeyExport_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey SubKeyDefault = Registry.LocalMachine.OpenSubKey(SUBKEYROOT, true);
                if (SubKeyDefault == null)
                {
                    MessageBox.Show("키가 존재하지 않습니다.");
                    return;
                }

                string[] arrDomainKey = SubKeyDefault.GetSubKeyNames();
                foreach (string strDomain in arrDomainKey) 
                {
                    KeyType repo = new KeyType();

                    RegistryKey KeyDomain = SubKeyDefault.OpenSubKey(strDomain);
                    string[] arrApp = KeyDomain.GetSubKeyNames();

                    foreach (string strApp in arrApp)
                    {
                        RegistryKey Key = KeyDomain.OpenSubKey(strApp);

                        byte []key = (byte[])Key.GetValue("key");
                        byte []iv = (byte[])Key.GetValue("iv");

                        repo.Add(strApp, new KeyValueType(key, iv));

                        Trace.WriteLine(strApp);
                    }


                    FileExport(strDomain, repo);
                }

                MessageBox.Show("성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FileExport(string strDomain, KeyType repo)
        {
            using (StreamWriter fs = new StreamWriter(File.Create(strDomain + ".key")))
            using (MemoryStream stream = new MemoryStream())
            using (IntCryptNetSC.Secure sc = new IntCryptNetSC.Secure())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, repo);

                byte[] arr = stream.ToArray();
                
                // default key로 키 암호화 
                string strEncKey = sc.AES_Encrypt("", "", Convert.ToBase64String(arr));
                string str = string.Format("{0}{1}", KEY_PROTO, strEncKey);

                fs.WriteLine(str);
            }
        }

        private void btnKeyFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("구현되지 않았습니다.");
            /*
            using (StreamReader fs = new StreamReader(File.OpenRead("Key.txt")))
            {
                if (fs == null)
                {
                    MessageBox.Show("Key.txt Error");
                }

                string str = fs.ReadToEnd();
                byte[] arr = Convert.FromBase64String(str);

                MemoryStream stream = new MemoryStream(arr);
                BinaryFormatter formatter = new BinaryFormatter();

                KeyType repo = (KeyType)formatter.Deserialize(stream);

                string strMessage = "";

                byte[] keyarr = repo["Default__3"].Key;

                foreach (byte b in keyarr)
                {
                    strMessage += string.Format("{0:x2},", b);
                }

                Trace.WriteLine(strMessage);

            }
             * */
        }

        private void btnBToH_Click(object sender, EventArgs e)
        {
            byte[] arr = Convert.FromBase64String(txtSource.Text);

            string strResult = "";
            foreach (byte b in arr)
            {
                strResult += string.Format("{0:x2},", b);
            }

            txtEncrypt.Text = strResult;
            txtRecover.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}