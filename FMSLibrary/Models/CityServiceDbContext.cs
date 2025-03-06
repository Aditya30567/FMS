using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FMSLibrary.Models;

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
            entity.HasKey(e => e.CityId).HasName("PK__City__F2D21B76A7E7A117");
            entity.Property(e => e.CityId)
             .ValueGeneratedOnAdd();
            entity.ToTable("City");

            entity.HasIndex(e => e.CityCode, "UQ__City__B488218CE9375948").IsUnique();

            entity.Property(e => e.AirportCharge).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CityCode).HasMaxLength(10);
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
