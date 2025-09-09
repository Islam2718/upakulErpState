using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models
{
    [Table("GraceSchedule", Schema = "loan")]
    public class GraceSchedule:EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? Reason { get; set; }
        public int? OfficeId { get; set; }
        public int? GroupId{ get; set; }
        public int? LoanId { get; set; }
        public DateTime GraceFrom { get; set; }
        public DateTime GraceTo { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    
    }
}
