using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Commands.DeleteToy
{
    public class DeleteToyCommandValidator : AbstractValidator<DeleteToyCommand>
    {

        public DeleteToyCommandValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThanOrEqualTo(0)
             .WithMessage("Not a valid id number. Must be greater than 0");
        }

    }
}
