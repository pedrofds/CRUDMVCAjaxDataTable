using CRUDMVCAjaxDataTable.Data;
using CRUDMVCAjaxDataTable.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastEscovaAppTeste.Controllers.Components
{
    public class EmployeeAddOrEditViewComponent : ViewComponent
    {
        private readonly DbContexto _dbContexto;

        public EmployeeAddOrEditViewComponent(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }
        public IViewComponentResult Invoke(Employee emp)
        {
            var employee = emp != null ? emp : new Employee();
            return View(employee);
        }
    }
}
