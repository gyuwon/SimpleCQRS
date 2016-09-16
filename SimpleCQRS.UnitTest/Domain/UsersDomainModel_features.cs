using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleCQRS.Messages;
using SimpleCQRS.Models.Data;

namespace SimpleCQRS.Domain
{
    [TestClass]
    public class UsersDomainModel_features
    {
        [TestMethod]
        public async Task CreateUserAsync_inserts_new_user_to_repository()
        {
            // Arrange
            var random = new Random();
            var command = new CreateUser
            {
                UserName = "foo",
                Password = random.Next().ToString()
            };
            string passwordHash = random.Next().ToString();

            var repository = Mock.Of<IUsersRepository>();
            var passwordHasher = Mock.Of<IPasswordHasher>(
                x => x.HashPassword(command.Password) == passwordHash);

            var sut = new UsersDomainModel(repository, passwordHasher);

            // Act
            await sut.CreateUserAsync(command);

            // Assert
            Mock<IUsersRepository> mock = Mock.Get(repository);
            mock.Verify(repo =>
                repo.InsertAsync(It.Is<User>(user =>
                    user.Id == command.UserId &&
                    user.UserName == command.UserName &&
                    user.PasswordHash == passwordHash)));
        }
    }
}
