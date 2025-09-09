using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace Global.Domain.Models
{
    [Table("Office", Schema = "dbo")]
    public class Office : EntityBase
    {
        [Key]
        public int OfficeId { get; set; }
        public int OfficeType { get; set; }
        public string OfficeCode { get; set; }
        public string? OfficeShortCode { get; set; }
        public string OfficeName { get; set; }        
        public string? OfficeAddress { get; set; }
        public DateTime? OperationStartDate { get; set; }
        public DateTime? OperationEndDate { get; set; }
        public string? OfficeEmail { get; set; }
        public string? OfficePhoneNo { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public int? ParentId { get; set; }
    }
}
