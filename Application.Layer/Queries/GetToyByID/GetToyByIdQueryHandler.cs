using Application.Layer.Interfaces;
using Domain.Layer.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Queries.GetToyByID
{
    public class GetToyByIdQueryHandler : IRequestHandler<GetToyByIdQuery, ToyDto>
    {
        private readonly IToyRepository _toyRepository;

        public GetToyByIdQueryHandler(IToyRepository toyRepository)
        {
            _toyRepository = toyRepository;
        }

      public async Task<ToyDto> Handle(GetToyByIdQuery request, CancellationToken cancellationToken)
        {
            var toy = await _toyRepository.GetByIdAsync(request.id, cancellationToken);

            if (toy == null)
                return null;

            return new ToyDto
            {
                Id = toy.Id,
                Name = toy.Name,
                Price = toy.Price,
                Series = toy.Series,
                Brand = toy.Brand,
                PlaySet = toy.PlaySet
            };
        }
    }
}
