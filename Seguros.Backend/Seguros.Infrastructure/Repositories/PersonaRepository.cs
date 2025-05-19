using Microsoft.EntityFrameworkCore;
using Seguros.Domain.Entities;
using Seguros.Domain.Interfaces.Repositories;
using Seguros.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Infrastructure.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly SegurosDbContext _context;

        public PersonaRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Persona>> GetAllAsync() =>
            await _context.Personas.AsNoTracking().ToListAsync();

        public async Task<Persona?> GetByIdAsync(Guid id) =>
            await _context.Personas.FindAsync(id);

        public async Task<bool> ExistsByIdentificacionAsync(string identificacion) =>
            await _context.Personas.AnyAsync(p => p.Identificacion == identificacion);

        public Task UpdateAsync(Persona persona)
        {
            _context.Personas.Update(persona);
             _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
