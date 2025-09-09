using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateBankAccountChequeCommand : IRequest<CommadResponse>
    {
        public int? BankAccountMappingId { get; set; }
        public string? ChequeNumberPrefix { get; set; }
        public string? ChequeNumberFrom { get; set; }
        public string? ChequeNumberTo { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
