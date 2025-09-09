using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace HRM.Domain.Models
{
    [Table("Department", Schema = "emp")]
   public class Department : EntityBase
    {
        [Key]
        public int DepartmentId { get; set; }
        public string? DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int OrderNo { get; set; } 
    }
}
