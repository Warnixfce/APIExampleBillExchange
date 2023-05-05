using APIBillExchange.Context;
using APIBillExchange.Models;

namespace APIBillExchange.Data_Access
{
    public class DivisaDA
    {
        private readonly MoneyExchangeContext _context;
 
        public DivisaDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public List<Divisa> GetAllDivisas()
        {
            List<Divisa> divisas = new (_context.Divisa);
            divisas = divisas.OrderByDescending(d => d.Valor).ToList();

            return divisas;
        }
    }
}
