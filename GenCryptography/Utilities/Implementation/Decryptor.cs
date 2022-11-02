using GenCryptography.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Service.Utilities.Implementation
{
    public class Decryptor : IDecryptor
    {
        public string Decrypt(byte[] key, string input)
        {
            try
            {
                byte[] inputArray = Convert.FromBase64String(input.Replace(" ", "+"));

                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider())
                {
                    tripleDES.Key = key;
                    tripleDES.Mode = CipherMode.ECB;
                    tripleDES.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                    tripleDES.Clear();
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
