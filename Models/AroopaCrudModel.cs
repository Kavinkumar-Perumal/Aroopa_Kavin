using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AroopaTechnology.Models
{
    public class AroopaCrudModel
    {
        [Key]
        public int EmpId { get; set; }
        public string Employee_Name { get; set; }
        public string Department { get; set; }
        public string Sex { get; set; }
        public string Marital_Status { get; set; }
        public string Salary { get; set; }
    }
}
