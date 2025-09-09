using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utility.Domain.DBDomain
{
    [Table("Employee", Schema = "dbo")]
    public class CommonEmployee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int OfficeId { get; set; }
        public int? ProjectId { get; set; }
        public int? DesignationId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeFullName { get; set; }
        public string? EmployeeNameBn     { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string Gender { get; set; }
        //public int? ParentId { get; set; }
        public bool IsActive { get; set; }
    }
}
