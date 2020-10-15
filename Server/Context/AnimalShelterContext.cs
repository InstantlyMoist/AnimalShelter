using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.Models;
using System;

namespace Server.Context
{
    public class AnimalShelterContext : DbContext
    {
        public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Remark>().HasOne(r => r.Animal).WithMany().HasForeignKey(p => p.AnimalId);

            modelBuilder.Entity<Remark>().Property(r => r.EntryDate).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<Animal>().Property(a => a.EntryDate).HasDefaultValueSql ("getutcdate()");

            modelBuilder.Entity<Animal>()
                .Property(e => e.AnimalType)
                .HasConversion(v => v.ToString(), v => (AnimalType)Enum.Parse(typeof(AnimalType), v));

            modelBuilder.Entity<Animal>()
                .Property(e => e.Gender)
                .HasConversion(v => v.ToString(), v => (Gender)Enum.Parse(typeof(Gender), v));

            modelBuilder.Entity<Animal>()
                .Property(e => e.CanBeWithKids)
                .HasConversion(v => v.ToString(), v => (DeepBoolean)Enum.Parse(typeof(DeepBoolean), v));
        }

        public DbSet<Animal> Animal { get; set; }
        public DbSet<Remark> Remark { get; set; }
    }
}