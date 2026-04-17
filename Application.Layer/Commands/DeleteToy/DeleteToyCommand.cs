using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Commands.DeleteToy
{
    public record DeleteToyCommand(int Id) : IRequest<Unit>
    { }
}
