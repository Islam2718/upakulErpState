using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class BankAccountMappingVM
    {
        public int BankAccountMappingId { get; set; }
        public int? BankId { get; set; }
        //public int? BankBranchId { get; set; }
        public int? OfficeId { get; set; }
        public string? BankName { get; set; }
       // public string? BankBranchName { get; set; }
        public string? OfficeName { get; set; }

        public string BranchName { get; set; }
        public string RoutingNo { get; set; }
        public string BranchAddress { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankAccountNumber { get; set; }
        public int? AccountId { get; set; }
        public string AccountHead { get; set; }


        public bool IsRefData { get; set; } = false;
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }



}
