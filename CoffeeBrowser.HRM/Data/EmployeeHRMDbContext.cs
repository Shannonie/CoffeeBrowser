using CoffeeBrowser.HRM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBrowser.HRM.Data
{
    public class EmployeeHRMDbContext : DbContext
    {
        public EmployeeHRMDbContext(
            DbContextOptions<EmployeeHRMDbContext> options) : base(options) { }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Finance" },
                new Department { Id = 2, Name = "Sales" },
                new Department { Id = 3, Name = "Marketing" },
                new Department { Id = 4, Name = "Human Resources" },
                new Department { Id = 5, Name = "IT" });

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Anna", LastName = "Rockstar", DepartmentId = 2 },
                new Employee { Id = 2, FirstName = "Julia", LastName = "Developer", DepartmentId = 5, IsDeveloper = true },
                new Employee { Id = 3, FirstName = "Ben", LastName = "Rockstar", DepartmentId = 4 },
                new Employee { Id = 4, FirstName = "Alex", LastName = "Rider", DepartmentId = 3, IsDeveloper = true },
                new Employee { Id = 5, FirstName = "Sophie", LastName = "Ramos", DepartmentId = 5 },
                new Employee { Id = 6, FirstName = "Jasmin", LastName = "Curtis", DepartmentId = 1, IsDeveloper = true });
        }
    }
}
