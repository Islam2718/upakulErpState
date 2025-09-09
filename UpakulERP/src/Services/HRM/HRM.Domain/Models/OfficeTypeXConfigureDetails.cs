using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace HRM.Domain.Models
{
    [Table("OfficeTypeXConfigureDetails", Schema = "lve")]
    public class OfficeTypeXConfigureDetails : EntityBase
    {
     
            [Key]
            public int Id { get; set; }

            public int ConfigureMasterId { get; set; }

            public int ApproverDesignationId { get; set; }
            public int LevelNo { get; set; }
            public int MinimumLeave { get; set; }
            public int? MaximumLeave { get; set; }

      
        }
    }

