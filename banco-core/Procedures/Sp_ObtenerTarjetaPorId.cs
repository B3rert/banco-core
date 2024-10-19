using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_ObtenerTarjetaPorId(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
           int id
           )
        {
            string storeProcedure = "sp_ObtenerTarjetaPorId";
            var parameters = new SqlParameter[]
           {
                new("@tarjetaId", SqlDbType.Int) {Value  = id},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, TarjetaShowModel.MapToModel, parameters);
        }
    }
    }
