using banco_core.Modelo;
using banco_core.Models;
using banco_core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Procedures
{
    public class Sp_CrearUsuario(IConfiguration configuration) : StoredProcedureExecutor(configuration)
    {
        public async Task<RespondeModel> SpExcecute(
           UsuarioModel user
           )
        {
            string storeProcedure = "sp_CrearUsuario";
            var parameters = new SqlParameter[]
           {
                new("@usuario", SqlDbType.VarChar) {Value  = user.Usuario},
                new("@clave", SqlDbType.VarChar) {Value  = user.Clave},
                new("@rol_id", SqlDbType.TinyInt) {Value  = user.Rol_Id},
           };

            //consumo y respuesta del procedimiento
            return await ExecuteStoredProcedureAsync(storeProcedure, UsuarioModel.MapToModel, parameters);
        }
    }
}
