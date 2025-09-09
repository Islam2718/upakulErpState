using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.HoliDay
{
    public class HolidayQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public HolidayQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }
}

