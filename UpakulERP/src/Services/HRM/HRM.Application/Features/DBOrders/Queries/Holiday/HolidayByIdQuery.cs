using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.HoliDay
{
    public class HolidayByIdQuery : IRequest<HolidayVM>
    {
        public int id { get; set; }
        public HolidayByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
