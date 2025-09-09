using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.API.Models
{
    [Table("DailyProcess", Schema = "pros")]
    public class DailyProcess
    {
        [Key]
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDayClose { get; set; }
        public DateTime? ReOpenDate { get; set; }
        public bool IsActive { get; set; }
    }
}
