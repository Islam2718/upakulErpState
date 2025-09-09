using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Office
{
    public class OfficeByPIDDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int pid {  get; set; }
        public OfficeByPIDDropdownQuery(int? pid)
        {
            this.pid = pid??0;
        }
    }
}
