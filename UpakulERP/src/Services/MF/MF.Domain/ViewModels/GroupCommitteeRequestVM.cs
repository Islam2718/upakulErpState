using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class GroupCommitteeRequestVM
    {
        public int GroupId { get; set; }
        public int CommitteePositionId { get; set; }
        public int? MemberId { get; set; }
        public string? StartDate { get; set; }       
    }
}
