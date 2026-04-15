using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Commands.CreateToy
{
    public class CreateCommandValidator : AbstractValidator<CreateToyCommand>
    {

        public CreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Must have a name"); 
            RuleFor(x => x.Series).NotEmpty().WithMessage("Must have a series");
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Must have a Brand");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Requires a price");
            RuleFor(x => x.PlaySet).NotEmpty().WithMessage("Requires a playset");
        }

    }
}