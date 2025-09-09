using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Domain.Models
{
    [Table("GroupXEmployeeAssign", Schema = "mem")]
    public class GroupWiseEmployeeAssign
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? ReleaseNote { get; set; }
        public bool IsActive { get; set; } = true;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
