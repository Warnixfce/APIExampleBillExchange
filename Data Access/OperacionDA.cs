using APIBillExchange.Context;
using APIBillExchange.Models;

namespace APIBillExchange.Data_Access
{
    public class OperacionDA
    {
        private readonly MoneyExchangeContext _context;        

        public OperacionDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public List<Operacion> AllOperaciones()
        {
            List<Operacion> operacion = _context.Operacion.ToList();

            return operacion;
        }

        public Operacion OperacionByID(int id)
        {
            Operacion operacion = _context.Operacion.FirstOrDefault(op => op.IdOperacion == id);

            return operacion;
        }

        public Operacion AddOperacion(decimal montoAPagar, decimal montoPagado)
        {
            Operacion operacion = new Operacion(); //creo la operacion para registrarlo en la tabla
            operacion.MontoApagar = montoAPagar;
            operacion.MontoPagado = montoPagado;
            operacion.FechaHora = DateTime.Now;
            _context.Add(operacion);
            _context.SaveChanges();

            return operacion;
        }

    }
}
