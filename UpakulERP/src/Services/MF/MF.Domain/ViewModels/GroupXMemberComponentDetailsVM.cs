using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class GroupXMemberComponentDetailsVM
    {
        public int GroupXMemberComponentDetailsId { get; set; }
        public int GroupId { get; set; }
        public int MemberId { get; set; }
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
                
        public int? LoanNo { get; set; }
        public int? InsNo { get; set; }
        public int? Component { get; set; }
        public DateTime? DisburseDate { get; set; }
        public int? DisburseAmt { get; set; }
        public int? OpeningOutst { get; set; }
        public int? OpeningOD { get; set; }
        public int? OpeningAdv { get; set; }
        public int? OpeningSaving { get; set; }
        public int? InsAmt { get; set; }
        public int? Att { get; set; }
        public int? LoanCollection { get; set; }
        public int? LoanRebate { get; set; }
        public int? LoanAdjust { get; set; }
        public bool? SavingsCollectionCom { get; set; }
        public bool? SavingsCollectionVol { get; set; }
        public bool? SavingsCollectionOth { get; set; }
        public int? SavingsRefundRefund { get; set; }
        public int? SavingsRefundCom { get; set; }
        public int? SavingsRefundVol { get; set; }
        public int? SavingsRefundOth { get; set; }
        public string? Ledger { get; set; }
        
    }

}
