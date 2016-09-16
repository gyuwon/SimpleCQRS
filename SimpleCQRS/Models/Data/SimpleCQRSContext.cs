using System.Data.Entity;

namespace SimpleCQRS.Models.Data
{
    public class SimpleCQRSContext : DbContext
    {
        public SimpleCQRSContext() : base("name=SimpleCQRSContext")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
