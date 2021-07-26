using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_App_Project.Models
{
    public class Salary
    {
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        public int SalaryAmount { get; set; }
    }
}