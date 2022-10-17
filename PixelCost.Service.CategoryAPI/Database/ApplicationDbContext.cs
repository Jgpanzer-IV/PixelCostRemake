using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PixelCost.Service.CategoryAPI.Models.Entities;
using System;

namespace PixelCost.Service.CategoryAPI.Database
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Expense>? Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Category>(option => {
                option.ToTable("Categories");
                option.HasKey(e => e.Id);
                option.Property(e => e.Name).IsRequired();
                option.Property(e => e.Cost).HasColumnType("money").HasConversion<double>().IsRequired();
                option.Property(e => e.Expense).HasColumnType("money").HasConversion<double>();
                option.Property(e => e.ExpenseCount);
                option.Property(e => e.Balance).HasColumnType("money").HasConversion<double>();
                option.Property(e => e.IsAchived).HasDefaultValue(value: true);

                option.HasMany(e=>e.Expenses).WithOne(e=>e.Category).HasForeignKey(e=>e.CategoryId).IsRequired();
            });

            modelBuilder.Entity<Expense>(option => {

                option.ToTable("Expenses");
                option.HasKey(e => e.Id);
                option.Property(e => e.OrderingName).IsRequired();
                option.Property(e => e.OrderingPrice).HasColumnType("money").HasConversion<double>().IsRequired();
                option.Property(e => e.OrderingDate).IsRequired();

                option.HasOne(e => e.Category).WithMany(e=>e.Expenses).HasForeignKey(e=>e.CategoryId).IsRequired();

            });
            
        
        }
    }
}
