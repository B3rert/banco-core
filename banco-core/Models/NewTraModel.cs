namespace banco_core.Models
{
    public class NewTraModel
    {
        public int?  CuentaId{ get; set; }
        public int?  TipoTra{ get; set; }
        public decimal?  Monto{ get; set; }
        public string?  Desc{ get; set; }
        public int? UserId { get; set; }
    }
}
