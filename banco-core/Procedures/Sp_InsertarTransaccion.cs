using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_InsertarTransaccion(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
           NewTraModel tra
           )
        {
            string storeProcedure = "Sp_InsertarTransaccion";
            var parameters = new SqlParameter[]
           {
                new("@cuenta_id", SqlDbType.Int) {Value  = tra.CuentaId},
                new("@tipo_transaccion_id", SqlDbType.Int) {Value  = tra.TipoTra},
                new("@monto", SqlDbType.Decimal) {Value  = tra.Monto},
                new("@descripcion", SqlDbType.VarChar) {Value  = tra.Desc},
                new("@realizado_por", SqlDbType.Int) {Value  = tra.UserId},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, TransaccionModel.MapToModel, parameters);
        }
    }
}
