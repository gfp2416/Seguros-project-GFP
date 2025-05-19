using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Application.Commands.DeletePersona
{
    public class DeletePersonaCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
