using System;
using System.Threading.Tasks;
using SimpleCQRS.Messages;
using SimpleCQRS.Models.Data;
using SimpleCQRS.Models.Presentation;

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

        public async Task<UserPresentation> CreateUserAsync(CreateUser command)
        {
            if (null == command)
                throw new ArgumentNullException(nameof(command));

            int userId = await _repository.InsertAsync(new User
            {
                UserName = command.UserName,
                PasswordHash = _passwordHasher.HashPassword(command.Password)
            });

            User user = await _repository.FindAsync(userId);

            return new UserPresentation
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
