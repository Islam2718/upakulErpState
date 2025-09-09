using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateBankAccountMappingCommand : IRequest<CommadResponse>
    {
        public int? BankId { get; set; }
        public int? OfficeId { get; set; }
        public string BranchName { get; set; }
        public string RoutingNo { get; set; }
        public string BranchAddress { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankAccountNumber { get; set; }
        public int? AccountId { get; set; }
        
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }

}
