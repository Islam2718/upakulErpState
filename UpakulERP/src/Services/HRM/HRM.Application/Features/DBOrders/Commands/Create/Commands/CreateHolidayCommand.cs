using MediatR;
using Utility.Response;
namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
   public class CreateHoliDayCommand  : IRequest<CommadResponse>
    {
    public string HoliDayName { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
}
}
