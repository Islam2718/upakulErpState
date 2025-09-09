namespace MF.Domain.ViewModels
{
   public class ComponentVM
    {
        public int Id { get; set; }
        public int MasterComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentCode { get; set; }
        public string ComponentType { get; set; }
        public string PaymentFrequency { get; set; }
        public decimal InterestRate { get; set; }
        public int? NoOfInstalment { get; set; }
        public int? GracePeriod { get; set; }
        public int MinimumLimit { get; set; }
        public int MaximumLimit { get; set; }
        public string CalculationMethod { get; set; }
        public decimal Latefeeperchantage { get; set; }
    }
}
