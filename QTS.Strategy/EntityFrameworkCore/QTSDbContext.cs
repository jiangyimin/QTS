using Microsoft.EntityFrameworkCore;

namespace QTS.Strategy.EntityFramework
{
    public class QTSDbContext : DbContext
    {
        public DbSet<Strategy> Strategies { get; set; }
        public DbSet<Instrument> Instruments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=QtsDb;Trusted_Connection=True;");
        }
    }
}
