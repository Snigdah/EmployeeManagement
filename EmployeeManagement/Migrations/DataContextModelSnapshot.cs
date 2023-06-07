﻿// <auto-generated />
using System;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeManagement.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeManagement.Models.Employee", b =>
                {
                    b.Property<int>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employeeId"));

                    b.Property<string>("employeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("employeeSalary")
                        .HasColumnType("int");

                    b.HasKey("employeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeManagement.Models.EmployeeAttendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("attendanceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("employeeId")
                        .HasColumnType("int");

                    b.Property<bool>("isAbsent")
                        .HasColumnType("bit");

                    b.Property<bool>("isOffday")
                        .HasColumnType("bit");

                    b.Property<bool>("isPresent")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.ToTable("EmployeeAttendances");
                });

            modelBuilder.Entity("EmployeeManagement.Models.EmployeeAttendance", b =>
                {
                    b.HasOne("EmployeeManagement.Models.Employee", "Employee")
                        .WithMany("Attendances")
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Employee", b =>
                {
                    b.Navigation("Attendances");
                });
#pragma warning restore 612, 618
        }
    }
}
