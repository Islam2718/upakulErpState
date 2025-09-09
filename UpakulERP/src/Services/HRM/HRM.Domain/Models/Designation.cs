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
    [Table("Designation", Schema = "emp")]
    public class Designation : EntityBase
    {
        [Key]
        public int DesignationId { get; set; }
        public string? DesignationCode { get; set; }
        public string DesignationName { get; set; }
        public int OrderNo { get; set; }



    }
}
