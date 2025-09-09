using MediatR;
using Microsoft.AspNetCore.Http;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateEmployeeCommand : IRequest<CommadResponse>
    {
        public int OfficeId { get; set; }
        public int? ProjectId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmployeeNameBn { get; set; }
        public string? EmployeePicURL { get; set; }
        public IFormFile? EmployeePic { get; set; }
        public string? EmpSignatureUrl { get; set; }
        public IFormFile? EmpSignature { get; set; }
        public string? FatherName { get; set; }
        public string? FatherNameBn { get; set; }
        public int? FatherOccupation { get; set; }
        public string? MotherName { get; set; }
        public string? MotherNameBn { get; set; }
        public int? MotherOccupation { get; set; }
        public string MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string? SpouseNameBn { get; set; }
        public string? SpouseNID { get; set; }
        public string? SpouseContactNo { get; set; }
        public string? SpousePicURL { get; set; }
        public IFormFile? SpousePic { get; set; }
        public int? SpouseOccupation { get; set; }
        public int? NoOfChild { get; set; }
        public DateTime? DivorcedDate { get; set; }
        public DateTime? WidowerDate { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? IdentificationMarks { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public string EmployeeTypeId { get; set; }
        public string EmployeeStatusId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? PermanentDate { get; set; }
        public int? CircularId { get; set; }
        //public int? AgreementPeriodInMonth { get; set; }
        //public DateTime? AgreementFromDate { get; set; }
        //public DateTime? AgreementToDate { get; set; }
        //public int? GradeId { get; set; }
        //public int? StepId { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public DateTime DOB { get; set; }
        public int CountryId { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? BloodGroup { get; set; }
        public string? TIN { get; set; }
        public string NID { get; set; }
        public IFormFile? NIDPic { get; set; }
        public string? NIDPicUrl { get; set; }
        public string? PassportNo { get; set; }
        public string? DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpDate { get; set; }
        public int? BankId { get; set; }
        public int? BankBranchId { get; set; }
        public string? BankAccountNo { get; set; }
        public string? PersonalEmail { get; set; }
        public string? OfficialEmail { get; set; }
        public string? PersonalContactNo { get; set; }
        public string? OfficialMobileNo { get; set; }
        public int? PresentDivisionId { get; set; }
        public int? PresentDistrictId { get; set; }
        public int? PresentThanaId { get; set; }
        public int? PresentUnionId { get; set; }
        public int? PresentVillageId { get; set; }
        public string? PresentAddress { get; set; }
        public int? PermanentDivisionId { get; set; }
        public int? PermanentDistrictId { get; set; }
        public int? PermanentThanaId { get; set; }
        public int? PermanentUnionId { get; set; }
        public int? PermanentVillageId { get; set; }
        public string? PermanentAddress { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
