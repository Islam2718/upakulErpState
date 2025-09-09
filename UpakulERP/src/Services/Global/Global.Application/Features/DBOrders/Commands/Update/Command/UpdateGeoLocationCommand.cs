using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateGeoLocationCommand : IRequest<CommadResponse>
    {
        public int GeoLocationId {  get; set; }
        public string GeoLocationCode { get; set; }
        public string GeoLocationName { get; set; }
        public int GeoLocationType { get; set; }
        public int? ParentId { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }=DateTime.Now;
    }
}
