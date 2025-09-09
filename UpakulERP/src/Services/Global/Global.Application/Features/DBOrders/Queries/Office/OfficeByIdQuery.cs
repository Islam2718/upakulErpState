using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class OfficeByIdQuery : IRequest<VWOffice>
    {
        public int id {  get; set; }
        public OfficeByIdQuery(int id)
        {
            this.id = id;
        }
    }


}
