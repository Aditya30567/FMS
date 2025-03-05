using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CityService.Models;

public partial class CityServiceDbContext : DbContext
{
    public CityServiceDbContext()
    {
    }

    public CityServiceDbContext(DbContextOptions<CityServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__F2D21B76D0D4F916");

            entity.ToTable("City");

            entity.HasIndex(e => e.CityCode, "UQ__City__B488218CF5F2BC2C").IsUnique();

            entity.Property(e => e.CityId).ValueGeneratedNever();
            entity.Property(e => e.AirportCharge).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CityCode).HasMaxLength(10);
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("Active");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
