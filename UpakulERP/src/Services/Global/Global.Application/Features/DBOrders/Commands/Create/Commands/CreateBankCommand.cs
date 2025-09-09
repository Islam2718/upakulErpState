using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateBankCommand : IRequest<CommadResponse>
    {
        public string BankType { get; set; }
        public string BankShortCode { get; set; }
        public string BankName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
