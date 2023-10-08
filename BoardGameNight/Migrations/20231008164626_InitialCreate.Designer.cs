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
    [Migration("20231008164626_InitialCreate")]
    partial class InitialCreate
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Foto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is18Plus")
                        .HasColumnType("bit");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoortSpel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumTijd")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Is18Plus")
                        .HasColumnType("bit");

                    b.Property<int>("MaxAantalSpelers")
                        .HasColumnType("int");

                    b.Property<int>("OrganisatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganisatorId");

                    b.ToTable("Bordspellenavond", (string)null);
                });

            modelBuilder.Entity("BoardGameNight.Models.Eten", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BordspellenavondId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAlcoholvrij")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLactosevrij")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNotenvrij")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVegetarisch")
                        .HasColumnType("bit");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BordspellenavondId");

                    b.ToTable("Eten", (string)null);
                });

            modelBuilder.Entity("BoardGameNight.Models.Persoon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergieën")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dieetwensen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Geboortedatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Geslacht")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persoon", (string)null);
                });

            modelBuilder.Entity("BoardGameNight.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BordspellenavondId")
                        .HasColumnType("int");

                    b.Property<int>("PersoonId")
                        .HasColumnType("int");

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

                    b.Property<int>("PersoonId")
                        .HasColumnType("int");

                    b.HasKey("BordspellenavondId", "PersoonId");

                    b.HasIndex("PersoonId");

                    b.ToTable("BordspellenavondPersoon");
                });

            modelBuilder.Entity("BoardGameNight.Models.Bordspellenavond", b =>
                {
                    b.HasOne("BoardGameNight.Models.Persoon", "Organisator")
                        .WithMany("GeorganiseerdeAvonden")
                        .HasForeignKey("OrganisatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisator");
                });

            modelBuilder.Entity("BoardGameNight.Models.Eten", b =>
                {
                    b.HasOne("BoardGameNight.Models.Bordspellenavond", "Bordspellenavond")
                        .WithMany("Eten")
                        .HasForeignKey("BordspellenavondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bordspellenavond");
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

            modelBuilder.Entity("BoardGameNight.Models.Bordspellenavond", b =>
                {
                    b.Navigation("Eten");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BoardGameNight.Models.Persoon", b =>
                {
                    b.Navigation("GeorganiseerdeAvonden");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
