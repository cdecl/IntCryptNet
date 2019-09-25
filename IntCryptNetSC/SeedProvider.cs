using System;
using System.Collections.Generic;
using System.Text;

namespace SEED_
{
    /// <summary>
    /// 시드(SEED) 알고리즘  공급자 클래스 Ver 1.3
    /// </summary>
    /// <remark>06.06.21 - ProtoType - 방실</newpara>
    /// <remark>06.08.24 - 프로세스 변경..이전 버전과 호환 안됨.. - 방실</newpara>
    /// <remark>06.08.29 - 복호화 버그 수정. - 방실</newpara>
    public class SeedProvider
    {
        #region Field
        SeedAlgorithm seed;
        private byte[] masterKey;

        private byte[][] plainTextBlock = null;
        private byte[][] cipherTextBlock = null;
        #endregion

        #region Property
        /// <summary>
        /// 암복호화에 이용할 마스터 키(Length = 16)
        /// </summary>
        public byte[] MasterKey
        {
            get { return masterKey; }
            set
            {
                if (value.Length != 16)
                {
                    throw new ArgumentException("MasterKey's Length : " + value.Length.ToString());
                }
                else
                {
                    this.masterKey = value;
                }
            }
        }
        #endregion

        #region 생성자
        /// <summary>
        /// 기본 키 사이즈는 128비트
        /// </summary>
        public SeedProvider()
        {
            this.seed = new SeedAlgorithm();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 마스터키의 기본값을 정의 한다.
        /// </summary>
        private void SetDefaultMasterKey()
        {
            if (this.masterKey == null)
            {
                this.masterKey = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    this.masterKey[i] = (byte)i;
                }
            }
        }

        #region Encrypt
        /// <summary>
        /// 입력 받은 값을 암호화한다.
        /// </summary>
        /// <param name="inputText">암호화할 문자열입니다.</param>
        private void Encrypt(string inputText)
        {
            this.SetDefaultMasterKey();

            this.EncryptDivisionBlock(inputText);
            this.Encrypt();
        }

        /// <summary>
        /// 암호화를 처리 한다.
        /// </summary>
        private void Encrypt()
        {
            this.cipherTextBlock = new byte[this.plainTextBlock.Length][];
            for (int i = 0; i < this.plainTextBlock.Length; i++)
            {
                this.cipherTextBlock[i] = new byte[16];
                cipherTextBlock[i] = this.seed.Encrypt(this.plainTextBlock[i], this.masterKey);
            }
        }
        #endregion

        #region Decrypt
        /// <summary>
        /// 암호화된 문자열을 복호화합니다.
        /// </summary>
        /// <param name="inputText">암호화된 문자열입니다.</param>
        private void Decrypt(string inputText)
        {
            this.SetDefaultMasterKey();

            this.DecryptDivisionBlock(inputText);
            this.Decrypt();
        }

        /// <summary>
        /// 암호화된 byte[]을 복호화합니다.
        /// </summary>
        /// <param name="inputArray">암호화된 byte[]입니다.</param>
        private void Decrypt(byte[] inputArray)
        {
            this.SetDefaultMasterKey();

            this.DecryptDivisionBlock(inputArray);
            this.Decrypt();
        }

        /// <summary>
        /// 복호화를 처리한다.
        /// </summary>
        private void Decrypt()
        {
            this.plainTextBlock = new byte[this.cipherTextBlock.Length][];
            for (int i = 0; i < this.cipherTextBlock.Length; i++)
            {
                this.plainTextBlock[i] = new byte[16];
                plainTextBlock[i] = this.seed.Decrypt(this.cipherTextBlock[i], this.masterKey);
            }
        }
        #endregion

        /// <summary>
        /// 입력된 문자열을 byte[]으로 변환시킨다.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private byte[] ConvertToByteArrayFromString(string str)
        {
            byte[] temp = Encoding.Default.GetBytes(str);


            if (temp.Length % 16 != 0)
            {
                byte[] temp2 = new byte[((temp.Length / 16) + 1) * 16];
                Buffer.BlockCopy(temp, 0, temp2, 0, temp.Length);
                return temp2;
            }
            else
            {
                return temp;
            }
        }

        /// <summary>
        /// 입력된 암호화된 문자열을 byte[]으로 변환시킨다.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private byte[] EncryptedConvertToByteArrayFromString(string str)
        {
            byte[] temp = new byte[str.Length / 2];
            for (int i = 0; i < temp.Length; i++)
            {
                string hex = str.Substring(i * 2, 2);
                temp[i] = Convert.ToByte(Convert.ToInt32(hex, 16));
            }

            if (temp.Length % 16 != 0)
            {
                byte[] temp2 = new byte[((temp.Length / 16) + 1) * 16];
                Buffer.BlockCopy(temp, 0, temp2, 0, temp.Length);
                return temp2;
            }
            else
            {
                return temp;
            }
        }

        /// <summary>
        /// 입력된 문자열을 byte[]에 16바이트로 나누어서 블럭 카피 한다.
        /// </summary>
        /// <param name="plainText"></param>
        private void EncryptDivisionBlock(string plainText)
        {
            byte[] temp = this.ConvertToByteArrayFromString(plainText);
            int index = (temp.Length - 1) / 16 + 1;
            this.plainTextBlock = new byte[index][];
            for (int i = 0, j = 0; j < index; i += 16, j++)
            {
                this.plainTextBlock[j] = new byte[16];
                Buffer.BlockCopy(temp, i, this.plainTextBlock[j], 0, 16);
            }
        }

        /// <summary>
        /// 입력된 암호화된 문자열을 byte[]에 16바이트로 나누어서 블럭 카피 한다.
        /// </summary>
        /// <param name="cipherText"></param>
        private void DecryptDivisionBlock(string cipherText)
        {
            byte[] temp = this.EncryptedConvertToByteArrayFromString(cipherText);
            int index = (temp.Length - 1) / 16 + 1;
            this.cipherTextBlock = new byte[index][];
            for (int i = 0, j = 0; j < index; i += 16, j++)
            {
                this.cipherTextBlock[j] = new byte[16];
                Buffer.BlockCopy(temp, i, this.cipherTextBlock[j], 0, 16);
            }
        }

        /// <summary>
        /// 입력된 암호화된 byte[]값을 byte[]에 16바이트로 나누어서 블럭 카피 한다.
        /// </summary>
        /// <param name="cipherByteArray"></param>
        private void DecryptDivisionBlock(byte[] cipherByteArray)
        {
            int index = (cipherByteArray.Length - 1) / 16 + 1;
            this.cipherTextBlock = new byte[index][];
            for (int i = 0, j = 0; j < index; i += 16, j++)
            {
                this.cipherTextBlock[j] = new byte[16];
                Buffer.BlockCopy(cipherByteArray, i, this.cipherTextBlock[j], 0, 16);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// 문자열을 받아 암호화된 문자열을 반환한다.
        /// </summary>
        /// <param name="inputText">암호화할 문자열입니다.</param>
        /// <returns>암호화된 문자열입니다.</returns>
        public string EncryptToString(string inputString)
        {
            if (String.IsNullOrEmpty(inputString))
            {
                return inputString;
            }
            else
            {
                this.Encrypt(inputString);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < this.cipherTextBlock.Length; i++)
                {
                    sb.Append(BitConverter.ToString(this.cipherTextBlock[i]).Replace("-", ""));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 문자열을 받아 암호화된 byte[]을 반환한다.
        /// </summary>
        /// <param name="inputText">암호화할 문자열입니다.</param>
        /// <returns>암호화된 byte[]입니다.</returns>
        public byte[] EncryptToByteArray(string inputString)
        {
            if (String.IsNullOrEmpty(inputString))
            {
                return null;
            }
            else
            {
                this.Encrypt(inputString);

                byte[] retValue = new byte[this.cipherTextBlock.Length * 16];
                for (int i = 0, j = 0; i < this.cipherTextBlock.Length; i++, j += 16)
                {
                    Buffer.BlockCopy(this.cipherTextBlock[i], 0, retValue, j, 16);
                }
                return retValue;
            }
        }

        /// <summary>
        /// 암호화된 문자열을 받아 복호화한다.
        /// </summary>
        /// <param name="inputText">암호화된 문자열입니다.</param>
        /// <returns>복호화된 문자열입니다.</returns>
        public string DecryptFromString(string inputString)
        {
            byte[] dest = new byte[inputString.Length];
            if (String.IsNullOrEmpty(inputString))
            {
                return inputString;
            }
            else
            {
                this.Decrypt(inputString);

                for (int i = 0, j = 0; i < this.plainTextBlock.Length; i++, j += 16)
                {
                    Buffer.BlockCopy(this.plainTextBlock[i], 0, dest, j, 16);
                }
                return Encoding.Default.GetString(dest);
            }
        }

        /// <summary>
        /// 암호화된 byte[]을 받아 복호화한다.
        /// </summary>
        /// <param name="inputByteArray">암호화된 byte[]입니다.</param>
        /// <returns>복호화된 문자열입니다.</returns>
        public string DecryptFromByteArray(byte[] inputByteArray)
        {
            byte[] dest = new byte[inputByteArray.Length];
            if (inputByteArray == null || inputByteArray.Length == 0)
            {
                return String.Empty;
            }
            else
            {
                this.Decrypt(inputByteArray);

                for (int i = 0, j = 0; i < this.plainTextBlock.Length; i++, j += 16)
                {
                    Buffer.BlockCopy(this.plainTextBlock[i], 0, dest, j, 16);
                }
                return Encoding.Default.GetString(dest);
            }
        }
        #endregion
    }
}
