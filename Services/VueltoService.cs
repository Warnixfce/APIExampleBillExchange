using APIBillExchange.Context;
using APIBillExchange.Data_Access;
using APIBillExchange.Interfaces;
using APIBillExchange.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Drawing.Text;

namespace APIBillExchange.Services
{
    public class VueltoService : IVueltoService
    {
        private readonly DivisaDA _diDA;
        private readonly OperacionDA _opDA;
        private readonly TipoDivisaDA _tdDA;
        private readonly TransaccionCambioDA _tcaDA;
        //private readonly IVueltoService _vuelto;
        
        public VueltoService(DivisaDA diDA, OperacionDA opDA, TipoDivisaDA tdDA, TransaccionCambioDA tcaDA /*, IVueltoService vuelto*/)
        {
            _diDA = diDA;
            _opDA = opDA;
            _tdDA = tdDA;
            _tcaDA = tcaDA;
            //_vuelto = vuelto;
        }

        public List<Operacion> GetOperaciones()
        {
            //OperacionDA operacionDA = new OperacionDA();
            List<Operacion> operaciones = _opDA.AllOperaciones();

            return operaciones;
        }

        public Operacion GetOperacionPorID(int id)
        {
            //OperacionDA operacionDA = new OperacionDA();
            //Operacion operacion1;

            //if (_context.Operacion == null)
            //{
            //    operacion1 = null;

            //    return operacion1;
            //}
            Operacion operacion = _opDA.OperacionPorID(id);
                        
            return operacion;
        }

        public string CantidadVuelto(decimal montoPagar, decimal montoPagado/*,*/ /*MoneyExchangeContext _context,*//* Operacion operacion*/)
        {
            string mensaje = "Entregar ";            
            Operacion operacionAgregada;

            decimal vuelto = montoPagado - montoPagar; //vuelto al cliente

            //Operacion operacion = new Operacion();
            if (montoPagar != 0 && montoPagado != 0)
            {
                operacionAgregada = _opDA.AddOperacion(montoPagar, montoPagado); //agrego la operacion a la tabla
                //operacion.MontoApagar = montoPagar;
                //operacion.MontoPagado = montoPagado;
                //operacion.FechaHora = DateTime.Now;
                //_context.Add(operacion);
                //_context.SaveChanges();
            }
            else
            {
                mensaje = "Ningún monto introducido debe ser cero.";
                return mensaje;

            }

            List<TransaccionCambio> transacciones = new List<TransaccionCambio>();

            //DivisaDA divisaDA = new DivisaDA();
            List<Divisa> divisas = _diDA.GetAllDivisas();//traigo una lista con todas las divisas que existen en la database
            //List<Divisa> divisas = new List<Divisa>(_context.Divisa); 
            //divisas = divisas.OrderByDescending(d => d.Valor).ToList();

            //var operacionAsociada = _context.Operacion.FirstOrDefault(op => op.MontoApagar == montoPagar && op.MontoPagado == montoPagado && op.FechaHora == operacion.FechaHora);

            Operacion operacionAsociada = _opDA.MatchOperacion(montoPagar, montoPagado, operacionAgregada.FechaHora);
            
            foreach (var divisa in divisas) //voy divisa por divisa
            {
                var contador = 0;

                while (vuelto >= divisa.Valor) //si el vuelto es mayor que la divisa...
                {
                    vuelto -= divisa.Valor; //resto del vuelto el valor de la divisa
                    contador++; //le sumo 1 al contador asi me dice cuántas de esa divisa use
                }
                if (contador != 0) //si el contador es distinto de 0, implica que se uso esa divisa (para sacarsela del vuelto)
                {
                    //creo la transaccion para registrar en la database
                    TransaccionCambio transaccion = _tcaDA.AddTransaccion(divisa.IdDivisa, contador, operacionAsociada.IdOperacion);
                    //transaccion.IdDivisa = divisa.IdDivisa;
                    //transaccion.CantidadDivisa = contador;
                    //transaccion.IdOperacion = operacionAsociada.IdOperacion;
                    //_context.Add(transaccion);
                    //_context.SaveChanges();

                    transacciones.Add(transaccion); //la agrego a la lista de transacciones para despues crear el mensaje
                }

                if (vuelto == 0)
                {
                    break;
                }

            }



            for (int i = 0; i < transacciones.Count(); i++) //recorro la lista de transacciones 
            {
                Divisa divisaMatch = divisas.FirstOrDefault(d => d.IdDivisa == transacciones[i].IdDivisa); //me fijo cual es la divisa asociada

                string nombreDivisa = _tdDA.DivisaPorId(divisaMatch.IdDivisa);
                

                //if (divisaMatch.IdTipoDivisa == 1) //es billete
                //{
                //    tipoDivisa = " billete";
                //}
                //if (divisaMatch.IdTipoDivisa == 2) //es moneda
                //{
                //    tipoDivisa = " moneda";
                //}
                if (transacciones[i].CantidadDivisa > 1) //es mas de 1 billete o moneda
                {
                    nombreDivisa += "s";
                }

                if (i == 0)
                {
                    mensaje = string.Concat(mensaje, transacciones[i].CantidadDivisa, nombreDivisa, " de AR$ ", divisaMatch.Valor);
                }
                else if (i == (transacciones.Count() - 1))
                {
                    mensaje = string.Concat(mensaje, " y ", transacciones[i].CantidadDivisa, nombreDivisa, " de AR$ ", divisaMatch.Valor);
                }
                else
                {
                    mensaje = string.Concat(mensaje, ", ", transacciones[i].CantidadDivisa, nombreDivisa, " de AR$ ", divisaMatch.Valor);
                }

            }

            if (mensaje == "Entregar ")
            {
                mensaje = "";
            }

            return mensaje;

        }









        //divisas.FirstOrDefault(m => m.IdDivisa == transaccion.IdDivisa).IdTipoDivisa
        //            transaccion.IdDivisa.tipo



    }
}

