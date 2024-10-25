namespace banco_core.Models
{
    public class PaymentRequest
    {
        public string? CardNumber { get; set; }  // Número de la tarjeta
        public string? CVV { get; set; }         // Código de seguridad
        public DateTime ExpirationDate { get; set; }  // Fecha de vencimiento
        public decimal Amount { get; set; }     // Monto de la transacción
        public string? DestinationAccount { get; set; } // Cuenta destino para el crédito
    }
}
