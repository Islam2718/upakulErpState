using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Domain.Models.Views
{
    [Table("vw_Office", Schema = "dbo")]
    public class VWOffice
    {
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

       public int? PrincipalOfficeId { get; set; }
       public string? PrincipalOfficeCode { get; set; }
       public string? PrincipalOfficeName { get; set; }
       public int? ZonalOfficeId { get; set; }
       public string? ZonalOfficeCode { get; set; }
       public string? ZonalOfficeName { get; set; }
       public int? RegonalOfficeId { get; set; }
       public string? RegonalOfficeCode { get; set; }
       public string? RegonalOfficeName { get; set; }
       public int? AreaOfficeId { get; set; }
       public string? AreaOfficeCode { get; set; }
       public string? AreaOfficeName { get; set; }
       public int? BranchOfficeId { get; set; }
       public string? BranchOfficeCode { get; set; }
       public string? BranchOfficeName { get; set; }

    }
}
