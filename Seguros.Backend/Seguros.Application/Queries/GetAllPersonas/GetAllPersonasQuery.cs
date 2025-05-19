using MediatR;
using Seguros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Application.Queries.GetAllPersonas
{
    public class GetAllPersonasQuery: IRequest<List<Persona>>
    {
    }
}
