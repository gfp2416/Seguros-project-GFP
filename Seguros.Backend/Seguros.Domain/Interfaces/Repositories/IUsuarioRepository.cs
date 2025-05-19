using Seguros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario?> GetByUsernameAsync(string username);
        Task AddAsync(Usuario usuario);
    }
}
