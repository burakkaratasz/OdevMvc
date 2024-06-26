﻿using Sube2.HelloMvc.Models.Relationships;
using Microsoft.EntityFrameworkCore;
using Sube2.HelloMvc.Models;

namespace Sube2.HelloMvc.Models
{
    public class OkulDbContext : DbContext
    {
        public DbSet<Ogrenci> Ogrenciler { get; set; } //öğrenci tablosu
        public DbSet<Ders> Dersler { get; set; } //ders tablosu

        public DbSet<OgrenciDers> OgrenciDersler { get; set; } //öğrenciders ilişki tablosu

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-C1RTOMS\\SQLEXPRESS;Initial Catalog= OkulDbMvc;Integrated Security = True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ogrenci>().ToTable("tblOgrenciler");
            modelBuilder.Entity<Ogrenci>().Property(o => o.Ad).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Soyad).HasColumnType("varchar").HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Numara).IsRequired();

            modelBuilder.Entity<Ders>().ToTable("tblDersler");
            modelBuilder.Entity<Ders>().Property(d => d.DersAd).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Ders>().Property(d => d.Kredi).IsRequired();
            modelBuilder.Entity<Ders>().Property(d => d.DersKodu).HasColumnType("varchar").HasMaxLength(10).IsRequired();


            modelBuilder.Entity<OgrenciDers>().ToTable("tblOgrenciDersler");
            modelBuilder.Entity<OgrenciDers>().HasKey(od => new { od.Ogrenciid, od.Dersid }); //birincil anahtar
            modelBuilder.Entity<OgrenciDers>().HasOne(od => od.Ogrenci).WithMany(o => o.OgrenciDersler).HasForeignKey(od => od.Ogrenciid); //öğrenciders tablosu bir öğrenciye sahiptir.
            //öğrenci tablosu birçok öğrenciders tablosuna sahiptir.
            modelBuilder.Entity<OgrenciDers>().HasOne(od => od.Ders).WithMany(d => d.OgrenciDersler).HasForeignKey(od => od.Dersid);


        }
    }
}