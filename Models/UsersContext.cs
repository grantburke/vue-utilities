using Microsoft.EntityFrameworkCore;

namespace vue_utilities.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() { }
        
        public UsersContext(DbContextOptions options) : base(options) { }
        
        public virtual DbSet<User> Users { get; set; }
    }
}