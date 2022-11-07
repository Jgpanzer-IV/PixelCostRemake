using Microsoft.EntityFrameworkCore;
using PixelCost.Service.RecordingAPI.Models.DTOs;

namespace PixelCost.Service.RecordingAPI.Database
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public DbSet<ExpenseDTO>? Expenses { get; set; }
        public DbSet<PrimaryExpenseDTO>? PrimaryExpenses { get; set; }
        public DbSet<RevenueDTO>? Revenues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<ExpenseDTO>(entity => {
                entity.ToTable("Expenses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderingName).IsRequired().HasMaxLength(64);
                entity.Property(e => e.OrderingPrice).IsRequired().HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.OrderingDate).IsRequired();

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.DurationId).IsRequired();
                entity.Property(e => e.SubDurationId);
                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.PaymentMethodId).IsRequired();
            });

            builder.Entity<PrimaryExpenseDTO>(entity => {
                entity.ToTable("PrimaryExpenses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderingName).IsRequired().HasMaxLength(64);
                entity.Property(e => e.OrderingPrice).IsRequired().HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.OrderingDate).IsRequired();

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.DurationId).IsRequired();
                entity.Property(e => e.PaymentMethodId).IsRequired();
            });

            builder.Entity<RevenueDTO>(entity => {
                entity.ToTable("Revenues");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Task).IsRequired().HasMaxLength(64);
                entity.Property(e => e.EarningAmount).IsRequired().HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.EarningDate).IsRequired();

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.DurationId).IsRequired();
                entity.Property(e => e.SubDurationId);
                entity.Property(e => e.PaymentMethodId).IsRequired();
            });

        }


    }
}
