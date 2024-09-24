using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class DunderMifflinContext : DbContext
{
    public DunderMifflinContext(DbContextOptions<DunderMifflinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderEntry> OrderEntries { get; set; }

    public virtual DbSet<Paper> Papers { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'pending'::character varying");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_customer_id_fkey");
        });

        modelBuilder.Entity<OrderEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_entries_pkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderEntries).HasConstraintName("order_entries_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderEntries).HasConstraintName("order_entries_product_id_fkey");
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paper_pkey");

            entity.Property(e => e.Discontinued).HasDefaultValue(false);
            entity.Property(e => e.Stock).HasDefaultValue(0);

            entity.HasMany(d => d.Properties).WithMany(p => p.Papers)
                .UsingEntity<Dictionary<string, object>>(
                    "PaperProperty",
                    r => r.HasOne<Property>().WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("paper_properties_property_id_fkey"),
                    l => l.HasOne<Paper>().WithMany()
                        .HasForeignKey("PaperId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("paper_properties_paper_id_fkey"),
                    j =>
                    {
                        j.HasKey("PaperId", "PropertyId").HasName("paper_properties_pkey");
                        j.ToTable("paper_properties");
                        j.HasIndex(new[] { "PropertyId" }, "IX_paper_properties_property_id");
                        j.IndexerProperty<int>("PaperId").HasColumnName("paper_id");
                        j.IndexerProperty<int>("PropertyId").HasColumnName("property_id");
                    });
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("properties_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
