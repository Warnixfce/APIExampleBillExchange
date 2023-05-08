using System;
using System.Collections.Generic;

namespace APIBillExchange.Models
{
    public partial class Operacion
    {
        public Operacion()
        {
            TransaccionCambios = new HashSet<TransaccionCambio>();
        }

        public int IdOperacion { get; set; }
        public decimal MontoApagar { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime FechaHora { get; set; }

        public virtual ICollection<TransaccionCambio> TransaccionCambios { get; set; }
    }
}
