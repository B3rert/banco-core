using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_InsertarCuenta(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
           CuentaModel cuenta
           )
        {
            string storeProcedure = "sp_InsertarCuenta";
            var parameters = new SqlParameter[]
           {
                new("@cliente_id", SqlDbType.TinyInt) {Value  = cuenta.Cliente_id},
                new("@tipo_cuenta_id", SqlDbType.TinyInt) {Value  = cuenta.Tipo_cuenta_id},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, CuentaModel.MapToModel, parameters);
        }
    }
}
