using GenCryptography.Service.Utilities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Service.Utilities.Implementation
{
    public class Encryptor : IEncryptor
    {
        public string Encrypt(byte[] key, string input)
        {
            try
            {
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);

                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider())
                {
                    tripleDES.Key = key;
                    tripleDES.Mode = CipherMode.ECB;
                    tripleDES.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                    tripleDES.Clear();
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
