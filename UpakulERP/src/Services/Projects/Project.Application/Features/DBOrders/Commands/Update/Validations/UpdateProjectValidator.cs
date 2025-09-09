using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Utility.Constants;

namespace Project.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.ProjectId)
             .NotEmpty().WithMessage("Project is required.");
            RuleFor(x => x.ProjectTitle)
             .NotEmpty().WithMessage("Name is required.")
             .Length(1, 50).WithMessage(MessageTexts.string_length("Project name", 50));

            RuleFor(x => x.ProjectType)
            .NotEmpty().WithMessage("Type is required.")
            .Length(1, 50).WithMessage(MessageTexts.string_length("Type required", 15));

           
        }
    }
}
