using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeManagement.DatabaseContext.DatabaseContext;
using EmployeeManagement.Models.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories.Repository
{
    public class DepartmentRepository
    {
       ApplicationDbContext db = new ApplicationDbContext();
        public bool Add(Department department)
        {
            db.Departments.Add(department);
            return db.SaveChanges() > 0;

        }

        public List<Department> GetAll()
        {
            return db.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return db.Departments
                .Include(c => c.Employees)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
