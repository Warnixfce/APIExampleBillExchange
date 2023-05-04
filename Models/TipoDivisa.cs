using System;
using System.Collections.Generic;

namespace APIBillExchange.Models
{
    public partial class TipoDivisa
    {
        public TipoDivisa()
        {
            Divisas = new HashSet<Divisa>();
        }

        public int IdTipoDivisa { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Divisa> Divisas { get; set; }
    }
}
