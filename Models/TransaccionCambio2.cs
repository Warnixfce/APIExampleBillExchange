namespace APIBillExchange.Models
{
    public class TransaccionCambio2
    {
        public int IdTransaccionCambio { get; set; }
        public Divisa divisa { get; set; }
        public int CantidadDivisa { get; set; }
        public Operacion operacion { get; set; }
    }
}
