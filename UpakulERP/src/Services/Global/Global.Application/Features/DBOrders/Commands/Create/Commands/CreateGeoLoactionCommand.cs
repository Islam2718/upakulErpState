using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Command
{
    public class CreateGeoLoactionCommand:IRequest<CommadResponse>
    {
        public string GeoLocationCode { get; set; }
        public string GeoLocationName { get; set; }
        public int GeoLocationType { get; set; }
        public int? ParentId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }=DateTime.Now;
    }
}
