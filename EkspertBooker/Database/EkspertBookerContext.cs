﻿
using EkspertBooker.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class EkspertBookerContext : DbContext
    {
        public EkspertBookerContext(DbContextOptions<EkspertBookerContext> options) : base(options)
        {
        }

        public DbSet<Projekt> Projekti { get; set; }
        public DbSet<Ponuda> Ponude { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<KorisnikKategorija> KorisniciKategorije { get; set; }
        public DbSet<Stanje> Stanja { get; set; }
        public DbSet<ProjektDetalji> ProjektDetalji { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<KorisnikSlika> KorisniciSlike { get; set; }
        public DbSet<KorisnikUloga> KorisniciUloge { get; set; }
        public DbSet<Uloga> Uloge { get; set; }
        public DbSet<Ekspert> Eksperti { get; set; }
        public DbSet<Poslodavac> Poslodavci { get; set; }
        public DbSet<RecenzijaOEkspert> RecenzijeOEksperti { get; set; }
        public DbSet<RecenzijaOPoslodavac> RecenzijeOPoslodavci { get; set; }
        public DbSet<ProjektDetaljiPrilog> ProjektDetaljiPrilozi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projekt>(entity =>
            {
            entity.HasKey(e => e.ProjektId);

               entity.Property(e => e.Naziv).IsRequired().HasMaxLength(100);

               entity.Property(e => e.KratkiOpis).IsRequired().HasMaxLength(100);

                entity.Property(e => e.StanjeId).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.Kategorija)
                .WithMany(e => e.Projekti)
                .HasForeignKey(e => e.KategorijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projekti_Kategorije");

               entity.HasOne(e => e.Stanje)
               .WithMany(e => e.Projekti)
               .HasForeignKey(e => e.StanjeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Projekti_Stanja");

                entity.Property(e => e.PoslodavacId).IsRequired();

                entity.HasOne(e => e.Poslodavac)
                .WithMany(e => e.Projekti)
                .HasForeignKey(e => e.PoslodavacId)
                .OnDelete(DeleteBehavior.ClientSetNull);
           });

            modelBuilder.Entity<Kategorija>(entity =>
            {
                entity.Property(e => e.Naziv).HasMaxLength(100);
            });

            modelBuilder.Entity<ProjektDetalji>(entity =>
            {
                entity.HasOne(e => e.Projekt).WithOne(e => e.ProjektDetalji);
            });

            modelBuilder.Entity<KorisnikSlika>(entity =>
            {
                entity.HasOne(e => e.Korisnik).WithOne(e => e.KorisnikSlika);
                entity.Property(e => e.SlikaNaziv).HasMaxLength(300);
                entity.HasIndex(e => e.KorisnikId).IsUnique();
            });

            modelBuilder.Entity<ProjektDetaljiPrilog>(entity =>
            {
                entity.HasOne(e => e.ProjektDetalji).WithOne(e => e.ProjektDetaljiPrilog);
            });

            modelBuilder.Entity<Stanje>(entity =>
            {
                entity.Property(e=>e.StanjeId).HasMaxLength(50);
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.Ime).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Prezime).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Telefon).HasMaxLength(30);
                entity.Property(e => e.LozinkaHash).HasMaxLength(200);
                entity.Property(e => e.LozinkaSalt).HasMaxLength(200);
                entity.Property(e => e.KorisnickoIme).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LozinkaHash).IsRequired();
                entity.Property(e => e.LozinkaSalt).IsRequired();
                entity.HasIndex(e => e.KorisnickoIme).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<KorisnikUloga>(entity =>
            {
                entity.HasOne(e => e.Korisnik)
                .WithMany(e => e.KorisnikUloge)
                .HasForeignKey(e => e.KorisnikId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Korisnici_KorisniciUloge");

                entity.HasKey(e => e.KorisnikUlogaId);
            });

            modelBuilder.Entity<KorisnikKategorija>(entity =>
            {
                entity.HasOne(e => e.Korisnik)
                .WithMany(e => e.KorisnikKategorije)
                .HasForeignKey(e => e.KorisnikId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Kategorija)
                .WithMany(e => e.KategorijaKorisnici)
                .HasForeignKey(e => e.KategorijaId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasKey(e => e.KorisnikKategorijaId);
            });

            modelBuilder.Entity<Uloga>(entity =>
            {
                entity.HasMany(e => e.KorisniciUloga)
                .WithOne(e => e.Uloga)
                .HasForeignKey(e => e.UlogaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Uloge_KorisniciUloge");

                entity.Property(e => e.Naziv).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Ekspert>(entity =>
            {
                entity.HasKey(e => e.KorisnikId);
                entity.HasOne(e => e.Korisnik).WithOne(e => e.Ekspert);

                entity.HasMany(e => e.Projekti)
                .WithOne(e => e.Ekspert)
                .HasForeignKey(e => e.EkspertId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.KorisnikUloga).WithOne(e => e.Ekspert).OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.BrojZavrsenihProjekata).HasDefaultValue(0);
                entity.Property(e => e.BrojRecenzija).HasDefaultValue(0);
            });

            modelBuilder.Entity<Poslodavac>(entity =>
            {
                entity.HasKey(e => e.KorisnikId);
                entity.HasOne(e => e.Korisnik).WithOne(e => e.Poslodavac);

                entity.HasMany(e => e.Projekti)
                .WithOne(e => e.Poslodavac)
                .HasForeignKey(e => e.PoslodavacId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.KorisnikUloga).WithOne(e => e.Poslodavac).OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.BrojZavrsenihProjekata).HasDefaultValue(0);
                entity.Property(e => e.BrojRecenzija).HasDefaultValue(0);
            });

            modelBuilder.Entity<RecenzijaOEkspert>(entity =>
            {
                entity.HasOne(e => e.Projekt).WithOne(e => e.RecenzijaOEkspert);
                entity.HasOne(e => e.Poslodavac).WithMany(e => e.RecenzijeOEksperti).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Ekspert).WithMany(e => e.RecenzijeOEksperti).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RecenzijaOPoslodavac>(entity =>
            {
                entity.HasOne(e => e.Projekt).WithOne(e => e.RecenzijaOPoslodavac);
                entity.HasOne(e => e.Poslodavac).WithMany(e => e.RecenzijeOPoslodavci).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Ekspert).WithMany(e => e.RecenzijeOPoslodavci).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Ponuda>(entity =>
            {
                entity.HasOne(e => e.Projekt).WithMany(e => e.Ponude).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Ekspert).WithMany(e => e.Ponude).OnDelete(DeleteBehavior.Cascade);
            });
        } 

    }
}
