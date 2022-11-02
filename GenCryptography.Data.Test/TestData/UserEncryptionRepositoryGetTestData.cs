using GenCryptography.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Data.Test.TestData
{
    public class UserEncryptionRepositoryGetTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { null, 0 };
            yield return new object[] { new UserEncryption() { UserEncryptionId = 1, UserId = 1, EncryptionKey = new byte[] { } } , 1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
