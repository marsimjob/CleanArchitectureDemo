using Domain.Layer.DTOs;
using MediatR;

namespace Application.Layer.Queries.GetAllQuery
{
    public record GetAllToysQuery : IRequest<List<ToyDto>>
    {
    }
}
