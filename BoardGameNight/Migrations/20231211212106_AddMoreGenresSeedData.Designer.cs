﻿// <auto-generated />
using System;
using BoardGameNight.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BoardGameNight.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231211212106_AddMoreGenresSeedData")]
    partial class AddMoreGenresSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BoardGameNight.Models.Bordspel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("FotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<bool>("Is18Plus")
                        .HasColumnType("bit");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SoortSpelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("SoortSpelId");

                    b.ToTable("Bordspel", (string)null);
                });

            modelBuilder.Entity("BoardGameNight.Models.Bordspellenavond", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DatumTijd")
                        .HasColumnType("datetime2");

                    b.Property<int>("Dieetwensen")
                        .HasColumnType("int");

                    b.Property<int>("DrankVoorkeur")
                        .HasColumnType("int");

                    b.Property<bool>("Is18Plus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPotluck")
                        .HasColumnType("bit");

                    b.Property<int>("MaxAantalSpelers")
                        .HasColumnType("int");

                    b.Property<string>("OrganisatorId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OrganisatorId");

                    b.ToTable("Bordspellenavond", (string)null);
                });

            modelBuilder.Entity("BoardGameNight.Models.Persoon", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dieetwensen")
                        .HasColumnType("int");

                    b.Property<int>("DrankVoorkeur")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Geboortedatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Geslacht")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BoardGameNight.Models.PotluckItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BordspellenavondId")
                        .HasColumnType("int");

                    b.Property<int?>("Dieetwensen")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ParticipantId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BordspellenavondId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("PotluckItem");
                });

            modelBuilder.Entity("BoardGameNight.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BordspellenavondId")
                        .HasColumnType("int");

                    b.Property<string>("PersoonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Tekst")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BordspellenavondId");

                    b.HasIndex("PersoonId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("BordspelGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("BordspelGenres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Naam = "Strategie"
                        },
                        new
                        {
                            Id = 2,
                            Naam = "Familie"
                        },
                        new
                        {
                            Id = 3,
                            Naam = "Avontuur"
                        },
                        new
                        {
                            Id = 4,
                            Naam = "Kaartspel"
                        },
                        new
                        {
                            Id = 5,
                            Naam = "Dobbelspel"
                        },
                        new
                        {
                            Id = 6,
                            Naam = "Educatief"
                        },
                        new
                        {
                            Id = 7,
                            Naam = "Fantasie"
                        },
                        new
                        {
                            Id = 8,
                            Naam = "Party"
                        },
                        new
                        {
                            Id = 9,
                            Naam = "Puzzel"
                        },
                        new
                        {
                            Id = 10,
                            Naam = "Sport"
                        });
                });

            modelBuilder.Entity("BordspellenavondBordspel", b =>
                {
                    b.Property<int>("BordspelId")
                        .HasColumnType("int");

                    b.Property<int>("BordspellenavondId")
                        .HasColumnType("int");

                    b.HasKey("BordspelId", "BordspellenavondId");

                    b.HasIndex("BordspellenavondId");

                    b.ToTable("BordspellenavondBordspel");
                });

            modelBuilder.Entity("BordspellenavondPersoon", b =>
                {
                    b.Property<int>("BordspellenavondId")
                        .HasColumnType("int");

                    b.Property<string>("PersoonId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BordspellenavondId", "PersoonId");

                    b.HasIndex("PersoonId");

                    b.ToTable("BordspellenavondPersoon");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SoortBordspel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SoortBordspellen");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Naam = "Abstract Spel"
                        },
                        new
                        {
                            Id = 2,
                            Naam = "Thematisch Spel"
                        },
                        new
                        {
                            Id = 3,
                            Naam = "Strategiespel"
                        },
                        new
                        {
                            Id = 4,
                            Naam = "Familiespel"
                        },
                        new
                        {
                            Id = 5,
                            Naam = "Kinderspel"
                        },
                        new
                        {
                            Id = 6,
                            Naam = "Partyspel"
                        },
                        new
                        {
                            Id = 7,
                            Naam = "Kaartspel"
                        },
                        new
                        {
                            Id = 8,
                            Naam = "Dobbelspel"
                        },
                        new
                        {
                            Id = 9,
                            Naam = "Coöperatief Spel"
                        },
                        new
                        {
                            Id = 10,
                            Naam = "Solo Spel"
                        });
                });

            modelBuilder.Entity("BoardGameNight.Models.Bordspel", b =>
                {
                    b.HasOne("BordspelGenre", "Genre")
                        .WithMany("Bordspellen")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoortBordspel", "SoortSpel")
                        .WithMany("Bordspellen")
                        .HasForeignKey("SoortSpelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("SoortSpel");
                });

            modelBuilder.Entity("BoardGameNight.Models.Bordspellenavond", b =>
                {
                    b.HasOne("BoardGameNight.Models.Persoon", "Organisator")
                        .WithMany("GeorganiseerdeAvonden")
                        .HasForeignKey("OrganisatorId");

                    b.Navigation("Organisator");
                });

            modelBuilder.Entity("BoardGameNight.Models.PotluckItem", b =>
                {
                    b.HasOne("BoardGameNight.Models.Bordspellenavond", "Bordspellenavond")
                        .WithMany("PotluckItems")
                        .HasForeignKey("BordspellenavondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoardGameNight.Models.Persoon", "Participant")
                        .WithMany("ContributedPotluckItems")
                        .HasForeignKey("ParticipantId");

                    b.Navigation("Bordspellenavond");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("BoardGameNight.Models.Review", b =>
                {
                    b.HasOne("BoardGameNight.Models.Bordspellenavond", "Bordspellenavond")
                        .WithMany("Reviews")
                        .HasForeignKey("BordspellenavondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BoardGameNight.Models.Persoon", "Persoon")
                        .WithMany("Reviews")
                        .HasForeignKey("PersoonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Bordspellenavond");

                    b.Navigation("Persoon");
                });

            modelBuilder.Entity("BordspellenavondBordspel", b =>
                {
                    b.HasOne("BoardGameNight.Models.Bordspel", null)
                        .WithMany()
                        .HasForeignKey("BordspelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoardGameNight.Models.Bordspellenavond", null)
                        .WithMany()
                        .HasForeignKey("BordspellenavondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BordspellenavondPersoon", b =>
                {
                    b.HasOne("BoardGameNight.Models.Bordspellenavond", null)
                        .WithMany()
                        .HasForeignKey("BordspellenavondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BoardGameNight.Models.Persoon", null)
                        .WithMany()
                        .HasForeignKey("PersoonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BoardGameNight.Models.Persoon", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BoardGameNight.Models.Persoon", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoardGameNight.Models.Persoon", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BoardGameNight.Models.Persoon", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoardGameNight.Models.Bordspellenavond", b =>
                {
                    b.Navigation("PotluckItems");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BoardGameNight.Models.Persoon", b =>
                {
                    b.Navigation("ContributedPotluckItems");

                    b.Navigation("GeorganiseerdeAvonden");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BordspelGenre", b =>
                {
                    b.Navigation("Bordspellen");
                });

            modelBuilder.Entity("SoortBordspel", b =>
                {
                    b.Navigation("Bordspellen");
                });
#pragma warning restore 612, 618
        }
    }
}
