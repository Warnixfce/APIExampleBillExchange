using System;
using System.Collections.Generic;

namespace APIBillExchange.Models
{
    public partial class Divisa
    {
        public Divisa()
        {
            TransaccionCambios = new HashSet<TransaccionCambio>();
        }

        public int IdDivisa { get; set; }
        public int IdTipoDivisa { get; set; }
        public decimal Valor { get; set; }
        public string? Descripcion { get; set; }

        public virtual TipoDivisa IdTipoDivisaNavigation { get; set; } = null!;
        public virtual ICollection<TransaccionCambio> TransaccionCambios { get; set; }
    }
}
