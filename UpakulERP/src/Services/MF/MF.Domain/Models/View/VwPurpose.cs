using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MF.Domain.Models.View
{
    [Table("vw_Purpose", Schema = "loan")]
    public class VwPurpose
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public int? SubcategoryId { get; set; }
        public string? Subcategory { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MRAPurposeId { get; set; }
    }
}
