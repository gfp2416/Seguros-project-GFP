using MediatR;
using Seguros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Application.Queries.GetPersonaById
{
    public class GetPersonaByIdQuery : IRequest<Persona?>
    {
        public Guid Id { get; set; }

        public GetPersonaByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
