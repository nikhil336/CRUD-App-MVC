using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_App_Project.Models;

namespace CRUD_App_Project.Controllers
{
    [CustomExceptionFilter]
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
            try
            {
                var department = _context.Departments.FirstOrDefault(c => c.DeptName.Equals(employee.Department.DeptName.ToUpper()));

                if (department == null)
                {
                    ModelState.AddModelError(nameof(employee.Department.DeptName), "Enter valid Department name");
                    return View();
                }

                employee.Department = department;
                employee.Department.Id = department.Id;
                _context.Salaries.Add(employee.Salary);
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return View("Index", GetAllEmployees());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Update(int id)
        {
            return View(_context.Employees.Include("Department").FirstOrDefault(e => e.Id == id));
        }

        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            try
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
                emp.DepartmentId = department.Id;
                emp.Address = employee.Address;
                emp.DOJ = employee.DOJ;
                emp.Email = employee.Email;
                emp.Mobile = employee.Mobile;
                emp.Name = employee.Name;
                _context.SaveChanges();
                return RedirectToAction("Index", GetAllEmployees());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            { 
                var salary = _context.Salaries.FirstOrDefault(e => e.Id == id);
                var emp = _context.Employees.FirstOrDefault(e => e.Id == id);
                _context.Salaries.Remove(salary);
                _context.Employees.Remove(emp);
                _context.SaveChanges();
                return RedirectToAction("Index", GetAllEmployees());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [NonAction]
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                var context = _context.Employees.Include("Salary").Include("Department");
                return context.Count() == 0 ? null : context.OrderByDescending(v => v.Salary.SalaryAmount).ThenBy(v => v.Name);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}