using MediatR;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.MainPurpose
{
    public class GetPurposeByIdQuery : IRequest<VwPurpose>
    {
        public int id {  get; set; }
        public GetPurposeByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
