using Microsoft.EntityFrameworkCore;
using PixelCost.Service.DurationAPI.Models.Entities;

namespace PixelCost.Service.DurationAPI.Database
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {}

        public DbSet<Duration>? Durations { get; set; }
        public DbSet<SubDuration>? SubDurations { get; set; }
        public DbSet<PrimaryExpense>? PrimaryExpenses { get; set; }
        public DbSet<Revenue>? Revenues { get; set; }
        public DbSet<Category>? Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Duration>(buildAction =>
            {
                buildAction.ToTable("Durations");
                buildAction.HasKey(e => e.Id);
                buildAction.Property(e => e.UserId).IsRequired();
                buildAction.Property(e => e.Name).IsRequired().HasMaxLength(32);
                buildAction.Property(e => e.StartingDate).IsRequired();
                buildAction.Property(e => e.EndingDate).IsRequired();
                buildAction.Property(e => e.TotalDays);
                buildAction.Property(e => e.RemainingDays);
                buildAction.Property(e => e.Progress);
                buildAction.Property(e => e.InitialCost).IsRequired().HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.TotalCost).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.SumSubDurationCost).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.SumCategoryCost).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.UsableMoney).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.Revenue).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.RevenueCount);
                buildAction.Property(e => e.Expense).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.ExpenseCount);
                buildAction.Property(e => e.Balance).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.SumSubDurationBalance).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.SumCategoryBalance).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.IsActive).HasDefaultValue(value: true);

                buildAction.HasMany(e => e.SubDurations).WithOne(e => e.Duration).HasForeignKey(e => e.DurationId).IsRequired();
                buildAction.HasMany(e => e.Revenues).WithOne(e => e.Duration).HasForeignKey(e => e.DurationId).IsRequired();
                buildAction.HasMany(e => e.PrimaryExpenses).WithOne(e => e.Duration).HasForeignKey(e => e.DurationId).IsRequired();
                buildAction.HasMany(e => e.Categories).WithOne(e => e.Duration).HasForeignKey(e => e.DurationId).IsRequired();
            });

            modelBuilder.Entity<SubDuration>(buildAction => {
                buildAction.ToTable("SubDurations");
                buildAction.HasKey(e => e.Id);

                buildAction.Property(e => e.Name).IsRequired().HasMaxLength(32);
                buildAction.Property(e => e.StartingDate).IsRequired();
                buildAction.Property(e => e.EndingDate).IsRequired();
                buildAction.Property(e => e.TotalDays);
                buildAction.Property(e => e.RemainingDays);
                buildAction.Property(e => e.Progress);
                buildAction.Property(e => e.InitialCost).HasColumnType("money").HasConversion<double>().IsRequired();
                buildAction.Property(e => e.TotalCost).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.SumCategoryCost).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.UsableMoney).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.Revenue).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.RevenueCount);
                buildAction.Property(e => e.Expense).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.ExpenseCount);
                buildAction.Property(e => e.Balance).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.SumCategoryBalance).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.IsAchived).HasDefaultValue(true);

                buildAction.HasOne(e => e.Duration).WithMany(e => e.SubDurations).HasForeignKey(e => e.DurationId).IsRequired();
            });

            modelBuilder.Entity<Category>(buildAction =>
            {
                buildAction.ToTable("Categories");
                buildAction.HasKey(e => e.Id);

                buildAction.Property(e => e.Name).IsRequired();
                buildAction.Property(e => e.Cost).HasColumnType("money").HasConversion<double>().IsRequired();
                buildAction.Property(e => e.Expense).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.ExpenseCount);
                buildAction.Property(e => e.Balance).HasColumnType("money").HasConversion<double>();
                buildAction.Property(e => e.IsAchived).HasDefaultValue(true);

                buildAction.HasOne(e=>e.Duration).WithMany(e=>e.Categories).HasForeignKey(e=>e.DurationId).IsRequired();
            });
            
            modelBuilder.Entity<PrimaryExpense>(buildAction => {
                buildAction.ToTable("PrimaryExpenses");
                buildAction.HasKey(e => e.Id);

                buildAction.Property(e => e.OrderingName).IsRequired();
                buildAction.Property(e => e.OrderingDate).IsRequired();
                buildAction.Property(e => e.OrderingPrice).HasColumnType("money").HasConversion<double>().IsRequired();

                buildAction.HasOne(e=>e.Duration).WithMany(e=>e.PrimaryExpenses).HasForeignKey(e => e.DurationId).IsRequired();
            });

            modelBuilder.Entity<Revenue>(buildAction => {
                buildAction.ToTable("Revenues");
                buildAction.HasKey(e => e.Id);

                buildAction.Property(e => e.Task).IsRequired();
                buildAction.Property(e => e.EarningAmount).HasColumnType("money").HasConversion<double>().IsRequired();
                buildAction.Property(e => e.EarningDate).IsRequired();

                buildAction.HasOne(e=>e.Duration).WithMany(e=>e.Revenues).HasForeignKey(e => e.DurationId).IsRequired();
                
            });
        
        }

    }
}
