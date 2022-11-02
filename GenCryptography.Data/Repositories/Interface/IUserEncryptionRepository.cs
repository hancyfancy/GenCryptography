using GenCryptography.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Data.Repositories.Interface
{
    public interface IUserEncryptionRepository
    {
        byte[] InsertOrUpdate(long userId, byte[] encryptionKey);

        UserEncryption Get(long userId);

        void SetConnectionString(string connectionString);
    }
}
