using Domain.Layer.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Commands.CreateToy
{
    public record CreateToyCommand : IRequest<ToyDto>
    {
        public string Name { get; set; }
        public string Series { get; set; }
        public string PlaySet { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }
}
