using Application.Layer.Interfaces;
using Application.Layer.Queries.GetAllQuery;
using Domain.Layer.DTOs;
using Domain.Layer.Models;
using MediatR;

namespace Application.Layer.Queries.GetAllToys
{
    public class GetAllToysQueryHandler : IRequestHandler<GetAllToysQuery, List<ToyDto>>
    {
        private readonly IToyRepository _toyRepository;

        public GetAllToysQueryHandler(IToyRepository toyRepository)
        {
            _toyRepository = toyRepository;
        }


        public async Task<List<ToyDto>> Handle(GetAllToysQuery request, CancellationToken cancellationToken)
        {
            var toys = await _toyRepository.GetAllAsync( cancellationToken);

            if (toys == null)
                return null;

            var toysDtos = toys.Select(toy => new ToyDto
            {
                     Id = toy.Id,
                     Name = toy.Name,
                     Price = toy.Price,
                     Series = toy.Series,
                     Brand = toy.Brand,
                     PlaySet = toy.PlaySet
              }).ToList();

            return toysDtos;
        }
    }
}
