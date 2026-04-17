using Application.Layer.Interfaces;
using Application.Layer.Services;
using Domain.Layer.DTOs;
using Domain.Layer.Models;
using MediatR;

namespace Application.Layer.Commands.CreateToy
{
    internal class CreateToyCommandHandler : IRequestHandler<CreateToyCommand, ToyDto>
    {
        private readonly IToyRepository _repository;
        private readonly IOpenAIService _chat;

        public CreateToyCommandHandler(IToyRepository repository, IOpenAIService chat)
        {
            _repository = repository;
            _chat = chat;
        }

        public async Task<ToyDto> Handle(CreateToyCommand request, CancellationToken cancellationToken)
        {

            var summary = await _chat.GenerateSummaryAsync(request.Name, request.Series, request.Brand, cancellationToken);

            var newToy = new Toy()
            {
                Name = request.Name,
                Price = request.Price,
                PlaySet = request.PlaySet,
                Series = request.Series,
                Brand = request.Brand,
                Summary = summary
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
                Summary = newToy.Summary
            };
        }
    }
}
