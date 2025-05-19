using MediatR;
using Seguros.Application.Queries.GetAllPersonas;
using Seguros.Domain.Entities;
using Seguros.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Application.Queries.GetPersonaById
{
    public class GetPersonaByIdQueryHandler : IRequestHandler<GetPersonaByIdQuery, Persona?>
    {
        private readonly IPersonaRepository _repository;

        public GetPersonaByIdQueryHandler(IPersonaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Persona?> Handle(GetPersonaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
