using APIBillExchange.Context;
using APIBillExchange.Models;

namespace APIBillExchange.Interfaces
{
    public interface IVueltoService
    {
        public List<Operacion> GetOperaciones();

        public Operacion GetOperacionByID(int id);

        public string VueltoQuantity(decimal montoPagar, decimal montoPagado);
    }
}
