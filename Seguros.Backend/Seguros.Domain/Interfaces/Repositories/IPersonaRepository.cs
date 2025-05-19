using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguros.Domain.Entities;

namespace Seguros.Domain.Interfaces.Repositories
{
    public interface IPersonaRepository
    {
        Task<Persona?> GetByIdAsync(Guid id);
        Task<List<Persona>> GetAllAsync();
        Task AddAsync(Persona persona);
        Task UpdateAsync(Persona persona);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsByIdentificacionAsync(string identificacion);
    }
}
