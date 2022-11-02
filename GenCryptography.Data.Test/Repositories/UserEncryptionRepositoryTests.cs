using GenCryptography.Data.Models;
using GenCryptography.Data.Repositories.Interface;
using GenCryptography.Data.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Data.Test.Repositories
{
    public class UserEncryptionRepositoryTests
    {
        private IUserEncryptionRepository _repo;

        public UserEncryptionRepositoryTests(IUserEncryptionRepository repo)
        { 
            _repo = repo;
        }

        private IEnumerable<User> GetUsers()
        {
            return new List<User>() {
                new User() {
                    UserId = 1,
                    Username = "MalavdeAtal",
                    Email = "atalmalavdework@gmail.com",
                    Phone = "+61450032640",
                    LastActive = DateTime.UtcNow,
                }
            };
        }

        [Fact]
        public void GetTest()
        {
            UserEncryption userEncryption = _repo.Get(1);

            Assert.NotNull(userEncryption);

            Assert.IsType<UserEncryption>(userEncryption);
        }
    }
}
