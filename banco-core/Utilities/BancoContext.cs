using banco_core.Modelo;
using banco_core.Models;
using Microsoft.EntityFrameworkCore;

namespace banco_core.Utilities
{
    public class BancoContext(DbContextOptions<BancoContext> options) : DbContext(options)
    {

        // Define las tablas como propiedades DbSet
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<RolModel> Rol { get; set; }
        public DbSet<CuentaModel> Cuenta { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<TipoCuentaModel> Tipo_Cuenta { get; set; }
        public DbSet<TransaccionModel> Transaccion { get; set; }
        public DbSet<TarjetaModel> Tarjeta { get; set; }
        public DbSet<TipoTransaccionModel> Tipo_Transaccion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes configurar relaciones o restricciones en las tablas
        }
    }
}
