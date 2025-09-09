using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.ViewModels
{
    public class GroupCommitteeResponseVM
    {
        public List<CustomSelectListItem> committee { get; set; }
        public List<CustomSelectListItem> members { get; set; }
        public List<GroupCommitteeVM> groupCommitteeDatas { get; set; }
    }
}
