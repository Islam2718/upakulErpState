
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace HRM.Domain.Models
{
    
     [Table("LeaveSetup", Schema = "lve")]
    public class LeaveSetup : EntityBase
    {
        [Key]
        public int LeaveTypeId { get; set; }
        public string LeaveCategoryId { get; set; }
        public string LeaveTypeName { get; set; }
        public string EmployeeTypeId { get; set; }
        public int YearlyMaxLeave { get; set; }
        public int YearlyMaxAvailDays { get; set; }
        public string LeaveGender { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }


    }
}