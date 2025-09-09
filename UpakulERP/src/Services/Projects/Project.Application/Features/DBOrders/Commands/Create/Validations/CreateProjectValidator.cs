using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Project.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Constants;

namespace Project.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.ProjectTitle)
             .NotEmpty().WithMessage("Name is required.")
             .Length(1, 50).WithMessage(MessageTexts.string_length("Project Name", 50)); ;

            RuleFor(x => x.ProjectType)
              .NotEmpty().WithMessage("Type is required.")
              .Length(1, 50).WithMessage(MessageTexts.string_length("Project Type", 50));
        }
    }
}
