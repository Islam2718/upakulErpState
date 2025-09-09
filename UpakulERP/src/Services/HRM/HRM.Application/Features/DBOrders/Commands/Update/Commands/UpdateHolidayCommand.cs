using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Commands
{
   public class UpdateHoliDayCommand : IRequest<CommadResponse>
    {
        public int HoliDayId { get; set; }
        public string HoliDayName { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

}
