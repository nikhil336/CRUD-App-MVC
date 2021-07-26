using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_App_Project.Models;

namespace CRUD_App_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private DataContext _context = new DataContext();
        public ActionResult Index()
        {
            return View(GetAllEmployees());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var department = _context.Departments.FirstOrDefault(c => c.DeptName.Equals(employee.Department.DeptName.ToUpper()));

            if (department == null)
            {
                ModelState.AddModelError(nameof(employee.Department.DeptName), "Enter valid Department name");
                return View();
            }

            employee.DepartmentId = department.Id;
            _context.Salaries.Add(employee.Salary);
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return View("Index", GetAllEmployees());
        }

        public ActionResult Update(int id)
        {
            return View(_context.Employees.Include("Department").FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            var department = _context.Departments.FirstOrDefault(c => c.DeptName.Equals(employee.Department.DeptName.ToUpper()));

            if (department == null)
            {
                ModelState.AddModelError(nameof(employee.Department.DeptName), "Enter valid Department name");
                return View(employee);
            }

            var salary = _context.Salaries.FirstOrDefault(e => e.Id == employee.Id);
            var emp = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);
            salary.SalaryAmount = employee.Salary.SalaryAmount;
            emp.DepartmentId = employee.DepartmentId;
            emp.Address = employee.Address;
            emp.DOJ = employee.DOJ;
            emp.Email = employee.Email;
            emp.Mobile = employee.Mobile;
            emp.Name = employee.Name;
            _context.SaveChanges();
            return RedirectToAction("Index", GetAllEmployees());
        }

        public ActionResult Delete(int id)
        {
            var salary = _context.Salaries.FirstOrDefault(e => e.Id == id);
            var emp = _context.Employees.FirstOrDefault(e => e.Id == id);
            _context.Salaries.Remove(salary);
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return RedirectToAction("Index", GetAllEmployees());
        }

        [NonAction]
        public IEnumerable<Employee> GetAllEmployees()
        {
            var context = _context.Employees.Include("Salary").Include("Department");
            return context == null ? null : context.OrderByDescending(v => v.Salary.SalaryAmount).ThenBy(v => v.Name);
        }

    }
}