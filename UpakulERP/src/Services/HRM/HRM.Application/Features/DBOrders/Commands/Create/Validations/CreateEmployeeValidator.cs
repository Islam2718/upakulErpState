using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.EmployeeCode)
              .NotEmpty()
              .WithMessage("File no is required.");
            RuleFor(x => x.OfficeId)
              .NotEmpty()
              .WithMessage("Office is required.");
            RuleFor(x => x.FirstName)
              .NotEmpty()
              .WithMessage("First name is required.");
            RuleFor(x => x.FatherName)
                .NotEmpty()
                .WithMessage("Father name is required.");
        }
    }
}
