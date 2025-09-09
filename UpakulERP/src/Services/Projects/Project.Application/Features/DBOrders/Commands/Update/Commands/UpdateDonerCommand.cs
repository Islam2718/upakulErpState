using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Update.Commands
{
    public class UpdateDonerCommand : IRequest<CommadResponse>
    {
        public int DonerId { get; set; }
        public string DonerCode { get; set; }
        public string DonerName { get; set; }
        public int CountryId { get; set; }
        public string Location { get; set; }
        public string FirstContactPersonName { get; set; }
        public string FirstContactPersonContactNo { get; set; }
        public string SecendContactPersonName { get; set; }
        public string SecendContactPersonContactNo { get; set; }


        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
