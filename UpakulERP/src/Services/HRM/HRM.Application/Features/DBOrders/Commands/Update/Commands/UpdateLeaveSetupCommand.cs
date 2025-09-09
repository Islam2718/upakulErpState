using MediatR;

using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Commands
{
   public class UpdateLeaveSetupCommand : IRequest<CommadResponse>
    {
        public int LeaveTypeId { get; set; }
        public string LeaveCategoryId { get; set; }
        public string LeaveTypeName { get; set; }
        public string EmployeeTypeId { get; set; }
        public int YearlyMaxLeave { get; set; }
        public int YearlyMaxAvailDays { get; set; }
        public string? LeaveGender { get; set; }
        public string EffectiveStartDate { get; set; }
        public string? EffectiveEndDate { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }

}