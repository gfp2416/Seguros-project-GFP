using MediatR;
using Microsoft.Extensions.Logging;
using Seguros.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Application.Commands.DeletePersona
{

    public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, Unit>
    {
        private readonly IPersonaRepository _repository;
        private readonly ILogger<DeletePersonaCommandHandler> _logger;

        public DeletePersonaCommandHandler(IPersonaRepository repository, ILogger<DeletePersonaCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = await _repository.GetByIdAsync(request.Id);
            if(persona == null)
            {
                _logger.LogWarning("No se encontró la persona con ID {Id} para eliminar", request.Id);
                throw new Exception("Persona no encontrada");
            }
            await _repository.DeleteAsync(request.Id);
            _logger.LogInformation("Persona con ID {Id} eliminada exitosamente", request.Id);
            return Unit.Value;
        }
    }
}
