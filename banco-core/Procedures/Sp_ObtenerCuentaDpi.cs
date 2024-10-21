using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerCuentaDpi(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
            string dpi
           )
        {
            string storeProcedure = "Sp_ObtenerCuentaDpi";
            var parameters = new SqlParameter[]
           {
                new("@dpi", SqlDbType.VarChar) {Value  = dpi},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, CuentaNumeroModel.MapToModel, parameters);
        }
    }
}
