using APIBillExchange.Context;
using APIBillExchange.Models;

namespace APIBillExchange.Data_Access
{
    public class TransaccionCambioDA
    {
        private readonly MoneyExchangeContext _context;

        public TransaccionCambioDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public TransaccionCambio AddTransaccion(int idDivisa, int contador, int idOperacion)
        {
            TransaccionCambio transaccion = new TransaccionCambio(); //creo la transaccion para registrar en la database
            transaccion.IdDivisa = idDivisa;
            transaccion.CantidadDivisa = contador;
            transaccion.IdOperacion = idOperacion;
            _context.Add(transaccion);            
            _context.SaveChanges();

            return transaccion;
        }


    }
}
