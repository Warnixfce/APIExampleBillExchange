using APIBillExchange.Context;

namespace APIBillExchange.Interfaces
{
    public interface IVueltoService
    {
        public void CantidadVuelto(decimal montoPagar, decimal montoPagado, MoneyExchangeContext _context);
    }
}
