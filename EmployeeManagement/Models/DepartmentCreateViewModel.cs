using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class DepartmentCreateViewModel
    {

        [StringLength(4)]
        public string Name { get; set; }
    }
}
