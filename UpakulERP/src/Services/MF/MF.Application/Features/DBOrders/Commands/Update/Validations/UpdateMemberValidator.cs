using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateMemberValidator : AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberValidator()
        {
            //RuleFor(x => x.OfficeId)
            //    .NotEmpty().WithMessage(MessageTexts.required_field("Office"));

            RuleFor(x => x.GroupId)
               .NotEmpty().WithMessage(MessageTexts.required_field("Group"));

            RuleFor(x => x.MemberName)
              .NotEmpty().WithMessage(MessageTexts.required_field("Member name"))
               .Length(1, 100).WithMessage(MessageTexts.string_length("Member name", 100));

            RuleFor(x => x.MemberNameBn)
               .Length(1, 300).WithMessage(MessageTexts.string_length("Member name", 300));

            RuleFor(x => x.AdmissionDate)
                .NotEmpty().WithMessage("Admission Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Admission Date cannot be in the future.");

            RuleFor(x => x.MaritalStatus)
             .NotEmpty().WithMessage(MessageTexts.required_field("Marital status"))
              .Length(1, 1).WithMessage(MessageTexts.string_length("Marital status", 1));

            RuleFor(x => x.SpouseName)
              .Length(1, 100).WithMessage(MessageTexts.string_length("Spouse name", 100));

            RuleFor(x => x.Gender)
            .NotEmpty().WithMessage(MessageTexts.required_field("Gender"))
             .Length(1, 1).WithMessage(MessageTexts.string_length("Gender", 1));

            RuleFor(x => x.MotherName)
            .NotEmpty().WithMessage(MessageTexts.required_field("Mother name"))
             .Length(1, 100).WithMessage(MessageTexts.string_length("Mother name", 100));
            RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage(MessageTexts.required_field("Father name"))
             .Length(1, 100).WithMessage(MessageTexts.string_length("Father name", 100));

            RuleFor(x => x.DateOfBirth)
               .NotEmpty().WithMessage("Date of birth is required.")
               .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.BirthCertificate)
          .Length(1, 25).WithMessage(MessageTexts.string_length("Birth certificate", 25));

            RuleFor(x => x.NationalId)
         .Length(1, 20).WithMessage(MessageTexts.string_length("NationalId", 20));
            RuleFor(x => x.SmartCard)
         .Length(1, 20).WithMessage(MessageTexts.string_length("Smart Card", 20));

            RuleFor(x => x.TIN)
          .Length(1, 25).WithMessage(MessageTexts.string_length("TIN", 25));
            RuleFor(x => x.OtherIdType)
         .Length(1, 12).WithMessage(MessageTexts.string_length("OtherId Type", 12));
            RuleFor(x => x.OtherIdNumber)
         .Length(1, 30).WithMessage(MessageTexts.string_length("TIN", 30));

            RuleFor(x => x.ContactNoOwn)
            .NotEmpty().WithMessage(MessageTexts.required_field("Personal contact number"))
             .Length(1, 15).WithMessage(MessageTexts.string_length("Personal contact number", 15));

            RuleFor(x => x.MobileNumber)
             .Length(1, 15).WithMessage(MessageTexts.string_length("Mobile no", 15));
            RuleFor(x => x.AuthorizedEmployeeId)
              .NotEmpty().WithMessage(MessageTexts.required_field("Authorized employee"));
            RuleFor(x => x.VerificationNote)
             .Length(1, 50).WithMessage(MessageTexts.string_length("MVerification note", 50));
            RuleFor(x => x.VerificationNote)
             .Length(1, 50).WithMessage(MessageTexts.string_length("MVerification note", 50));


            RuleFor(x => x.PresentCountryId)
              .NotEmpty().WithMessage(MessageTexts.required_field("Present country"));
            RuleFor(x => x.PresentDivisionId)
              .NotEmpty().WithMessage(MessageTexts.required_field("Present division"));
            RuleFor(x => x.PresentDistrictId)
              .NotEmpty().WithMessage(MessageTexts.required_field("Present distric"));
            RuleFor(x => x.PresentUpazilaId)
              .NotEmpty().WithMessage(MessageTexts.required_field("Present upazila"));
            RuleFor(x => x.PresentUnionId)
              .NotEmpty().WithMessage(MessageTexts.required_field("Present union"));
            RuleFor(x => x.PresentVillageId)
              .NotEmpty().WithMessage(MessageTexts.required_field("Present village"));

            RuleFor(x => x.PresentAddress)
           .Length(1, 150).WithMessage(MessageTexts.string_length("Present address", 150));
            RuleFor(x => x.PermanentAddress)
          .Length(1, 150).WithMessage(MessageTexts.string_length("Permanent address", 150));
            RuleFor(x => x.IdentifierName)
          .Length(1, 100).WithMessage(MessageTexts.string_length("Identifier name", 100));
            RuleFor(x => x.RelationWithIdentifier)
          .Length(1, 100).WithMessage(MessageTexts.string_length("RelationWith identifier", 100));
            //RuleFor(x => x.ReferenceMemberId)
            //  .NotEmpty().WithMessage("Reference member is required.");
            RuleFor(x => x.RelationWithReferenceMember)
           .Length(1, 50).WithMessage(MessageTexts.string_length("Relation with reference member", 50));


            RuleFor(x => x.ResidentialHouseArea)
          .Length(1, 100).WithMessage(MessageTexts.string_length("Residential House Area", 100));
            RuleFor(x => x.ArableLandArea)
          .Length(1, 100).WithMessage(MessageTexts.string_length("Arable Land Area", 100));
            RuleFor(x => x.ApplicationNo)
          .Length(1, 10).WithMessage(MessageTexts.string_length("Application No", 10));
            RuleFor(x => x.PassbookNo)
          .Length(1, 10).WithMessage(MessageTexts.string_length("PassbookNo", 10));
            RuleFor(x => x.IncomeType)
          .Length(1, 15).WithMessage(MessageTexts.string_length("IncomeType", 15));
            RuleFor(x => x.Latitude)
          .Length(1, 30).WithMessage(MessageTexts.string_length("Latitude", 30));
            RuleFor(x => x.Longitude)
          .Length(1, 30).WithMessage(MessageTexts.string_length("Longitude", 30));

        }
    }


}
