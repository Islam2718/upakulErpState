using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupValidator()
        {
            RuleFor(x => x.GroupId)
             .NotEmpty().WithMessage("Group id is required.");

            //RuleFor(x => x.GroupCode)
            //    .NotEmpty().WithMessage("Group Member code is required.")
            //    .Length(1, 20).WithMessage(MessageTexts.string_length("Group Member code", 20));

            RuleFor(x => x.GroupName)
              .NotEmpty().WithMessage("Group Member name is required.")
               .Length(1, 100).WithMessage(MessageTexts.string_length("Group Member name", 100));

            RuleFor(x => x.GroupType)
              .NotEmpty().WithMessage("Group Member type is required.")
               .Length(1, 1).WithMessage(MessageTexts.string_length("Group Member type", 1));

            RuleFor(x => x.OfficeId)
               .NotEmpty().WithMessage("Office is required.");

            RuleFor(x =>Convert.ToDateTime(x.OpeninigDate))
                .NotEmpty().WithMessage("Opening Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Opening Date cannot be in the future.");

            RuleFor(x => Convert.ToDateTime(x.StartDate))
                .NotEmpty().WithMessage("Start Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Start Date cannot be in the future.");
            RuleFor(x => x.MeetingDay)
               .NotEmpty().WithMessage("Meeting day is required.");

            RuleFor(x => x.DivisionId)
                .NotEmpty().WithMessage("Division is required.");

            RuleFor(x => x.DistrictId)
                .NotEmpty().WithMessage("District is required.");

            RuleFor(x => x.UpazilaId)
                .NotEmpty().WithMessage("Thana/Upzila is required.");

            RuleFor(x => x.UnionId)
                .NotEmpty().WithMessage("Union is required.");

            RuleFor(x => x.VillageId)
                .NotEmpty().WithMessage("Village is required.");

        }
    }

}
