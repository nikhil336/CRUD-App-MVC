using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_App_Project.Models;

namespace CRUD_App_Project.Controllers
{
    [CustomExceptionFilter]
    public class DepartmentController : Controller
    {
        private DataContext _context = new DataContext();

        public ActionResult Index()
        {
            return View(_context.Departments.Count() == 0 ? null : _context.Departments);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Update(int Id)
        {
            return View(_context.Departments.FirstOrDefault(d => d.Id == Id));
        }

        [HttpPost]
        public ActionResult Update(Department department)
        {
            try
            {
                var dept = _context.Departments.FirstOrDefault(d => d.Id == department.Id);
                dept.Description = department.Description;
                dept.DeptName = department.DeptName;
                _context.SaveChanges();
                return RedirectToAction("Index", "Department");
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
                var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
                var employees = _context.Employees.Where(e => e.DepartmentId == dept.Id).ToList();
                foreach (var employee in employees)
                {
                    var salary = _context.Salaries.FirstOrDefault(s => s.Id == employee.Id);
                    _context.Salaries.Remove(salary);
                    _context.Employees.Remove(employee);
                }
                _context.Departments.Remove(dept);
                _context.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}