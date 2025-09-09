using MediatR;
using roject.Domain.ViewModels;

namespace Project.Application.Features.DBOrders.Queries.Doner
{
    public class DonerByIdQuery : IRequest<DonerVM>
    {
        public int id { get; set; }
        public DonerByIdQuery(int id)
        {
            this.id = id;
        }
    }

}
