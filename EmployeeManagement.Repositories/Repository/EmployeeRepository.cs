using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeManagement.DatabaseContext.DatabaseContext;
using EmployeeManagement.Models.EntityModels;
using EmployeeManagement.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        ApplicationDbContext db = new ApplicationDbContext();

        
       
        
       

        public IEnumerable<Employee> Search(Employee employeeSearchCriteria)
        {
            var result = db.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(employeeSearchCriteria.Name))
            {
                result = result.Where(c => c.Name.Contains(employeeSearchCriteria.Name));
            }

            if (!string.IsNullOrEmpty(employeeSearchCriteria.RegNo))
            {
                result = result.Where(c => c.RegNo.Contains(employeeSearchCriteria.RegNo));
            }

            if (!string.IsNullOrEmpty(employeeSearchCriteria.MobileNumber))
            {
                result = result.Where(c => c.MobileNumber.StartsWith(employeeSearchCriteria.MobileNumber));
            }

            return result.ToList();
        }
        public override ICollection<Employee> GetAll()
        {
            return db.Employees
                .Include(c => c.Department)
                .ToList();
        }
      
       
        public List<Employee> GetByDepartmentId(int departmentId)
        {
            return db.Employees.Where(c => c.DepartmentId == departmentId).ToList();
        }
    }
}
