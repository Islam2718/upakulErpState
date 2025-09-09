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
    [Table("EmployeeType", Schema = "emp")]
    public class EmployeeType : EntityBase
    {
        [Key]
        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        //public string EmployeeTypeValue { get; set; }
    }
}
