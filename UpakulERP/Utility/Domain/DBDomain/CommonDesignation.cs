using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utility.Domain.DBDomain
{
    [Table("Designation", Schema = "dbo")]
    public class CommonDesignation
    {
        [Key]
        public int DesignationId { get; set; }
        public string? DesignationCode { get; set; }
        public string DesignationName { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }=true;
    }
}
