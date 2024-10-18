using banco_core.Models;
using Microsoft.Data.SqlClient;
using banco_core.Utilities;


namespace banco_core.Modelo
{
    public class UsuarioModel
    {
        public int? Id { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public int? Rol_Id { get; set; }
        public bool? Estado { get; set; }


        public static UsuarioModel MapToModel(SqlDataReader reader)
        {

            return new UsuarioModel()
            {
                Id = reader.GetValueOrDefault<int>("Id"),
                Usuario = reader.GetValueOrDefault<string>("Usuario"),
                Clave = reader.GetValueOrDefault<string>("Clave"),
                Rol_Id = reader.GetValueOrDefault<int>("Rol_Id"),
                Estado = reader.GetValueOrDefault<bool>("Estado"),
            };

        }



    }

}
