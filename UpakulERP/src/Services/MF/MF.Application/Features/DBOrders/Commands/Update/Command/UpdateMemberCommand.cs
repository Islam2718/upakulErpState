using MediatR;
using Microsoft.AspNetCore.Http;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateMemberCommand : IRequest<CommadResponse>
    {
        public int MemberId { get; set; }
        //public int? OfficeId { get; set; }
        public int GroupId { get; set; }
        //public string MemberCode { get; set; }
        public string MemberName { get; set; }
        public string? MemberNameBn { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int? OccupationId { get; set; }
        public string MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string Gender { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? BirthCertificate { get; set; }
        public bool? BirthCertificateVerified { get; set; }
        public string? NationalId { get; set; }
        public string? SmartCard { get; set; }
        public bool? NidVerified { get; set; }
        public string? TIN { get; set; }
        public int? AcademicQualificationId { get; set; }
        public string? OtherIdType { get; set; }
        public string? OtherIdNumber { get; set; }
        public string ContactNoOwn { get; set; }
        //public bool? IsChecked { get; set; }
        public string? MobileNumber { get; set; }
        public int? MemberRemarks { get; set; }
        public int? NoOfDependents { get; set; }
        public int? AuthorizedEmployeeId { get; set; }
        public string? VerificationNote { get; set; }       
        public int PresentCountryId { get; set; }
        public int PresentDivisionId { get; set; }
        public int PresentDistrictId { get; set; }
        public int PresentUpazilaId { get; set; }
        public int PresentUnionId { get; set; }
        public int PresentVillageId { get; set; }
        public string? PresentAddress { get; set; }
        public int? PermanentCountryId { get; set; }
        public int? PermanentDivisionId { get; set; }
        public int? PermanentDistrictId { get; set; }
        public int? PermanentUpazilaId { get; set; }
        public int? PermanentUnionId { get; set; }
        public int? PermanentVillageId { get; set; }
        public string? PermanentAddress { get; set; }
        public string? IdentifierName { get; set; }
        public string? RelationWithIdentifier { get; set; }
        public int? ReferenceMemberId { get; set; }
        public string? RelationWithReferenceMember { get; set; }
        public int? IncomeAmt { get; set; }
        public string? ResidentialHouseArea { get; set; }
        public string? ArableLandArea { get; set; }
        public bool PreviouslyLoanReceiver { get; set; }
        public bool RelatedOtherProgram { get; set; }
        public bool MemberOfOtherOrganization { get; set; }
        public int? DependentPersonNo { get; set; }
        public IFormFile? MemberImg { get; set; }
        public IFormFile? SignatureImg { get; set; }
        public IFormFile? NidFrontImg { get; set; }
        public IFormFile? NidBackImg { get; set; }
        public int? AdmissionFee { get; set; }
        public int? PassbookFee { get; set; }
        public string? ApplicationNo { get; set; }
        public string? PassbookNo { get; set; }
        public int? TotalFamilyMember { get; set; }
        public int? TotalChildren { get; set; }
        public int? TotalIncome { get; set; }
        public string? IncomeType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }
}
