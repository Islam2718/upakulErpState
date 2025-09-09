using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Domain.Models.View
{
    [Table("vw_memberCommonData", Schema = "mem")]
    public class VWmemberCommonData
    {
        public int MemberId { get; set; }
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
        public string? MemberNameBn { get; set; }
        public int OfficeId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? OccupationName { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string? Gender { get; set; }
        public string? ContactNoOwn { get; set; }
        public string? MobileNumber { get; set; }
        public string? NationalId { get; set; }
        public string? SmartCard { get; set; }
        public string? MemberImgUrl { get; set; }
        public string? MemberStatus { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsCheckedInContactNo { get; set; }
        public bool? IsMigrated { get; set; }
    }
}
