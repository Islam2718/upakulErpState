using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace HRM.Domain.Models
{
    [Table("OfficeTypeXConfigMaster", Schema = "lve")]
    public class OfficeTypeXConfigMaster :EntityBase
    {
        [Key]
        public int ConfigureMasterId { get; set; }

        public int OfficeTypeId { get; set; }
        public int ApplicantDesignationId { get; set; }
        public string LeaveCategoryId { get; set; }

    }
}
