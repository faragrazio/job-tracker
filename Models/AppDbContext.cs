using Microsoft.EntityFrameworkCore;

namespace JobTracker.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Candidatura> Candidature { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidatura>(entity =>
            {
                entity.ToTable("candidature");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Stato).HasDefaultValue("Inviata");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}