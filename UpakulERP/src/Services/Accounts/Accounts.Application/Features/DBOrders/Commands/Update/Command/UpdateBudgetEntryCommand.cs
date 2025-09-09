using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Update.Command
{

    public class UpdateBudgetEntryCommand : IRequest<CommadResponse>
    {
        public string FinancialYear { get; set; }
        public int OfficeId { get; set; }
        public int ComponentParentId { get; set; }
        public int ComponentId { get; set; }
        public int? NoOfStaff { get; set; }
        public int? NoOfPiece { get; set; }
        public int? ComponentNoOfDay { get; set; }
        public decimal ComponentPerAmount { get; set; }
        public decimal ComponentTotalAmount { get; set; }
        public int? NoOfGratuity { get; set; }
        public decimal? Basic1 { get; set; }
        public decimal? Other1 { get; set; }
        public decimal? Basic2 { get; set; }
        public decimal? Other2 { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? PF { get; set; }
        public decimal? Gratuity { get; set; }
        public decimal? MedicalAllowance { get; set; }


        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }


}
