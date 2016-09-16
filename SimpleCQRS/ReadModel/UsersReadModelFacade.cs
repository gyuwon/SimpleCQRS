using System;
using System.Data.Entity;
using System.Threading.Tasks;
using SimpleCQRS.Models.Data;
using SimpleCQRS.Models.Presentation;

namespace SimpleCQRS.ReadModel
{
    public class UsersReadModelFacade
    {
        private readonly Func<SimpleCQRSContext> _dbContextFactory;

        public UsersReadModelFacade(Func<SimpleCQRSContext> dbContextFactory)
        {
            if (null == dbContextFactory)
                throw new ArgumentNullException(nameof(dbContextFactory));

            _dbContextFactory = dbContextFactory;
        }

        public async Task<UserPresentation> FindByIdAsync(Guid userId)
        {
            using (SimpleCQRSContext db = _dbContextFactory.Invoke())
            {
                return await db.Users
                    .FilterById(userId)
                    .ToPresentation()
                    .SingleOrDefaultAsync();
            }
        }
    }
}
