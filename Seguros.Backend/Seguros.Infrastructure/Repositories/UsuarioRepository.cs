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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SegurosDbContext _context;

        public UsuarioRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == username);
        }

        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
