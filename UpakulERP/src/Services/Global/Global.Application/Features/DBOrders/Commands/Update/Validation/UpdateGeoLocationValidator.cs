using FluentValidation;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Utility.Constants;

namespace Global.Application.Features.DBOrders.Commands.Create.Validations
{
    public class UpdateGeoLocationValidator : AbstractValidator<UpdateGeoLocationCommand>
    {
        public UpdateGeoLocationValidator()
        {
            RuleFor(x => x.GeoLocationId)
                .NotEmpty().WithMessage(MessageTexts.required_field("Id"));
            RuleFor(x => x.GeoLocationCode)
               .NotEmpty().WithMessage(MessageTexts.required_field("Code"))
               .Length(1, 10).WithMessage(MessageTexts.string_length("Code", 10));
            RuleFor(x => x.GeoLocationName)
              .NotEmpty().WithMessage(MessageTexts.required_field("Name"))
              .Length(1, 50).WithMessage(MessageTexts.string_length("Name", 50));
            RuleFor(x => x.GeoLocationType)
                .NotEmpty().WithMessage(MessageTexts.required_field("Type"));
        }
    }
}