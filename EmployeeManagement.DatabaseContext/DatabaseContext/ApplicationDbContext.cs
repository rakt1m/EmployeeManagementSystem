using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Models.EntityModels;

namespace EmployeeManagement.DatabaseContext.DatabaseContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=RAKTIM-PC;Database=EmployeeDB;Trusted_Connection=true");
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        

        
    }
}

