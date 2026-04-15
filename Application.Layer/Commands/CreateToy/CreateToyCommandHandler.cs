using Application.Layer.Interfaces;
using Domain.Layer.DTOs;
using Domain.Layer.Models;
using MediatR;

namespace Application.Layer.Commands.CreateToy
{
    internal class CreateToyCommandHandler : IRequestHandler<CreateToyCommand, ToyDto>
    {
        private readonly IToyRepository _repository;

        public CreateToyCommandHandler(IToyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ToyDto> Handle(CreateToyCommand request, CancellationToken cancellationToken)
        {
            var newToy = new Toy()
            {
                Name = request.Name,
                Price = request.Price,
                PlaySet = request.PlaySet,
                Series = request.Series,
                Brand = request.Brand
            };

            await _repository.AddAsync(newToy, cancellationToken);

            return new ToyDto
            {
                Id = newToy.Id,
                Name = newToy.Name,
                Price = newToy.Price,
                Brand = newToy.Brand,
                PlaySet = newToy.PlaySet,
                Series = newToy.Series,
            };
        }
    }
}
