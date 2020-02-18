using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Value> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Value>()
                .HasData(
                new Value { Id = 1, Name = "value1" },
                new Value { Id = 2, Name = "value2" },
                new Value { Id = 3, Name = "value3" }
                );
        }
    }
}