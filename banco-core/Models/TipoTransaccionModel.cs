namespace banco_core.Models
{
    public class TipoTransaccionModel
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? Es_credito { get; set; }

    }
}
