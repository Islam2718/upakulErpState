using MediatR;

namespace MF.Application.Features.DBOrders.Queries.Component
{
   public class ComponentByIdQuery : IRequest<Domain.Models.Component>
    {
        public int id { get; set; }
        public ComponentByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
