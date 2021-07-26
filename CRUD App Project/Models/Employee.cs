using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_App_Project.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Employee Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Id is required")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of joining is required")]
        [DataType(DataType.Date)]
        public DateTime DOJ { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [DataType(DataType.PhoneNumber)]
        [Index(IsUnique=true)]
        [MaxLength(10)]
        public string Mobile { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required")]
        [Index(IsUnique=true)]
        [MaxLength(25)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(40)]
        public string Address { get; set; }

        public virtual Salary Salary { get; set; }
    }
}