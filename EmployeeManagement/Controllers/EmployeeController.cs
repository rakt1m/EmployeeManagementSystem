using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.Models.EntityModels;
using EmployeeManagement.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeRepository _employeeRepository;
        private DepartmentRepository _departmentRepository;

        public EmployeeController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepository)
        {
            this._employeeRepository = employeeRepository;
            this._departmentRepository = departmentRepository;
        }
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            model.Departments = _departmentRepository
                 .GetAll()
                 .Select(c => new SelectListItem
                 {
                     Value = c.Id.ToString(),
                     Text = c.Name
                 }).ToList();
            model.Employees = _employeeRepository.GetAll();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    DepartmentId = model.DepartmentId,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    Salary = model.Salary,
                    RegNo = model.RegNo
                };
                bool isSaved = _employeeRepository.Add(employee);
                if (isSaved)
                {
                    ViewBag.Message = "Saved Succesful!";
                }
            }

            model.Departments = _departmentRepository
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            model.Employees = _employeeRepository.GetAll();
            return View(model);
        }

        public async Task<IActionResult> Search(string name, string regNo)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> searchQuery = from m in _employeeRepository.Search(employeeSearchCriteria: Name)
                orderby m.Name
                select m.Name;


            return View(model);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.Update(id);
            if (employee == null)
            {
                return NotFound();
            }
           

            return View();
        }
        [HttpPost]
        public IActionResult Update(int id, [Bind("name,regNo,mobileNumber,salary,email,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id)
                {
                    return NotFound();
                }
                try
                {
                    _employeeRepository.Update(employee);
                    _employeeRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExist(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Search");
            }
            
            return View();
        }

        private bool EmployeeExist(int id)
        {
            return _employeeRepository.Update.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.Delete
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeRepository.Delete.FindAsync(id);
            _employeeRepository.Employee.Remove(employee);
           
            await _employeeRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Search));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.Delete
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(course);
        }

    }
}

