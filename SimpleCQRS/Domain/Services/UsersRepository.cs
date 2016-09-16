using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SimpleCQRS.Models.Data;

namespace SimpleCQRS.Domain.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly Func<SimpleCQRSContext> _dbContextFactory;

        public UsersRepository(Func<SimpleCQRSContext> dbContextFactory)
        {
            if (null == dbContextFactory)
                throw new ArgumentNullException(nameof(dbContextFactory));

            _dbContextFactory = dbContextFactory;
        }

        public async Task<User> FindAsync(int userId)
        {
            using (SimpleCQRSContext db = _dbContextFactory.Invoke())
            {
                return await db.Users
                    .Where(x => x.Id == userId)
                    .SingleOrDefaultAsync();
            }
        }

        public async Task<int> InsertAsync(User user)
        {
            if (null == user)
                throw new ArgumentNullException(nameof(user));

            using (SimpleCQRSContext db = _dbContextFactory.Invoke())
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return user.Id;
            }
        }
    }
}
