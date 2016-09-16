using System;
using System.Threading.Tasks;
using SimpleCQRS.Models.Data;

namespace SimpleCQRS.Domain
{
    public interface IUsersRepository
    {
        Task InsertAsync(User user);

        Task<User> FindAsync(Guid userId);
    }
}
