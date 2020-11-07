using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDMVCAjaxDataTable.Data;

using CRUDMVCAjaxDataTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDMVCAjaxDataTable.Controllers
{
    public class EmployeeListEditController : Controller
    {
        private readonly DbContexto _dbContexto;

        public EmployeeListEditController(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }
        public IActionResult Index()
        {

            //List<Employee> employees = new List<Employee>();
            List<Employee> employees = GetAllEmployee();

            employees.Insert(0, new Employee());
            return View(employees);
        }

        public class EmployeeDto
        {
            public string Name { get; set; }
            public int? Salary { get; set; }
        }

        [HttpPost]
        public JsonResult InsertEmployee(EmployeeDto employee)
        {
            //_dbContexto.Employee.Add(employee);
            //_dbContexto.SaveChanges();

            return Json(employee);
        }

        

        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            //_dbContexto.Employee.Update(employee);
            //_dbContexto.SaveChanges();

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int employeeId)
        {
            Employee emp = _dbContexto.Employee.Where(x => x.EmployeeID == employeeId).FirstOrDefault<Employee>();
            _dbContexto.Employee.Remove(emp);
            _dbContexto.SaveChanges();

            return new EmptyResult();
        }

        List<Employee> GetAllEmployee()
        {
            var listaEmployee = _dbContexto.Employee.ToList();
            List<Employee> lista = new List<Employee>();
            foreach (var item in listaEmployee)
            {
                lista.Add(new Employee()
                {
                    EmployeeID = item.EmployeeID,
                    Name = item.Name,
                    Office = item.Office,
                    Position = item.Position,
                    Salary = item.Salary,
                    ImagePath = item.ImagePath
                });
            }
            return lista;
        }
    }
}