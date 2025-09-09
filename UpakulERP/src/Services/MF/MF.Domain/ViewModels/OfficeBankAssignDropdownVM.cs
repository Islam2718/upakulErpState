using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.ViewModels
{
    public class OfficeBankAssignDropdownVM
    {
        public List<CustomSelectListItem> bank { get; set; }
        public List<CustomSelectListItem> accountHead { get; set; }
    }
}
