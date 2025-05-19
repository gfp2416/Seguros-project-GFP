using Seguros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Application.Services
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string GenerateJwtToken(Usuario usuario);
    }
}
