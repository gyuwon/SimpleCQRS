using System;

namespace SimpleCQRS.Messages
{
    public class CreateUser
    {
        public Guid UserId { get; } = Guid.NewGuid();

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
