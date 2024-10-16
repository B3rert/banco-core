using banco_core.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace banco_core.Utilities
{
    public class StoredProcedureExecutor(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("BancoDatabase")!;

        //COnsumo generico de procedimientos
        protected async Task<RespondeModel> ExecuteStoredProcedureAsync<T>(
            string procedureName, //Procedsimiento que se sua
            Func<SqlDataReader, T> mapFunction, //Respuesta que debe retornar
            params SqlParameter[] parameters //parametros para el procedimeito
            )
        {

            try
            {
                //Instanci para la conexion
                using SqlConnection sql = new(_connectionString);
                //Instancia para el comando sql 
                using SqlCommand cmd = new(procedureName, sql);
                //Tipo de commando 
                cmd.CommandType = CommandType.StoredProcedure;

                //Agregar parametros
                cmd.Parameters.AddRange(parameters);

                //abrir conexion
                await sql.OpenAsync();

                //ejecutar procedimiento
                using var reader = await cmd.ExecuteReaderAsync();

                //Respuesta del procedimieto (tabla)
                List<T> response = [];

                //Recoorer cada registro obtenido
                while (await reader.ReadAsync())
                {
                    //agregar objeto mapeado
                    response.Add(mapFunction(reader));
                }

                return new RespondeModel() //respuesta correcta
                {
                    Success = true,
                    Data = response,
                };
            }
            catch (Exception e)
            {
                return new RespondeModel() //Respuesta incorrecta
                {
                    Success = false,
                    Data = e.Message,

                };
            }
        }
    }
}
