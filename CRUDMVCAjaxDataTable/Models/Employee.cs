using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMVCAjaxDataTable.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Office { get; set; }
        public int? Salary { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile ImageUpload { get; set; }

        public Employee()
        {
            ImagePath = "~/images/default.png";
        }
    }
}
