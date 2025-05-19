using Seguros.Domain.Entities;
using Seguros.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Seguros.Application.Commands.DeletePersona;

namespace Seguros.Application.Commands.CreatePersona
{
    public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, Guid>
    {
        private readonly IPersonaRepository _repository;
        private readonly ILogger<CreatePersonaCommandHandler> _logger;

        public CreatePersonaCommandHandler(IPersonaRepository repository, ILogger<CreatePersonaCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = new Persona
            {
                Id = Guid.NewGuid(),
                NombreCompleto = request.NombreCompleto,
                Identificacion = request.Identificacion,
                Edad = request.Edad,
                Genero = request.Genero,
                EstaActiva = request.EstaActiva,
                AtributosAdicionales = request.AtributosAdicionales,
                Maneja = request.Maneja,
                UsaLentes = request.UsaLentes,
                Diabetico = request.Diabetico,
                PadeceOtraEnfermedad = request.PadeceOtraEnfermedad,
                CualOtraEnfermedad = request.CualOtraEnfermedad
            };
            
            await _repository.AddAsync(persona);

            _logger.LogInformation("Se creó la persona con ID {Id} correctamente", persona.Id);
            return persona.Id;
        }
    }
}
