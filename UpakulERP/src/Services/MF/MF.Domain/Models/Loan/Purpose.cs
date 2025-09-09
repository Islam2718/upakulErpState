using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models.Loan
{
    [Table("Purpose", Schema = "loan")]
    public class Purpose : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MRAPurposeId { get; set; } 
        // Backend Issue
        public string? RowType { get; set; }
    }
}
