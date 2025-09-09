using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.API.Models
{
    [Table(name: "Employee", Schema = "emp")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int OfficeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmployeeStatusId {  get; set; }
        public bool IsActive { get; set; }
    }
}
