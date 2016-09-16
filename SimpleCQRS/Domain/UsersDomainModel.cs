using System;
using System.Threading.Tasks;
using SimpleCQRS.Messages;
using SimpleCQRS.Models.Data;

namespace SimpleCQRS.Domain
{
    public class UsersDomainModel
    {
        private readonly IUsersRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public UsersDomainModel(
            IUsersRepository repository,
            IPasswordHasher passwordHasher)
        {
            if (null == repository)
                throw new ArgumentNullException(nameof(repository));
            if (null == passwordHasher)
                throw new ArgumentNullException(nameof(passwordHasher));

            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task CreateUserAsync(CreateUser command)
        {
            if (null == command)
                throw new ArgumentNullException(nameof(command));

            await _repository.InsertAsync(new User
            {
                Id = command.UserId,
                UserName = command.UserName,
                PasswordHash = _passwordHasher.HashPassword(command.Password)
            });
        }
    }
}
