using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models.Loan
{
    [Table("MRAPurpose", Schema = "loan")]
    public class MRAPurpose : EntityBase
    {
        [Key]
        public int Code { get; set; }
        public string? Category {  get; set; }
        public string? Subcategory {  get; set; }
        public string Name { get; set; }
        //public virtual List<Purpose> Children { get; set; }
    }
}
