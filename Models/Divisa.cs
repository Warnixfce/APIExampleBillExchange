using System;
using System.Collections.Generic;

namespace APIBillExchange.Models
{
    public partial class Divisa
    {
        public int IdDivisa { get; set; }
        public int IdTipoDivisa { get; set; }
        public decimal Valor { get; set; }
        public long Stock { get; set; }
        public string? Descripcion { get; set; }
    }
}
