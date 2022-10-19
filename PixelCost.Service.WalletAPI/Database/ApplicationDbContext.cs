using Microsoft.EntityFrameworkCore;
using PixelCost.Service.WalletAPI.Model.Entities;

namespace PixelCost.Service.WalletAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
        }

        public DbSet<Wallet>? Wallets { get; set; }
        public DbSet<PaymentMethod>? PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallets");
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserID).IsRequired();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(32);
                entity.Property(e => e.JobTitle).HasMaxLength(32);
                entity.Property(e => e.Balance).HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.TotalExpense).HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.TotalNumberExpense).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.TotalRevenue).HasColumnType("money").HasConversion<double>();
                entity.Property(e => e.TotalNumberRevenue).IsRequired().HasDefaultValue(0);
                entity.HasMany(e => e.PaymentMethods).WithOne(e => e.Wallet).HasForeignKey(f => f.WalletID).IsRequired();

                /*entity.HasData(
                    new Wallet{
                        UserID = "Van2001",
                        Username = "Karnchai Sakkarnjana",
                        JobTitle = "Software Developer",
                        Balance = 20000,
                        DateCreate = DateTime.Now,
                    }
                );*/
            });

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
                entity.HasOne(e => e.Wallet).WithMany(e => e.PaymentMethods).HasForeignKey(f => f.WalletID).IsRequired();

                /*entity.HasData(
                    new PaymentMethod
                    {
                        Id = 1,
                        WalletID = "Van2001",
                        PaymentName = "KrungThai Van",
                        PaymentType = "Mobile Banking",
                        DateCreate = new DateTime(2022, 8, 30),
                        PaymentRevenue = 5250,
                        PaymentExpense = 4250,
                        PaymentBalance = 1000,
                        PaymentExpenseCount = 10,
                        PaymentRevenueCount = 7,
                        AverageUsedPerPayment = 425,
                        Symbol = "https://Domain/img/component/krungthaiSym.jpg"
                    },
                    new PaymentMethod
                    {
                        Id = 2,
                        WalletID = "Van2001",
                        PaymentName = "SCB Van",
                        PaymentType = "Mobile Banking",
                        DateCreate = new DateTime(2022, 8, 30),
                        PaymentRevenue = 10000,
                        PaymentExpense = 8000,
                        PaymentBalance = 2000,
                        PaymentExpenseCount = 2,
                        PaymentRevenueCount = 10,
                        AverageUsedPerPayment = 4000,
                        Symbol = "https://Domain/img/component/scbSym.jpg"
                    },
                    new PaymentMethod
                    {
                        Id = 3,
                        WalletID = "Van2001",
                        PaymentName = "Daly spending",
                        PaymentType = "Cache",
                        DateCreate = new DateTime(2022, 8, 30),
                        PaymentRevenue = 70000,
                        PaymentExpense = 60000,
                        PaymentBalance = 10000,
                        PaymentExpenseCount = 10,
                        PaymentRevenueCount = 1,
                        AverageUsedPerPayment = 6000,
                        Symbol = "https://Domain/img/component/cacheSym.jpg"
                    }
                );*/

            });


        }

    }
}
