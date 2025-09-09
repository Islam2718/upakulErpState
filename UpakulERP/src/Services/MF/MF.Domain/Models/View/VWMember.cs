using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.Models.View
{
    [Table("vw_member",Schema ="mem")]
    public class VWMember
    {
        public int MemberId { get; set; }
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
        //public string? MemberNameBn { get; set; }
        //public int OfficeId { get; set; }
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
        public string? Gender { get; set; }
        public string? SpouseName { get; set; }
        public string? ContactNoOwn {  get; set; }
        public bool? IsCheckedInContactNo { get; set; }
        public string? MobileNumber {  get; set; }
        public string? NationalId { get; set; }
        public string? SmartCard { get; set; }
        public bool? NidVerified { get; set; }
        public string? BirthCertificate { get; set; }
        public bool? BirthCertificateVerified { get; set; }
        public string? MemberImgUrl { get; set; }
        public string? SignatureImgUrl { get; set; }
        public string? NidFrontImgUrl { get; set; }
        public string? NidBackImgUrl { get; set; }
        public string? PresentDivision { get; set; }
        public string? PresentDistrict { get; set; }
        public string? PresentUpazila { get; set; }
        public string? PresentUnion { get; set; }
        public string? PresentVillage { get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentDivision { get; set; }
        public string? PermanentDistrict { get; set; }
        public string? PermanentUpazila { get; set; }
        public string? PermanentUnion { get; set; }
        public string? PermanentVillage { get; set; }
        public string? PermanentAddress { get; set; }
        public string? AcademicQualification { get; set; }
        public string? Ref_MemberCode { get; set; }
        public string? Ref_MemberName { get; set; }
        public string? Ref_MemberSignatureUrl { get; set; }
        //public string?ChairmanName { get; set; }
        //public string? Chairman_SignatureUrl { get; set; }
      
        public string? MemberStatus { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsMigrated { get; set; }
        public string? MigrotionStatus { get; set; }
    }
}
