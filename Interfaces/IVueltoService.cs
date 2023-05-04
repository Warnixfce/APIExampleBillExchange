using APIBillExchange.Context;
using APIBillExchange.Models;

namespace APIBillExchange.Interfaces
{
    public interface IVueltoService
    {
        public List<Operacion> GetOperaciones();

        public Operacion GetOperacionPorID(int id);

        public string CantidadVuelto(decimal montoPagar, decimal montoPagado/*,*//*, MoneyExchangeContext _context*/ /*Operacion operacion*/);
    }
}
