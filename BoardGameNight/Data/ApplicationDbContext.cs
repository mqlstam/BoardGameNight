using BoardGameNight.Models;
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
    

    // Relatie tussen Persoon en Bordspellenavond (Organisator)
    modelBuilder.Entity<Bordspellenavond>()
        .HasOne(b => b.Organisator)
        .WithMany(p => p.GeorganiseerdeAvonden)
        .HasForeignKey(b => b.OrganisatorId);

// Many-to-Many relatie tussen Persoon en Bordspellenavond (Deelnemers)
    modelBuilder.Entity<Bordspellenavond>()
        .HasMany(b => b.Deelnemers)
        .WithMany(p => p.DeelgenomenAvonden)
        .UsingEntity<Dictionary<string, object>>(
            "BordspellenavondPersoon",
            j => j
                .HasOne<Persoon>()
                .WithMany()
                .HasForeignKey("PersoonId")
                .OnDelete(DeleteBehavior.NoAction), // Verander van Cascade naar NoAction
            j => j
                .HasOne<Bordspellenavond>()
                .WithMany()
                .HasForeignKey("BordspellenavondId")
                .OnDelete(DeleteBehavior.NoAction) // Verander van Cascade naar NoAction
        );

    // Many-to-Many relatie tussen Bordspellenavond en Bordspel
    modelBuilder.Entity<Bordspellenavond>()
        .HasMany(b => b.Bordspellen)
        .WithMany(s => s.Bordspellenavonden)
        .UsingEntity<Dictionary<string, object>>(
            "BordspellenavondBordspel",
            j => j
                .HasOne<Bordspel>()
                .WithMany()
                .HasForeignKey("BordspelId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<Bordspellenavond>()
                .WithMany()
                .HasForeignKey("BordspellenavondId")
                .OnDelete(DeleteBehavior.Cascade)
        );

    // One-to-Many relatie tussen Bordspellenavond en Review
    modelBuilder.Entity<Bordspellenavond>()
        .HasMany(b => b.Reviews)
        .WithOne(r => r.Bordspellenavond)
        .HasForeignKey(r => r.BordspellenavondId)
        .OnDelete(DeleteBehavior.NoAction);

    // One-to-Many relatie tussen Persoon en Review
    modelBuilder.Entity<Persoon>()
        .HasMany(p => p.Reviews)
        .WithOne(r => r.Persoon)
        .HasForeignKey(r => r.PersoonId)
        .OnDelete(DeleteBehavior.NoAction);

    // One-to-Many relatie tussen Bordspellenavond en Eten
    modelBuilder.Entity<Bordspellenavond>()
        .HasMany(b => b.Eten)
        .WithOne(e => e.Bordspellenavond)
        .HasForeignKey(e => e.BordspellenavondId);
}  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:boardgamedatabase.database.windows.net,1433;Initial Catalog=BoardGameDatabase;Persist Security Info=False;User ID=lemigie;Password=Mikzakker1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
    
    
}