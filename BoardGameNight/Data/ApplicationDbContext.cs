using BoardGameNight.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Data
{
    public class ApplicationDbContext : IdentityDbContext<Persoon>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Bordspel> Bordspellen { get; set; }
        public DbSet<Bordspellenavond> Bordspellenavonden { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BordspelGenre> BordspelGenres { get; set; }
        public DbSet<SoortBordspel> SoortBordspellen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to be first, as it creates all ASP.NET Core Identity tables with their relationships.

            modelBuilder.Entity<Bordspel>().ToTable("Bordspel");
            modelBuilder.Entity<Bordspellenavond>().ToTable("Bordspellenavond");
            modelBuilder.Entity<Review>().ToTable("Review");
            
            modelBuilder.Entity<BordspelGenre>().HasData(
                new BordspelGenre { Id = 1, Naam = "Strategie" },
                new BordspelGenre { Id = 2, Naam = "Familie" },
                new BordspelGenre { Id = 3, Naam = "Avontuur" },
                new BordspelGenre { Id = 4, Naam = "Kaartspel" },
                new BordspelGenre { Id = 5, Naam = "Dobbelspel" },
                new BordspelGenre { Id = 6, Naam = "Educatief" },
                new BordspelGenre { Id = 7, Naam = "Fantasie" },
                new BordspelGenre { Id = 8, Naam = "Party" },
                new BordspelGenre { Id = 9, Naam = "Puzzel" },
                new BordspelGenre { Id = 10, Naam = "Sport" }
                // Voeg eventueel nog meer genres toe
            );

            
            modelBuilder.Entity<SoortBordspel>().HasData(
                new SoortBordspel { Id = 1, Naam = "Abstract Spel" },
                new SoortBordspel { Id = 2, Naam = "Thematisch Spel" },
                new SoortBordspel { Id = 3, Naam = "Strategiespel" },
                new SoortBordspel { Id = 4, Naam = "Familiespel" },
                new SoortBordspel { Id = 5, Naam = "Kinderspel" },
                new SoortBordspel { Id = 6, Naam = "Partyspel" },
                new SoortBordspel { Id = 7, Naam = "Kaartspel" },
                new SoortBordspel { Id = 8, Naam = "Dobbelspel" },
                new SoortBordspel { Id = 9, Naam = "Coöperatief Spel" },
                new SoortBordspel { Id = 10, Naam = "Solo Spel" }
                // Voeg meer soorten toe zoals gewenst
            );


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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }



    }
}