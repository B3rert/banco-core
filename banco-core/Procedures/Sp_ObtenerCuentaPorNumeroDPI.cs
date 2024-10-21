using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerCuentaPorNumeroDPI(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
           string cuenta,
            string dpi
           )
        {
            string storeProcedure = "Sp_ObtenerCuentaPorNumeroDPI";
            var parameters = new SqlParameter[]
           {
                new("@numero_cuenta", SqlDbType.VarChar) {Value  = cuenta},
                new("@dpi", SqlDbType.VarChar) {Value  = dpi},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, CuentaNumeroModel.MapToModel, parameters);
        }
    }
}
