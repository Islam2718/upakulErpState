using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.ViewModels
{
    public class MultipleDropdownForOfficeComponentMappingVM
    {
        public List<CustomSelectListItem> ComponentDropdown { get; set; }
        public List<CustomSelectListItem> BranchDropdown { get; set; }

    }
}
