using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
  public  class UpdateCodeGeneratorCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int CodeLength { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }
        public bool? IsReset { get; set; }
        public string MainJoinCode { get; set; }
        public string VirtualJoinCode { get; set; }

        //public int? UpdatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }
    }
   

}