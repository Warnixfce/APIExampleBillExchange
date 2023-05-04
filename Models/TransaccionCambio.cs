using System;
using System.Collections.Generic;

namespace APIBillExchange.Models
{
    public partial class TransaccionCambio
    {
        public int IdTransaccionCambio { get; set; }
        public int IdDivisa { get; set; }
        public int CantidadDivisa { get; set; }
        public int IdOperacion { get; set; }
        public virtual Operacion IdOperacionNavigation { get; set; } = null!;
    }
}
