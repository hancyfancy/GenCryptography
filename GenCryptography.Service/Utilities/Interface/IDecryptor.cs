using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Service.Utilities.Interface
{
    public interface IDecryptor
    {
        string Decrypt(byte[] key, string input);
    }
}
