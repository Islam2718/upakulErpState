using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Group
{
    public class GroupByIdQuery : IRequest<SamityVM>
    {
        public int id { get; set; }
        public GroupByIdQuery(int id)
        {
            this.id = id;
        }
    }


}
