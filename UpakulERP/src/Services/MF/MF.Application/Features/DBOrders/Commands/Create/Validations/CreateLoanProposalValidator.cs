using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateLoanProposalValidator : AbstractValidator<CreateLoanApplicationCommand>
    {
        public CreateLoanProposalValidator()
        {
            RuleFor(x => x.GroupId)
              .NotEmpty()
              .WithMessage(MessageTexts.required_field("Group"));

            RuleFor(x => x.MemberId)
              .NotEmpty()
              .WithMessage(MessageTexts.required_field("Member"));

            RuleFor(x => x.ComponentId)
             .NotEmpty()
             .WithMessage(MessageTexts.required_field("Component"));

            RuleFor(x => x.PurposeId)
             .NotEmpty()
             .WithMessage(MessageTexts.required_field("Purpose"));

            RuleFor(x => x.ProposedBy)
             .NotEmpty()
             .WithMessage(MessageTexts.required_field("Proposed Employee"));

            RuleFor(x => x.ProposedAmount)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Proposed Amount"));

            RuleFor(x => x.ApplicationDate)
             .NotEmpty()
             .WithMessage(MessageTexts.required_field("Application Date"))
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of application cannot be in the future.");

            RuleFor(x => x.Emp_SelfFullTimeMale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Self Full Time Male"));

            RuleFor(x => x.Emp_SelfFullTimeFemale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Self Full Time Female"));

            RuleFor(x => x.Emp_SelfPartTimeMale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Self Part Time Male"));
            RuleFor(x => x.Emp_SelfPartTimeFemale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Wage Part Time Female"));
            RuleFor(x => x.Emp_WageFullTimeMale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Wage Full Time Male"));
            RuleFor(x => x.Emp_WageFullTimeFemale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Wage Full Time Female"));
            RuleFor(x => x.Emp_WagePartTimeMale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Wage Part Time Male"));
            RuleFor(x => x.Emp_WagePartTimeFemale)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Wage Part Time Female"));


            RuleFor(x => x.FirstGuarantorName)
           .NotEmpty().WithMessage(MessageTexts.required_field("1st  Guarantor Name"))
            .Length(1, 50).WithMessage(MessageTexts.string_length("1st  Guarantor Name", 50));
            RuleFor(x => x.FirstGuarantorContactNo)
           .NotEmpty().WithMessage(MessageTexts.required_field("1st  Guarantor contact no"))
            .Length(1, 20).WithMessage(MessageTexts.string_length("1st  Guarantor contact no", 20));
            RuleFor(x => x.FirstGuarantorRelation)
           .NotEmpty().WithMessage(MessageTexts.required_field("1st  Guarantor relation"))
            .Length(1, 50).WithMessage(MessageTexts.string_length("1st  Guarantor relation", 50));

            RuleFor(x => x.SecondGuarantorName)
           .NotEmpty().WithMessage(MessageTexts.required_field("2nd  Guarantor Name"))
            .Length(1, 50).WithMessage(MessageTexts.string_length("2nd  Guarantor Name", 50));
            RuleFor(x => x.SecondGuarantorContactNo)
           .NotEmpty().WithMessage(MessageTexts.required_field("2nd  Guarantor contact no"))
            .Length(1, 20).WithMessage(MessageTexts.string_length("2nd  Guarantor contact no", 20));
            RuleFor(x => x.SecondGuarantorRelation)
           .NotEmpty().WithMessage(MessageTexts.required_field("2nd  Guarantor relation"))
            .Length(1, 50).WithMessage(MessageTexts.string_length("2nd  Guarantor relation", 50));
        }
    }



}
