using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Domain.Entities
{
    public class Persona
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Identificacion { get; set; }
        public int Edad {  get; set; }
        public string Genero { get; set; }
        public bool EstaActiva { get; set; }
        public string? AtributosAdicionales { get; set; } = null;
        public bool Maneja { get; set; }
        public bool UsaLentes { get; set; }
        public bool Diabetico { get; set; }
        public bool PadeceOtraEnfermedad { get; set; }
        public string? CualOtraEnfermedad { get; set; }

    }
}
