using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMVCAjaxDataTable.Models
{
    public class Region
    {
        [Key]
        [MaxLength(2)]
        public string RegionId { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        public string RegionNameEnglish { get; set; }

        [Required]
        public string CountryId { get; set; }
        public Country Country { get; set; }
    }
}
