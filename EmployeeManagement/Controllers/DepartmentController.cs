using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Models.EntityModels;
using EmployeeManagement.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentRepository _departmentRepository;
        private IMapper _mapper;

        public DepartmentController(DepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);
                bool isSaved = _departmentRepository.Add(department);

                if (isSaved)
                {
                    ViewBag.Message = "Saved Successfully!";
                }

            }
            ModelState.Clear();
            return View();
        }
    }
}
