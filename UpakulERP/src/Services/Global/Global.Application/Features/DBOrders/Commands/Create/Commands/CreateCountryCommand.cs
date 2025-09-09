using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateCountryCommand : IRequest<CommadResponse>
    {

        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
