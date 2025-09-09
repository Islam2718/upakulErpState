using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models
{
    [Table("Component", Schema = "prod")]
    public class Component : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string ComponentCode { get; set; }
        public string ComponentName { get; set; }
        public int MasterComponentId { get; set; }
        public string ComponentType { get; set; }
        public string? LoanType { get; set; }
        public bool? SavingMap { get; set; }
        public string? PaymentFrequency { get; set; }
        public decimal InterestRate { get; set; }
        public int? DurationInMonth { get; set; }
        public int? NoOfInstalment { get; set; }
        public int? GracePeriodInDay { get; set; }
        public int? MinimumLimit { get; set; }
        public int? MaximumLimit { get; set; }
        public string? CalculationMethod { get; set; }
        public decimal? Latefeeperchantage { get; set; }

      
    }
}
