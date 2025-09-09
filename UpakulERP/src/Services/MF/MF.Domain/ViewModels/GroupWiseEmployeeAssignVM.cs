using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
   public class GroupWiseEmployeeAssignVM
   {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeCode { get; set; }
        public string? GroupName { get; set; }
        public string? GroupCode { get; set; }
        public string? JoiningDateString { get; set; }
        
        public DateTime JoiningDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? ReleaseNote { get; set; }
    }
}
