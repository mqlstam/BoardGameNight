using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Bordspel> Bordspellen { get; set; }
    public DbSet<Bordspellenavond> Bordspellenavonden { get; set; }
    public DbSet<Eten> Eten { get; set; }
    public DbSet<Persoon> Personen { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bordspel>().ToTable("Bordspel");
        modelBuilder.Entity<Bordspellenavond>().ToTable("Bordspellenavond");
        modelBuilder.Entity<Eten>().ToTable("Eten");
        modelBuilder.Entity<Persoon>().ToTable("Persoon");
        modelBuilder.Entity<Review>().ToTable("Review");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:boardgamedatabase.database.windows.net,1433;Initial Catalog=BoardGameDatabase;Persist Security Info=False;User ID=lemigie;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
    
    
}