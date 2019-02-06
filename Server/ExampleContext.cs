using Microsoft.EntityFrameworkCore;

namespace Server
{
    public class ExampleContext : DbContext
    {
        public DbSet<Text> Texts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Text>()
                .HasAlternateKey(t => t.TextContent)
                .HasName("AlternateKey_TextContent");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=127.0.0.1;port=5432;database=assessmentdayexample;userid=postgres;password=bondstone");
        }
    }
}