using Microsoft.EntityFrameworkCore;

namespace MeetupGraphQLNet.Models
{
    public class MeetupGraphQLNetContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        public MeetupGraphQLNetContext(DbContextOptions<MeetupGraphQLNetContext> options) :
           base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().HasKey(c => c.Id);
        }
    }
}
