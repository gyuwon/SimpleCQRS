using System.Threading.Tasks;
using SimpleCQRS.Models.Data;

namespace SimpleCQRS.Domain
{
    public interface IUsersRepository
    {
        Task<int> InsertAsync(User user);

        Task<User> FindAsync(int userId);
    }
}
