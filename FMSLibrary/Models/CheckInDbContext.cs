using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FMSLibrary.Models;

public partial class CheckInDbContext : DbContext
{
    public CheckInDbContext()
    {
    }

    public CheckInDbContext(DbContextOptions<CheckInDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.CheckInId).HasName("PK__CheckIn__E64976849643C375");

            entity.ToTable("CheckIn");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
