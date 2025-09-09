using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.Models
{
    [Table("OfficeComponentMapping", Schema = "prod")]
    public class OfficeComponentMapping : EntityBase
    {
        [Key]
        public int OfficeComponentMappingId { get; set; }
        public int ComponentId { get; set; }
        public int OfficeId { get; set; }
    }
}
