using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace HRM.Domain.Models
{
    [Table("BoardUniversity", Schema = "emp")]
    public class BoardUniversity : EntityBase
    {
        [Key]
        public int BUId { get; set; }
        public string BUName { get; set; }
    }
}
