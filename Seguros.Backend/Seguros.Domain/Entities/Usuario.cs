using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Rol { get; set; } = "Usuario";
    }
}
