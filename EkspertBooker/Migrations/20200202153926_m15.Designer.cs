﻿// <auto-generated />
using System;
using EkspertBooker.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EkspertBooker.WebAPI.Migrations
{
    [DbContext(typeof(EkspertBookerContext))]
    [Migration("20200202153926_m15")]
    partial class m15
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Ekspert", b =>
                {
                    b.Property<int>("EkspertId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojRecenzija")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("BrojZavrsenihProjekata")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int?>("KorisnikId");

                    b.Property<int>("KorisnikUlogaId");

                    b.Property<float?>("ProsjecnaOcjena");

                    b.HasKey("EkspertId");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("KorisnikUlogaId");

                    b.ToTable("Eksperti");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Kategorija", b =>
                {
                    b.Property<int>("KategorijaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.HasKey("KategorijaId");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LozinkaHash")
                        .IsRequired();

                    b.Property<string>("LozinkaSalt")
                        .IsRequired();

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Telefon")
                        .HasMaxLength(50);

                    b.HasKey("KorisnikId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("KorisnickoIme")
                        .IsUnique();

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.KorisnikKategorija", b =>
                {
                    b.Property<int>("KorisnikKategorijaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KategorijaId");

                    b.Property<int>("KorisnikId");

                    b.HasKey("KorisnikKategorijaId");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("KorisniciKategorije");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.KorisnikSlika", b =>
                {
                    b.Property<int>("KorisnikSlikaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KorisnikId");

                    b.Property<byte[]>("ProfilnaSlika");

                    b.Property<string>("SlikaNaziv");

                    b.HasKey("KorisnikSlikaId");

                    b.HasIndex("KorisnikId")
                        .IsUnique();

                    b.ToTable("KorisniciSlike");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.KorisnikUloga", b =>
                {
                    b.Property<int>("KorisnikUlogaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KorisnikId");

                    b.Property<int>("UlogaId");

                    b.HasKey("KorisnikUlogaId");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("UlogaId");

                    b.ToTable("KorisniciUloge");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Ponuda", b =>
                {
                    b.Property<int>("PonudaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Cijena");

                    b.Property<int>("EkspertId");

                    b.Property<string>("OpisPonude");

                    b.Property<int>("ProjektId");

                    b.Property<bool>("Status");

                    b.Property<DateTime>("VrijemePonude");

                    b.HasKey("PonudaId");

                    b.HasIndex("EkspertId");

                    b.HasIndex("ProjektId");

                    b.ToTable("Ponude");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Poslodavac", b =>
                {
                    b.Property<int>("PoslodavacId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojRecenzija")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("BrojZavrsenihProjekata")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int?>("KorisnikId");

                    b.Property<int>("KorisnikUlogaId");

                    b.Property<float?>("ProsjecnaOcjena");

                    b.HasKey("PoslodavacId");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("KorisnikUlogaId");

                    b.ToTable("Poslodavci");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Projekt", b =>
                {
                    b.Property<int>("ProjektId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Budzet");

                    b.Property<DateTime?>("DatumObjave");

                    b.Property<DateTime?>("DatumPocetka");

                    b.Property<DateTime?>("DatumZavrsetka");

                    b.Property<string>("DetaljniOpis");

                    b.Property<int?>("EkspertId");

                    b.Property<bool?>("Hitan");

                    b.Property<int>("KategorijaId");

                    b.Property<string>("KratkiOpis")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("PoslodavacId");

                    b.Property<string>("StanjeId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("TrajanjeDana");

                    b.HasKey("ProjektId");

                    b.HasIndex("EkspertId");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("PoslodavacId");

                    b.HasIndex("StanjeId");

                    b.ToTable("Projekti");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.ProjektDetalji", b =>
                {
                    b.Property<int>("ProjektDetaljiId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AktivanDetaljanOpis");

                    b.Property<string>("Napomena");

                    b.Property<int>("ProjektId");

                    b.HasKey("ProjektDetaljiId");

                    b.HasIndex("ProjektId")
                        .IsUnique();

                    b.ToTable("ProjektDetalji");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.ProjektDetaljiPrilog", b =>
                {
                    b.Property<int>("ProjektDetaljiPrilogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Prilog");

                    b.Property<string>("PrilogNaziv");

                    b.Property<int>("ProjektDetaljiId");

                    b.HasKey("ProjektDetaljiPrilogId");

                    b.HasIndex("ProjektDetaljiId")
                        .IsUnique();

                    b.ToTable("ProjektDetaljiPrilozi");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.RecenzijaOEkspert", b =>
                {
                    b.Property<int>("RecenzijaOEkspertId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumRecenzije");

                    b.Property<int>("EkspertId");

                    b.Property<string>("Komentar");

                    b.Property<int>("Ocjena");

                    b.Property<int>("PoslodavacId");

                    b.Property<int>("ProjektId");

                    b.HasKey("RecenzijaOEkspertId");

                    b.HasIndex("EkspertId");

                    b.HasIndex("PoslodavacId");

                    b.HasIndex("ProjektId")
                        .IsUnique();

                    b.ToTable("RecenzijeOEksperti");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.RecenzijaOPoslodavac", b =>
                {
                    b.Property<int>("RecenzijaOPoslodavacId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumRecenzije");

                    b.Property<int>("EkspertId");

                    b.Property<string>("Komentar");

                    b.Property<int>("Ocjena");

                    b.Property<int>("PoslodavacId");

                    b.Property<int>("ProjektId");

                    b.HasKey("RecenzijaOPoslodavacId");

                    b.HasIndex("EkspertId");

                    b.HasIndex("PoslodavacId");

                    b.HasIndex("ProjektId")
                        .IsUnique();

                    b.ToTable("RecenzijeOPoslodavci");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Stanje", b =>
                {
                    b.Property<string>("StanjeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.HasKey("StanjeId");

                    b.ToTable("Stanja");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Uloga", b =>
                {
                    b.Property<int>("UlogaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("UlogaId");

                    b.ToTable("Uloge");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Ekspert", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId");

                    b.HasOne("EkspertBooker.WebAPI.Database.KorisnikUloga", "KorisnikUloga")
                        .WithMany()
                        .HasForeignKey("KorisnikUlogaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.KorisnikKategorija", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Kategorija", "Kategorija")
                        .WithMany("KategorijaKorisnici")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EkspertBooker.WebAPI.Database.Korisnik", "Korisnik")
                        .WithMany("KorisnikKategorije")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.KorisnikSlika", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Korisnik", "Korisnik")
                        .WithOne("KorisnikSlika")
                        .HasForeignKey("EkspertBooker.WebAPI.Database.KorisnikSlika", "KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.KorisnikUloga", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Korisnik", "Korisnik")
                        .WithMany("KorisnikUloge")
                        .HasForeignKey("KorisnikId")
                        .HasConstraintName("FK_Korisnici_KorisniciUloge")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EkspertBooker.WebAPI.Database.Uloga", "Uloga")
                        .WithMany("KorisniciUloga")
                        .HasForeignKey("UlogaId")
                        .HasConstraintName("FK_Uloge_KorisniciUloge")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Ponuda", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Ekspert", "Ekspert")
                        .WithMany("Ponude")
                        .HasForeignKey("EkspertId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EkspertBooker.WebAPI.Database.Projekt", "Projekt")
                        .WithMany("Ponude")
                        .HasForeignKey("ProjektId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Poslodavac", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId");

                    b.HasOne("EkspertBooker.WebAPI.Database.KorisnikUloga", "KorisnikUloga")
                        .WithMany()
                        .HasForeignKey("KorisnikUlogaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.Projekt", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Ekspert", "Ekspert")
                        .WithMany("Projekti")
                        .HasForeignKey("EkspertId");

                    b.HasOne("EkspertBooker.WebAPI.Database.Kategorija", "Kategorija")
                        .WithMany("Projekti")
                        .HasForeignKey("KategorijaId")
                        .HasConstraintName("FK_Projekti_Kategorije");

                    b.HasOne("EkspertBooker.WebAPI.Database.Poslodavac", "Poslodavac")
                        .WithMany("Projekti")
                        .HasForeignKey("PoslodavacId");

                    b.HasOne("EkspertBooker.WebAPI.Database.Stanje", "Stanje")
                        .WithMany("Projekti")
                        .HasForeignKey("StanjeId")
                        .HasConstraintName("FK_Projekti_Stanja");
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.ProjektDetalji", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Projekt", "Projekt")
                        .WithOne("ProjektDetalji")
                        .HasForeignKey("EkspertBooker.WebAPI.Database.ProjektDetalji", "ProjektId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.ProjektDetaljiPrilog", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.ProjektDetalji", "ProjektDetalji")
                        .WithOne("ProjektDetaljiPrilog")
                        .HasForeignKey("EkspertBooker.WebAPI.Database.ProjektDetaljiPrilog", "ProjektDetaljiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.RecenzijaOEkspert", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Ekspert", "Ekspert")
                        .WithMany("RecenzijeOEksperti")
                        .HasForeignKey("EkspertId");

                    b.HasOne("EkspertBooker.WebAPI.Database.Poslodavac", "Poslodavac")
                        .WithMany("RecenzijeOEksperti")
                        .HasForeignKey("PoslodavacId");

                    b.HasOne("EkspertBooker.WebAPI.Database.Projekt", "Projekt")
                        .WithOne("RecenzijaOEkspert")
                        .HasForeignKey("EkspertBooker.WebAPI.Database.RecenzijaOEkspert", "ProjektId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EkspertBooker.WebAPI.Database.RecenzijaOPoslodavac", b =>
                {
                    b.HasOne("EkspertBooker.WebAPI.Database.Ekspert", "Ekspert")
                        .WithMany("RecenzijeOPoslodavci")
                        .HasForeignKey("EkspertId");

                    b.HasOne("EkspertBooker.WebAPI.Database.Poslodavac", "Poslodavac")
                        .WithMany("RecenzijeOPoslodavci")
                        .HasForeignKey("PoslodavacId");

                    b.HasOne("EkspertBooker.WebAPI.Database.Projekt", "Projekt")
                        .WithOne("RecenzijaOPoslodavac")
                        .HasForeignKey("EkspertBooker.WebAPI.Database.RecenzijaOPoslodavac", "ProjektId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
