namespace banco_core.Modelo
{
    public class UsuarioModel
    {
            public int Id { get; set; }
            public string? Usuario { get; set; }
            public string? Clave { get; set; }
            public string? Correo { get; set; }
            public int Rol_Id { get; set; }

    }

}
