using Microsoft.EntityFrameworkCore;
using TaskWebApplication.Models;

namespace TaskWebApplication.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
        {
            
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>().HasKey(t => t.id);
        }*/


        public DbSet<Tasks> TasksProperty { get; set; }
    }
}
