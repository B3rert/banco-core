using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerTransaccionesPorRangoDeFechas(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
          int account,
          DateTime incio,
          DateTime fin

          )
        {
            string storeProcedure = "Sp_ObtenerTransaccionesPorRangoDeFechas";
            var parameters = new SqlParameter[]
           {
                new("@cuenta_id", SqlDbType.Int) {Value  = account},
                new("@fecha_inicio", SqlDbType.Date) {Value  = incio},
                new("@fecha_fin", SqlDbType.Date) {Value  = fin},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, TransaccionMesModel.MapToModel, parameters);
        }

    }
}
