using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class GroupCommitteeVM
    {
        public int Id { get; set; }
        public int CommitteePositionId { get; set; }
        //public string? CommitteePositionName { get; set; }
        public int MemberId { get; set; }
        //public string? MemberName { get; set; }
        public int GroupId { get; set; }
        //public string? GroupName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }       
    }
}
