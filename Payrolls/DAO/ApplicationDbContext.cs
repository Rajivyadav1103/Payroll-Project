using Microsoft.EntityFrameworkCore;
using Payrolls.Models;

namespace Payrolls.DAO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
        }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<Allowance> Allowances { get; set; }
    }
}
