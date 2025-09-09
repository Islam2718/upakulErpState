using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Domain
{
    public class Personal
    {
        public string userId { get; set; }
        public int employeeId { get; set; }
        public int office_type_id { get; set; }
        public string office_type {  get; set; }
        public string emp_code { get; set; }
        public string emp_name { get; set; }
        public string office_name { get; set; }
        public string? email { get; set; }
        public string image_url { get; set; }
    }
}
