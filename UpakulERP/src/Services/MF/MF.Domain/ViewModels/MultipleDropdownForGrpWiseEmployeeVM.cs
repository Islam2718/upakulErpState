using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.ViewModels
{
    public class MultipleDropdownForGrpWiseEmployeeVM
    {
        public List<CustomSelectListItem> availableGroup { get; set; }
        public List<CustomSelectListItem> releaseEmployee { get; set; }
        public List<CustomSelectListItem> assignEmployee { get; set; }
    }
}
