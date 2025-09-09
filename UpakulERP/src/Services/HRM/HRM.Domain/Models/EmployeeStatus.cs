using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace HRM.Domain.Models
{
    [Table("EmployeeStatus", Schema = "emp")]
    public class EmployeeStatus : EntityBase
    {
        [Key]
        public int EmployeeStatusId { get; set; }
        public string EmployeeStatusName { get; set; }
        //public string EmployeeStatusValue { get; set; }
    }
}
