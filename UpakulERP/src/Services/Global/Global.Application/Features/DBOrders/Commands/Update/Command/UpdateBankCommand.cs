using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateBankCommand : IRequest<CommadResponse>
    {
        public int BankId { get; set; }
        public string BankType { get; set; }
        public string BankShortCode { get; set; }
        public string BankName { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }=DateTime.Now;
    }
}
