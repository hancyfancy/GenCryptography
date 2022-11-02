using GenCryptography.Service.Utilities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GenCryptography.Service.Utilities.Implementation
{
    public class KeyGenerator : IKeyGenerator
    {
        public byte[] GenerateEncryptionKey()
        {
            try
            {
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider())
                {
                    tripleDES.GenerateKey();
                    return tripleDES.Key;
                }
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
