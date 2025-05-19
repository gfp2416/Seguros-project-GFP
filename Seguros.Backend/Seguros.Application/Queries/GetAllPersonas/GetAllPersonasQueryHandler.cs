using MediatR;
using Seguros.Domain.Entities;
using Seguros.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Application.Queries.GetAllPersonas
{
    public class GetAllPersonasQueryHandler : IRequestHandler<GetAllPersonasQuery, List<Persona>>
    {
        private readonly IPersonaRepository _repository;

        public GetAllPersonasQueryHandler(IPersonaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Persona>> Handle(GetAllPersonasQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
