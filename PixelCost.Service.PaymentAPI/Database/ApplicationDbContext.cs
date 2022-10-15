using Microsoft.EntityFrameworkCore;
using PixelCost.Service.PaymentAPI.Models.Entities;

namespace PixelCost.Service.PaymentAPI.Database
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        { }

        public DbSet<PaymentMethod>? PaymentMethods { get; set; }
        public DbSet<Expense>? Expenses { get; set; }
        public DbSet<PrimaryExpense>? PrimaryExpense { get; set; }
        public DbSet<Revenue>? Revenues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethods");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PaymentName).IsRequired().HasMaxLength(32);
                entity.Property(e => e.PaymentType).IsRequired().HasMaxLength(32);
                entity.Property(e => e.PaymentRevenue).HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.PaymentRevenueCount).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.PaymentBalance).HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.PaymentExpense).HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.PaymentExpenseCount).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.AverageUsedPerPayment).HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.Symbol).HasMaxLength(512).IsRequired();

                entity.Property(e => e.UserId).IsRequired();
                entity.HasMany(e => e.Expenses).WithOne(e => e.PaymentMethod).HasForeignKey(e => e.PaymentId);
                entity.HasMany(e => e.Revenues).WithOne(e => e.PaymentMethod).HasForeignKey(e => e.PaymentId);

            });

            builder.Entity<Expense>(entity => {
                entity.ToTable("Expenses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderingName).IsRequired().HasMaxLength(64);
                entity.Property(e => e.OrderingPrice).IsRequired().HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.OrderingDate).IsRequired();

                entity.HasOne(e => e.PaymentMethod).WithMany(e => e.Expenses).HasForeignKey(e => e.PaymentId);

              /*  entity.HasData(
                    new Expense
                    {
                        Id = 1,
                        OrderingName = "Burger",
                        OrderingPrice = 120M,
                        OrderingDate = new DateTime(2022, 9, 11),
                        PaymentId = 1
                    },
                    new Expense
                    {
                        Id = 2,
                        OrderingName = "Pizza",
                        OrderingPrice = 340M,
                        OrderingDate = new DateTime(2022, 9, 16),
                        PaymentId = 1
                    },
                    new Expense
                    {
                        Id = 3,
                        OrderingName = "Chocolat",
                        OrderingPrice = 74M,
                        OrderingDate = new DateTime(2022, 9, 13),
                        PaymentId = 1
                    }
                );*/
            });

            builder.Entity<PrimaryExpense>(entity => {
                entity.ToTable("PrimaryExpenses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderingName).IsRequired().HasMaxLength(64);
                entity.Property(e => e.OrderingPrice).IsRequired().HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.OrderingDate).IsRequired();
                entity.HasOne(e => e.PaymentMethod).WithMany(e => e.PrimaryExpenses).HasForeignKey(e => e.PaymentId).IsRequired();
                /*
                entity.HasData(
                    new PrimaryExpense
                    {
                        Id = 1,
                        OrderingName = "Room",
                        OrderingPrice = 8900M,
                        PaymentId = 1,
                        OrderingDate = new DateTime(2022, 07, 3),
                    },
                    new PrimaryExpense
                    {
                        Id = 2,
                        OrderingName = "Services",
                        OrderingPrice = 3120M,
                        PaymentId = 1,
                        OrderingDate = new DateTime(2022, 07, 8),
                    },
                    new PrimaryExpense
                    {
                        Id = 3,
                        OrderingName = "Game Fallout 76",
                        OrderingPrice = 1340M,
                        PaymentId = 1,
                        OrderingDate = new DateTime(2022, 07, 12),
                    }
                );*/
            });

            builder.Entity<Revenue>(entity => {
                entity.ToTable("Revenues");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Task).IsRequired().HasMaxLength(64);
                entity.Property(e => e.EarningAmount).IsRequired().HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.EarningDate).IsRequired();

                entity.HasOne(e => e.PaymentMethod).WithMany(e => e.Revenues).HasForeignKey(e => e.PaymentId);

                /*entity.HasData(
                    new Revenue
                    {
                        Id = 1,
                        Task = "Web development",
                        EarningAmount = 84200M,
                        EarningDate = new DateTime(2022, 9, 1),
                        PaymentId = 1,
                    },
                    new Revenue
                    {
                        Id = 2,
                        Task = "Selling Game",
                        EarningAmount = 9780M,
                        EarningDate = new DateTime(2022, 9, 2),
                        PaymentId = 1
                    },
                    new Revenue
                    {
                        Id = 3,
                        Task = "Pixel's yield of service",
                        EarningAmount = 78200M,
                        EarningDate = new DateTime(2022, 9, 3),
                        PaymentId = 1
                    }
                );*/

            });

        }


    }
}
