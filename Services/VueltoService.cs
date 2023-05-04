using APIBillExchange.Context;
using APIBillExchange.Interfaces;
using APIBillExchange.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Drawing.Text;

namespace APIBillExchange.Services
{
    public class VueltoService : IVueltoService
    {
        private readonly MoneyExchangeContext _context;
        //private readonly IVueltoService _vuelto;

        public VueltoService(MoneyExchangeContext context /*, IVueltoService vuelto*/)
        {
            _context = context;
            //_vuelto = vuelto;
        }

        public List<Operacion> GetOperaciones()
        {
            return _context.Operacion.ToList();
        }

        public Operacion GetOperacionPorID(int id)
        {
            //Operacion operacion1;

            //if (_context.Operacion == null)
            //{
            //    operacion1 = null;

            //    return operacion1;
            //}
            Operacion operacion = _context.Operacion.FirstOrDefault(op => op.IdOperacion == id);
                        
            return operacion;
        }

        public string CantidadVuelto(decimal montoPagar, decimal montoPagado/*,*/ /*MoneyExchangeContext _context,*//* Operacion operacion*/)
        {
            string mensaje = "Entregar ";
            string tipoDivisa = "";

            decimal vuelto = montoPagado - montoPagar; //vuelto al cliente

            Operacion operacion = new Operacion(); //creo la operacion para registrarlo en la tabla
            if (montoPagar != 0 && montoPagado != 0)
            {
                operacion.MontoApagar = montoPagar;
                operacion.MontoPagado = montoPagado;
                operacion.FechaHora = DateTime.Now;
                _context.Add(operacion);
                _context.SaveChanges();
            }
            else
            {
                //TODO: modificar el logger de error
                return mensaje;

            }

            List<TransaccionCambio> transacciones = new List<TransaccionCambio>();
            List<Divisa> divisas = new List<Divisa>(_context.Divisa); //creo lista con todas las divisas que existen en la database
            divisas = divisas.OrderByDescending(d => d.Valor).ToList();

            var operacionAsociada = _context.Operacion.FirstOrDefault(op => op.MontoApagar == montoPagar && op.MontoPagado == montoPagado && op.FechaHora == operacion.FechaHora);

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
                    TransaccionCambio transaccion = new TransaccionCambio(); //creo la transaccion para registrar en la database
                    transaccion.IdDivisa = divisa.IdDivisa;
                    transaccion.CantidadDivisa = contador;
                    transaccion.IdOperacion = operacionAsociada.IdOperacion;
                    _context.Add(transaccion);
                    transacciones.Add(transaccion); //la agrego a la lista de transacciones para despues crear el mensaje
                    _context.SaveChanges();
                }

                if (vuelto == 0)
                {
                    break;
                }

            }


            for (int i = 0; i < transacciones.Count(); i++) //recorro la lista de transacciones 
            {
                Divisa divisaMatch = divisas.FirstOrDefault(d => d.IdDivisa == transacciones[i].IdDivisa); //me fijo cual es la divisa asociada

                if (divisaMatch.IdTipoDivisa == 1) //es billete
                {
                    tipoDivisa = " billete";
                }
                if (divisaMatch.IdTipoDivisa == 2) //es moneda
                {
                    tipoDivisa = " moneda";
                }
                if (transacciones[i].CantidadDivisa > 1) //es mas de 1 billete o moneda
                {
                    tipoDivisa += "s";
                }

                if (i == 0)
                {
                    mensaje = string.Concat(mensaje, transacciones[i].CantidadDivisa, tipoDivisa, " de AR$ ", divisaMatch.Valor);
                }
                else if (i == (transacciones.Count() - 1))
                {
                    mensaje = string.Concat(mensaje, " y ", transacciones[i].CantidadDivisa, tipoDivisa, " de AR$ ", divisaMatch.Valor);
                }
                else
                {
                    mensaje = string.Concat(mensaje, ", ", transacciones[i].CantidadDivisa, tipoDivisa, " de AR$ ", divisaMatch.Valor);
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

