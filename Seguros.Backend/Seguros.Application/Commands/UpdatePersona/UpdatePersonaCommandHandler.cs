using Seguros.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Seguros.Application.Commands.UpdatePersona;

public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, Unit>
{
    private readonly IPersonaRepository _repository;
    private readonly ILogger<UpdatePersonaCommandHandler> _logger;

    public UpdatePersonaCommandHandler(IPersonaRepository repository, ILogger<UpdatePersonaCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
    {
        var persona = await _repository.GetByIdAsync(request.Id);
        if (persona == null)
        {
            _logger.LogWarning("No se encontró la persona con ID {Id} para actualizar", request.Id);
            throw new Exception("Persona no encontrada");
        }

        persona.NombreCompleto = request.NombreCompleto;
        persona.Identificacion = request.Identificacion;
        persona.Edad = request.Edad;
        persona.Genero = request.Genero;
        persona.EstaActiva = request.EstaActiva;
        persona.AtributosAdicionales = request.AtributosAdicionales;
        persona.Maneja = request.Maneja;
        persona.UsaLentes = request.UsaLentes;
        persona.Diabetico = request.Diabetico;
        persona.PadeceOtraEnfermedad = request.PadeceOtraEnfermedad;
        persona.CualOtraEnfermedad = request.CualOtraEnfermedad;

        await _repository.UpdateAsync(persona);
        _logger.LogInformation("Persona con ID {Id} actualizada correctamente", request.Id);
        return Unit.Value;
    }
}