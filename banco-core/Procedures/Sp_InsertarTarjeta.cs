using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_InsertarTarjeta(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
          TarjetaModel tarjeta
          )
        {
            string storeProcedure = "sp_InsertarTarjeta";
            var parameters = new SqlParameter[]
           {
                new("@cuenta_id", SqlDbType.TinyInt) {Value  = tarjeta.Cuenta_id},
                new("@tipo_tarjeta_id", SqlDbType.TinyInt) {Value  = tarjeta.Tipo_tarjeta_id},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, TarjetaModel.MapToModel, parameters);
        }

    }
}
