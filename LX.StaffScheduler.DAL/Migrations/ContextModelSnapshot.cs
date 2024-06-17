﻿// <auto-generated />
using System;
using LX.StaffScheduler.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LX.StaffScheduler.DAL.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LX.StaffScheduler.DAL.Cafe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressOfCafe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Cafes");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CafeId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("EndContractDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateOnly>("StartContractDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.UserContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayWeek")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("EndContractTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("StartContractTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("UserContracts");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.WorkShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CafeId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateOnly>("ShiftDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("WorkShifts");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.Cafe", b =>
                {
                    b.HasOne("LX.StaffScheduler.DAL.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.District", b =>
                {
                    b.HasOne("LX.StaffScheduler.DAL.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.Employee", b =>
                {
                    b.HasOne("LX.StaffScheduler.DAL.Cafe", "Cafe")
                        .WithMany("Employees")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.UserContract", b =>
                {
                    b.HasOne("LX.StaffScheduler.DAL.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.WorkShift", b =>
                {
                    b.HasOne("LX.StaffScheduler.DAL.Cafe", "Cafe")
                        .WithMany("Workshifts")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LX.StaffScheduler.DAL.Employee", "Employee")
                        .WithMany("Workshifts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.Cafe", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Workshifts");
                });

            modelBuilder.Entity("LX.StaffScheduler.DAL.Employee", b =>
                {
                    b.Navigation("Workshifts");
                });
#pragma warning restore 612, 618
        }
    }
}