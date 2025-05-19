using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seguros.Application.Commands.CreatePersona;
using Seguros.Application.Commands.DeletePersona;
using Seguros.Application.Commands.UpdatePersona;
using Seguros.Application.Queries.GetAllPersonas;
using Seguros.Application.Queries.GetPersonaById;

namespace Seguros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin,Usuario")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personas = await _mediator.Send(new GetAllPersonasQuery());
            return Ok(personas);
        }

        [Authorize(Roles = "Admin,Usuario")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var persona = await _mediator.Send(new GetPersonaByIdQuery(id));
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonaCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID en la ruta no coincide con el cuerpo");

            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePersonaCommand { Id = id });
            return NoContent();
        }
    }
}