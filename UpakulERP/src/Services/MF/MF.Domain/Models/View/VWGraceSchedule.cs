using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.Models.View
{
    [Table("vw_GraceSchedule", Schema = "loan")]
    public class VWGraceSchedule
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string? Office { get; set; }
        public string? Group { get; set; }      
        public string? ApplicationNo { get; set; }
        public DateTime GraceFrom { get; set; }
        public DateTime GraceTo { get; set; }
        public bool? IsApproved { get; set; }

    }
}
