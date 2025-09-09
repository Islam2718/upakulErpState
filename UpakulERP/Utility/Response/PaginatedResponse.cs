using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Response
{
    public class PaginatedResponse<T>
    {
        public List<T> listData { get; set; }
        public int TotalRecords { get; set; }
        public PaginatedResponse(List<T> listData, int totalRecords)
        {
            this.listData = listData;
            this.TotalRecords = totalRecords;
        }
    }
}
