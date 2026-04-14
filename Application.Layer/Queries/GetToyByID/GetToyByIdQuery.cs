using Domain.Layer.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Queries.GetToyByID
{
    public record GetToyByIdQuery(int id) : IRequest<ToyDto>
    { 
    }
}
