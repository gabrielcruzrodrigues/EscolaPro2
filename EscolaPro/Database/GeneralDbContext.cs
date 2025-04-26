﻿using EscolaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Database;

public class GeneralDbContext : DbContext
{
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<UserGeneral> UsersGeneral { get; set; }
    public DbSet<Salt> Salts { get; set; }
    public DbSet<Family> Families { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasIndex(e => e.CNPJ)
            .IsUnique();

        modelBuilder.Entity<UserGeneral>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
        });
    }
}
