using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerTransaccionesMes(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
          int account,
          int month,
          int year

          )
        {
            string storeProcedure = "sp_ObtenerTransaccionesMes";
            var parameters = new SqlParameter[]
           {
                new("@cuenta_id", SqlDbType.Int) {Value  = account},
                new("@month", SqlDbType.Int) {Value  = month},
                new("@year", SqlDbType.Int) {Value  = year},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, TransaccionMesModel.MapToModel, parameters);
        }

    }
}
