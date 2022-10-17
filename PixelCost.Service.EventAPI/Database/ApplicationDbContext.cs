using Microsoft.EntityFrameworkCore;
using PixelCost.Service.EventAPI.Models.Entities;

namespace PixelCost.Service.EventAPI.Database
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<SubDuration>? SubDurations { get; set; }
        public DbSet<Revenue>? Revenues { get; set; }
        public DbSet<Category>? Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder optionBuilder) {

            optionBuilder.Entity<SubDuration>(action => {

                action.ToTable("Events");
                action.HasKey(e => e.Id);

                action.Property(e => e.Name).IsRequired().HasMaxLength(32);
                action.Property(e => e.StartingDate).IsRequired();
                action.Property(e => e.EndingDate).IsRequired();
                action.Property(e => e.TotalDays);
                action.Property(e => e.RemainingDays);
                action.Property(e => e.Progress);
                action.Property(e => e.InitialCost).HasColumnType("money").HasConversion<double>().IsRequired();
                action.Property(e => e.TotalCost).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.SumCategoryCost).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.UsableMoney).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.Revenue).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.RevenueCount);
                action.Property(e => e.Expense).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.ExpenseCount);
                action.Property(e => e.Balance).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.SumCategoryBalance).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.IsAchived).HasDefaultValue(value:true);

                action.HasMany(e => e.Revenues).WithOne(e => e.SubDuration).HasForeignKey(e=>e.SubDurationId).IsRequired();
                action.HasMany(e => e.Categories).WithOne(e => e.SubDuration).HasForeignKey(e => e.SubDurationId).IsRequired();
            });

            optionBuilder.Entity<Category>(action => {

                action.ToTable("Categories");
                action.HasKey(e => e.Id);

                action.Property(e => e.Name).IsRequired();
                action.Property(e => e.Cost).HasColumnType("money").HasConversion<double>().IsRequired();
                action.Property(e => e.Expense).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.ExpenseCount);
                action.Property(e => e.Balance).HasColumnType("money").HasConversion<double>();
                action.Property(e => e.IsAchived).HasDefaultValue(value:true);

                action.HasOne(e=>e.SubDuration).WithMany(e=>e.Categories).HasForeignKey(e=>e.SubDurationId).IsRequired();
            });

            optionBuilder.Entity<Revenue>(action => {
                action.ToTable("Revenues");
                action.HasKey(e => e.Id);

                action.Property(e => e.Task).IsRequired();
                action.Property(e => e.EarningAmount).HasColumnType("money").HasConversion<double>().IsRequired();
                action.Property(e => e.EarningDate).IsRequired();

                action.HasOne(e => e.SubDuration).WithMany(e => e.Revenues).HasForeignKey(e => e.SubDurationId).IsRequired();
            });


        }


    }
}
