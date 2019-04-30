using Microsoft.EntityFrameworkCore;
using WsTestes.Model;

namespace WsTestes.Repo
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }
    }
}
