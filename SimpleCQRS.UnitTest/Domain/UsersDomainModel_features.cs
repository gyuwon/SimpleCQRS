using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleCQRS.Messages;
using SimpleCQRS.Models.Data;
using SimpleCQRS.Models.Presentation;

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
            var userName = "foo";
            string password = random.Next().ToString();
            string passwordHash = random.Next().ToString();

            var repository = Mock.Of<IUsersRepository>();
            var passwordHasher = Mock.Of<IPasswordHasher>(
                x => x.HashPassword(password) == passwordHash);

            var sut = new UsersDomainModel(repository, passwordHasher);

            Mock.Get(repository)
                .Setup(x => x.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(new User());

            // Act
            await sut.CreateUserAsync(new CreateUser
            {
                UserName = userName,
                Password = password
            });

            // Assert
            Mock<IUsersRepository> mock = Mock.Get(repository);
            mock.Verify(repo =>
                repo.InsertAsync(It.Is<User>(user =>
                    user.UserName == userName &&
                    user.PasswordHash == passwordHash)));
        }

        [TestMethod]
        public async Task CreateUserAsync_returns_correct_presentation_object()
        {
            // Arrange
            var random = new Random();
            int userId = random.Next();
            var userName = "foo";
            string password = random.Next().ToString();
            string passwordHash = random.Next().ToString();

            var repository = Mock.Of<IUsersRepository>();
            var passwordHasher = Mock.Of<IPasswordHasher>(
                x => x.HashPassword(password) == passwordHash);

            var sut = new UsersDomainModel(repository, passwordHasher);

            Mock.Get(repository)
                .Setup(x => x.InsertAsync(
                    It.Is<User>(p =>
                        p.UserName == userName &&
                        p.PasswordHash == passwordHash)))
                .ReturnsAsync(userId);

            Mock.Get(repository)
                .Setup(x => x.FindAsync(userId))
                .ReturnsAsync(new User { Id = userId, UserName = userName });

            // Act
            UserPresentation actual = await sut.CreateUserAsync(new CreateUser
            {
                UserName = userName,
                Password = password
            });

            // Assert
            actual.ShouldBeEquivalentTo(
                new { Id = userId, UserName = userName });
        }
    }
}
