using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.Domain.Models.Views
{
    [Table("vw_Employee",Schema ="emp")]
    public class VWEmployee
    {
        public int EmployeeId { get; set; }
        public int OfficeType { get; set; }
        public int OfficeId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public int? ProjectId { get; set; }
        public string? ProjectShortName { get; set; }
        public string? ProjectTitle { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmployeeNameBn { get; set; }
        public string? EmployeePicURL { get; set; }
        public string? EmpSignatureUrl { get; set; }
        public string? FatherName { get; set; }
        public string? FatherNameBn { get; set; }
        public string? FatherOccupation { get; set; }
        public string? MotherName { get; set; }
        public string? MotherNameBn { get; set; }
        public string? MotherOccupation { get; set; }
        public string MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string? SpouseNameBn { get; set; }
        public string? SpouseNID { get; set; }
        public string? SpouseContactNo { get; set; }
        public string? SpousePicURL { get; set; }
        public string? SpouseOccupation { get; set; }
        public int? NoOfChild { get; set; }
        public DateTime? DivorcedDate { get; set; }
        public DateTime? WidowerDate { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? IdentificationMarks { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }
        public int DesignationId { get; set; }
        public string? DesignationCode { get; set; }
        public string? DesignationName { get; set; }
        public string? EmployeeType { get; set; }
        public string? EmployeeStatus { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? PermanentDate { get; set; }
        public int? CircularId { get; set; }
        public string? CircularNo { get; set; }
        public string? CircularTitle { get; set; }
        public DateTime? CircularDate { get; set; }
        //public int? AgreementPeriodInMonth { get; set; }
        //public DateTime? AgreementFromDate { get; set; }
        //public DateTime? AgreementToDate { get; set; }
        public int? GradeId { get; set; }
        public int? StepId { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public DateTime DOB { get; set; }
        public int CountryId { get; set; }

        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? BloodGroup { get; set; }
        public string? TIN { get; set; }
        public string? NID { get; set; }
        public string? NIDPicUrl { get; set; }
        public string? PassportNo { get; set; }
        public string? DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpDate { get; set; }
        public int? BankId { get; set; }
        public string? BankShortCode { get; set; }
        public string? BankName { get; set; }
        public int? BankBranchId { get; set; }
        public string? BranchName { get; set; }
        public string? BranchAddress { get; set; }
        public string? RoutingNo { get; set; }
        public string? BankAccountNo { get; set; }
        public string? PersonalEmail { get; set; }
        public string? OfficialEmail { get; set; }
        public string? PersonalContactNo { get; set; }
        public string? OfficialMobileNo { get; set; }
        public string? PresentDivision { get; set; }
        public string? PresentDistrict { get; set; }
        public string? PresentThana { get; set; }
        public string? PresentUnion { get; set; }
        public string? PresentVillage { get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentDivision { get; set; }
        public string? PermanentDistrict { get; set; }
        public string? PermanentThana { get; set; }
        public string? PermanentUnion { get; set; }
        public string? PermanentVillage { get; set; }
        public string? PermanentAddress { get; set; }
    }
}
