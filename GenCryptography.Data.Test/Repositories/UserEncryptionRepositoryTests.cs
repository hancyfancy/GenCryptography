using Autofac.Extras.Moq;
using Dapper;
using GenCryptography.Data.Models;
using GenCryptography.Data.Repositories.Implementation;
using GenCryptography.Data.Repositories.Interface;
using GenCryptography.Data.Test.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Data.Test.Repositories
{
    public class UserEncryptionRepositoryTests
    {
        private IUserEncryptionRepository _repo;
        private readonly string _connectionString;

        public UserEncryptionRepositoryTests()
        {
            _connectionString = "Data Source=localhost;Initial Catalog=CwRetail;Persist Security Info=true;User ID=TestLogin; Password = ABC123";
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<IUserEncryptionRepository>(s => new UserEncryptionRepository(_connectionString));
            ServiceProvider provider = services.BuildServiceProvider();
            _repo = provider.GetService<IUserEncryptionRepository>();
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

        private IEnumerable<UserEncryption> GetUserEncryptions()
        {
            return new List<UserEncryption>() {
                new UserEncryption() {
                    UserEncryptionId = 1,
                    UserId = 1,
                    EncryptionKey = new byte [] { },
                }
            };
        }

        [Fact]
        public void GetTest()
        {
            User user = GetUsers().FirstOrDefault();

            Assert.NotNull(user);

            using (var mock = AutoMock.GetLoose())
            {
                var mocked = mock.Mock<IUserEncryptionRepository>();

                mocked
                    .Setup(x => x.Get(user.UserId))
                    .Returns(GetUserEncryptions().FirstOrDefault());

                var repo = mock.Create<IUserEncryptionRepository>();

                var expected = GetUserEncryptions().FirstOrDefault();
                var actual = repo.Get(user.UserId);

                Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
            }
        }
    }
}
