using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
   public class UpdateComponentCommand : IRequest<CommadResponse>
    {
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
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
