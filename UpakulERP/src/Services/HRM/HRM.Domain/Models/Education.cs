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
    [Table("Education", Schema = "emp")]
    public class Education : EntityBase
    {
        [Key]
        public int EducationId { get; set; }
        public string EducationName { get; set; }
        public string EducationDescription { get; set; }

    }
}
