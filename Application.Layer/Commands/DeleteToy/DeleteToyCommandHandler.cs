using Application.Layer.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Commands.DeleteToy
{
    internal class DeleteToyCommandHandler : IRequestHandler<DeleteToyCommand, Unit>
    {
        private readonly IToyRepository _repository;
        public DeleteToyCommandHandler(IToyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteToyCommand request, CancellationToken cancellationToken)
        {
             await _repository.SoftDeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
