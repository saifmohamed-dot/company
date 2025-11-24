using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = -1, Name = "General" } // to map all the employees to it , if their original department got deleted .
                                                                       // we will not delete the employee associated  with deptartment 
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = -1, Name = "Temp", DepartmentId = -1 } // same for clients .
            );
            
        }
    }
}
