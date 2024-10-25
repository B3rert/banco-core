using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerTarjetasPorUsuario(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
           int userId
           )
        {
            string storeProcedure = "sp_ObtenerTarjetasPorUsuario";
            var parameters = new SqlParameter[]
           {
                new("@usuarioId", SqlDbType.Int) {Value  = userId},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, TarjetaHidenModel.MapToModel, parameters);
        }
    }
}
