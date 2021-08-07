using App.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Database.Context
{
    public class DataContext : DbContext
    {

        public DbSet<Users> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                      
        }
    }
}
