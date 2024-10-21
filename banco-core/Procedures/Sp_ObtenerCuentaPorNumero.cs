using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerCuentaPorNumero(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
           string cuenta
           )
        {
            string storeProcedure = "Sp_ObtenerCuentaPorNumero";
            var parameters = new SqlParameter[]
           {
                new("@numero_cuenta", SqlDbType.VarChar) {Value  = cuenta},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, CuentaNumeroModel.MapToModel, parameters);
        }
    }
}
