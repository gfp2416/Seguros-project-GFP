using Microsoft.EntityFrameworkCore;
using Seguros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguros.Persistence
{
    public class SegurosDbContext: DbContext
    {
        public SegurosDbContext(DbContextOptions options)
        : base(options) { }

        public DbSet<Persona> Personas { get; set; } = null!;
        public DbSet<Usuario> Usuarios{ get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.NombreUsuario).IsUnique();
        }
    }
}
