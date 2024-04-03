using GoSave.Models;
using Microsoft.EntityFrameworkCore;

namespace GoSave.Context
{
    public class GoSaveDbContext : DbContext
    {

        public GoSaveDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Identity> Identity { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Vault> Vaults { get; set; }
    }
}
