using banco_core.Modelo;
using banco_core.Models;
using Microsoft.EntityFrameworkCore;

namespace banco_core.Utilities
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        // Define las tablas como propiedades DbSet
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<RolModel> Rol { get; set; }
        public DbSet<CuentaModel> Cuenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes configurar relaciones o restricciones en las tablas
        }
    }
}
