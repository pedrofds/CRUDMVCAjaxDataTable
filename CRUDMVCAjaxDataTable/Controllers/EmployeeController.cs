using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CRUDMVCAjaxDataTable.Data;
using CRUDMVCAjaxDataTable.Models;
using CRUDMVCAjaxDataTable.Util;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static CRUDMVCAjaxDataTable.Util.GlobalClass;

namespace CRUDMVCAjaxDataTable.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbContexto _dbContexto;
        IHostingEnvironment _appEnvironment;

        public EmployeeController(DbContexto dbContexto, IHostingEnvironment env)
        {
            _dbContexto = dbContexto;
            _appEnvironment = env;
        }

        public IActionResult Index()
        {
            return View(GetAllEmployee());
        }

        // GET: Produto/Details/5
        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());
        }       

        IEnumerable<Employee> GetAllEmployee()
        {
            var listaEmployee = _dbContexto.Employee.ToList();
            List<Employee> lista = new List<Employee>();
            foreach (var item in listaEmployee)
            {
                lista.Add(new Employee() {
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

        public ActionResult AddOrEdit(int id = 0)
        {
            Employee emp = new Employee();
            if (id != 0)
            {
                emp = _dbContexto.Employee.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
            }
            return ViewComponent("EmployeeAddOrEdit", emp);
            //return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            try
            {
                if (emp.ImageUpload != null)
                {
                    string pasta = "foto";
                    string fileName = GetUniqueFileName(emp.ImageUpload.FileName);
                    string extension = Path.GetExtension(emp.ImageUpload.FileName);
                    //fileName = fileName + extension;
                    var uploads = Path.Combine(_appEnvironment.WebRootPath, $"images\\{pasta}");
                    var filePath = Path.Combine(uploads, fileName);
                    var pathSave = Path.GetRelativePath(_appEnvironment.WebRootPath, fileName);

                    emp.ImagePath = $"~/images/foto/{fileName}";
                    emp.ImageUpload.CopyTo(new FileStream(filePath, FileMode.Create));

                    //emp.ImageUpload.CopyTo(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                    if (emp.EmployeeID == 0)
                    {
                        _dbContexto.Employee.Add(emp);
                        _dbContexto.SaveChanges();
                    }
                    else
                    {
                        _dbContexto.Update(emp);
                        _dbContexto.SaveChanges();

                    }
                return Json(new { success = true, html = GetAllEmployee(), message = "Dados salvos com sucesso!" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Employee emp = _dbContexto.Employee.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                _dbContexto.Employee.Remove(emp);
                _dbContexto.SaveChanges();
                return Json(new { success = true, html = GetAllEmployee(), message = "Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        /* DATATABLE */
        public JsonResult CustomServerSideSearchAction(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;
            var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

            var result = new List<EmployeeViewMovel>(res.Count);
            foreach (var s in res)
            {
                // simple remapping adding extra info to found dataset
                result.Add(new EmployeeViewMovel
                {
                    EmployeeID = s.EmployeeID,
                    Name = s.Name,
                    Office = s.Office,
                    Position = s.Position,
                    Salary = s.Salary,
                    ImagePath = s.ImagePath
                });
            };

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
            });
        }    

        public IList<Employee> YourCustomSearchFunc(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            var result = GetDataFromDbase(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<Employee>();
            }
            return result;
        }

        public List<Employee> GetDataFromDbase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            // the example datatable used is not supporting multi column ordering
            // so we only need get the column order from the first column passed to us.        
            var whereClause = BuildDynamicWhereClause(_dbContexto, searchBy);

            //if (String.IsNullOrEmpty(searchBy))
            //{
            //    // if we have an empty search then just order the results by Id ascending
            //    sortBy = "EmployeeID";
            //    sortDir = true;
            //}
            var direcao = sortDir;
            var predicate = PredicateBuilder.New<Employee>(true);
            var sortfield = sortBy;
            var listaResultado = _dbContexto.Employee.Where(whereClause).ToList();
            var teste = SortExtension.OrderByDynamic(_dbContexto.Employee.Where(whereClause), sortfield, direcao);
            var result = SortExtension.OrderByDynamic(_dbContexto.Employee.Where(whereClause), sortfield, direcao)
                            .ToList()
                            .Select(m => new Employee
                            {
                                EmployeeID = m.EmployeeID,
                                Name = m.Name,
                                Office = m.Office,
                                Position = m.Position,
                                Salary = m.Salary,
                                ImagePath = m.ImagePath
                            })
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            filteredResultsCount = _dbContexto.Employee.Where(whereClause).Count();
            totalResultsCount = _dbContexto.Employee.Count();

            return result;
        }

        private Expression<Func<Employee, bool>> BuildDynamicWhereClause(DbContexto entities, string searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<Employee>(true); // true -where(true) return all
            if (String.IsNullOrWhiteSpace(searchValue) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                //var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
                //predicate = predicate.Or(s => searchTerms.Any(srch => s.Name.ToLower().Contains(srch)));

                predicate = predicate.Or(x => x.Name.Contains(searchValue));
                predicate = predicate.Or(x => x.Office.Contains(searchValue));
                predicate = predicate.Or(x => x.Position.Contains(searchValue));
            }
            return predicate;
        }

        public class EmployeeViewMovel
        {
            public int EmployeeID { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Office { get; set; }
            public Nullable<int> Salary { get; set; }
            public string ImagePath { get; set; }
        }
    }
}