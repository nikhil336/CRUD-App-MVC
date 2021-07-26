using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_App_Project.Models
{
    public class Department
    {
        [Required(ErrorMessage = "Department id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [MinLength(2)]
        [MaxLength(10)]
        public string DeptName { get; set; }

        [Required(ErrorMessage = "Department Description is required")]
        [MinLength(10)]
        [MaxLength(40)]
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}