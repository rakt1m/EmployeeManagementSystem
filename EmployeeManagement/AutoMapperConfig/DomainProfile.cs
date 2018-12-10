using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Models.EntityModels;

namespace EmployeeManagement.AutoMapperConfig
{
   public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<EmployeeCreateViewModel,Employee>();
            CreateMap<Employee, EmployeeCreateViewModel>();
            CreateMap<DepartmentCreateViewModel, Department>();
            CreateMap<Department, DepartmentCreateViewModel>();
        }
    }
}
