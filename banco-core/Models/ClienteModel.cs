namespace banco_core.Models
{
    public class ClienteModel
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? Fecha_nacimiento { get; set; }
        public string? Dpi { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int? Usuario_id { get; set; }
    }
}
