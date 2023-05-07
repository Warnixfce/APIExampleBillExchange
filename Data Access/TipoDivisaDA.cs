using APIBillExchange.Context;
using APIBillExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBillExchange.Data_Access
{
    public class TipoDivisaDA
    {
        private readonly MoneyExchangeContext _context;

        public TipoDivisaDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public string DivisaById(int idTipoDivisa)
        {
            TipoDivisa tipoDivisa = _context.TipoDivisa.FirstOrDefault(td => td.IdTipoDivisa == idTipoDivisa);
            string nombreDivisa = tipoDivisa.Nombre;

            return nombreDivisa;
           
        }
    }
}
