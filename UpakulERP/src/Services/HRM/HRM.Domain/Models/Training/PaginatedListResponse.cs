using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models.Training
{
    public class PaginatedListResponse
    {
        public List<Training> listData { get; set; }
        public int TotalRecords { get; set; }

        public PaginatedListResponse(List<Training> obj, int totalRecords)
        {
            listData = obj;
            TotalRecords = totalRecords;
        }
    }

}
