using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerTransaccionesPorAutorizacionYCredito(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
          string autorizacion

          )
        {
            string storeProcedure = "Sp_ObtenerTransaccionesPorAutorizacionYCredito";
            var parameters = new SqlParameter[]
           {
                new("@numero_autorizacion", SqlDbType.VarChar, 12) {Value  = autorizacion},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, TransaccionModel.MapToModel, parameters);
        }
    }
}
