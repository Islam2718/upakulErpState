using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utility.Domain.DBDomain
{
    [Table("Office", Schema = "dbo")]
    public class CommonOffice
    {
        [Key]
        public int OfficeId { get; set; }
        public int OfficeType { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string? OfficeEmail { get; set; }
        public string? OfficePhoneNo { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }=true;
    }
}
