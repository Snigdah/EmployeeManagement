using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .HasKey(u => u.employeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(u => u.Attendances)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.employeeId);

            

            modelBuilder.Entity<EmployeeAttendance>()
                .HasKey(oi => oi.Id);

            modelBuilder.Entity<EmployeeAttendance>()
                .HasOne(oi => oi.Employee)
                .WithMany(o => o.Attendances)
                .HasForeignKey(oi => oi.employeeId);


    
        }
    }
}
