using CRUDMVCAjaxDataTable.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastEscovaAppTeste.Controllers.Components
{
    public class EmployeeListViewComponent : ViewComponent
    {
        private readonly DbContexto _dbContexto;

        public EmployeeListViewComponent(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }
        public IViewComponentResult Invoke()
        {
            var model = _dbContexto.Employee.ToList();
            return View(model);//"_EmployeeList", 
        }
    }
}
