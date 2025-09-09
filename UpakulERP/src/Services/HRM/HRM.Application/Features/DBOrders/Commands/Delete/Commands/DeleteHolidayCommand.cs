using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Delete.Commands
{
   public class DeleteHoliDayCommand : IRequest<CommadResponse>
    {
    public int HoliDayId { get; set; }
    public bool IsActive { get; set; } = false;
    public int? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }= DateTime.Now;
}
}