using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Domain
{
    public class DataTableRelated
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Filtering { get; set; } = "";
        public string SortOrder { get; set; } = "";
    }
}
