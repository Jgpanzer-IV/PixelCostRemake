﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PixelCost.Service.DurationAPI.Database;

#nullable disable

namespace PixelCost.Service.DurationAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal?>("Balance")
                        .HasColumnType("money");

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<long>("DurationId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Expense")
                        .HasColumnType("money");

                    b.Property<int?>("ExpenseCount")
                        .HasColumnType("int");

                    b.Property<bool?>("IsAchived")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.Duration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal?>("Balance")
                        .HasColumnType("money");

                    b.Property<DateTime>("EndingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Expense")
                        .HasColumnType("money");

                    b.Property<int?>("ExpenseCount")
                        .HasColumnType("int");

                    b.Property<decimal>("InitialCost")
                        .HasColumnType("money");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<float?>("Progress")
                        .HasColumnType("real");

                    b.Property<int?>("RemainingDays")
                        .HasColumnType("int");

                    b.Property<decimal?>("Revenue")
                        .HasColumnType("money");

                    b.Property<int?>("RevenueCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("SumCategoryBalance")
                        .HasColumnType("money");

                    b.Property<decimal?>("SumCategoryCost")
                        .HasColumnType("money");

                    b.Property<decimal?>("SumSubDurationBalance")
                        .HasColumnType("money");

                    b.Property<decimal?>("SumSubDurationCost")
                        .HasColumnType("money");

                    b.Property<decimal?>("TotalCost")
                        .HasColumnType("money");

                    b.Property<int?>("TotalDays")
                        .HasColumnType("int");

                    b.Property<decimal?>("UsableMoney")
                        .HasColumnType("money");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Durations", (string)null);
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.PrimaryExpense", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("DurationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("OrderingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderingName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OrderingPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.ToTable("PrimaryExpenses", (string)null);
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.Revenue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("DurationId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("EarningAmount")
                        .HasColumnType("money");

                    b.Property<DateTime>("EarningDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Task")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.ToTable("Revenues", (string)null);
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.SubDuration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal?>("Balance")
                        .HasColumnType("money");

                    b.Property<long>("DurationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Expense")
                        .HasColumnType("money");

                    b.Property<int?>("ExpenseCount")
                        .HasColumnType("int");

                    b.Property<decimal>("InitialCost")
                        .HasColumnType("money");

                    b.Property<bool?>("IsAchived")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<float?>("Progress")
                        .HasColumnType("real");

                    b.Property<int?>("RemainingDays")
                        .HasColumnType("int");

                    b.Property<decimal?>("Revenue")
                        .HasColumnType("money");

                    b.Property<int?>("RevenueCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("SumCategoryBalance")
                        .HasColumnType("money");

                    b.Property<decimal?>("SumCategoryCost")
                        .HasColumnType("money");

                    b.Property<decimal?>("TotalCost")
                        .HasColumnType("money");

                    b.Property<int?>("TotalDays")
                        .HasColumnType("int");

                    b.Property<decimal?>("UsableMoney")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.ToTable("SubDurations", (string)null);
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.Category", b =>
                {
                    b.HasOne("PixelCost.Service.DurationAPI.Models.Entities.Duration", "Duration")
                        .WithMany("Categories")
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Duration");
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.PrimaryExpense", b =>
                {
                    b.HasOne("PixelCost.Service.DurationAPI.Models.Entities.Duration", "Duration")
                        .WithMany("PrimaryExpenses")
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Duration");
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.Revenue", b =>
                {
                    b.HasOne("PixelCost.Service.DurationAPI.Models.Entities.Duration", "Duration")
                        .WithMany("Revenues")
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Duration");
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.SubDuration", b =>
                {
                    b.HasOne("PixelCost.Service.DurationAPI.Models.Entities.Duration", "Duration")
                        .WithMany("SubDurations")
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Duration");
                });

            modelBuilder.Entity("PixelCost.Service.DurationAPI.Models.Entities.Duration", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("PrimaryExpenses");

                    b.Navigation("Revenues");

                    b.Navigation("SubDurations");
                });
#pragma warning restore 612, 618
        }
    }
}
