using BestFinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BestFinanceTracker.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Budget> Budgets => Set<Budget>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.TransactionType).IsRequired();
        });

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
             d => d.ToDateTime(TimeOnly.MinValue),
             d => DateOnly.FromDateTime(d));

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Amount).HasPrecision(18, 2);
            entity.Property(t => t.Date)
                  .IsRequired()
                  .HasConversion(dateOnlyConverter)
                  .HasColumnType("date");
            entity.Property(t => t.Type).IsRequired();
            entity.Property(t => t.Description).HasMaxLength(250);

            entity.HasOne(t => t.Category)
                  .WithMany()
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Limit).HasPrecision(18, 2);
            entity.Property(b => b.Year).IsRequired();
            entity.Property(b => b.Month).IsRequired();

            entity.HasOne(b => b.Category)
                  .WithMany()
                  .HasForeignKey(b => b.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(b => new { b.CategoryId, b.Year, b.Month }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}