using System;
using Microsoft.AspNet.Identity;

namespace SimpleCQRS.Domain.Services
{
    public class AspNetPasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher _provider;

        public AspNetPasswordHasher(PasswordHasher provider)
        {
            if (null == provider)
                throw new ArgumentNullException(nameof(provider));

            _provider = provider;
        }

        public string HashPassword(string password) =>
            _provider.HashPassword(password);
    }
}
