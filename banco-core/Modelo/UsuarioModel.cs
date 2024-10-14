namespace banco_core.Modelo
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? Clave { get; set; }
        public string? Correo { get; set; }
        public int RolId { get; set; }
    }

}
