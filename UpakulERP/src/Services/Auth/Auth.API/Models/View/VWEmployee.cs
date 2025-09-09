using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.API.Models.View
{
    [Table(name: "vw_ActiveEmployeeCommonColumn", Schema = "emp")]
    public class VWEmployee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int OfficeType { get; set; }
        public int OfficeId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string? DepartmentName {  get; set; }
        public string? DesignationName { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalEmail { get; set; }
        public string? OfficialEmail { get; set; }
        public string? EmployeePicURL { get; set; }
    }
}
