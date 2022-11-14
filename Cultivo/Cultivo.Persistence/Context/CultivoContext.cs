using Cultivo.Domain.Models;
using Cultivo.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Cultivo.Persistence.Context
{
    public class CultivoContext : DbContext
    {
        protected DbSet<User> User { get; set; }

        public CultivoContext(DbContextOptions<CultivoContext> options) : base(options)
        {
            // ---
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMapper().Configure);
        }
    }
}
