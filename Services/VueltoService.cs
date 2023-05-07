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

        public VueltoService(DivisaDA diDA, OperacionDA opDA, TipoDivisaDA tdDA, TransaccionCambioDA tcaDA)
        {
            _diDA = diDA;
            _opDA = opDA;
            _tdDA = tdDA;
            _tcaDA = tcaDA;
        }

        public List<Operacion> GetOperaciones()
        {
            List<Operacion> operaciones = _opDA.AllOperaciones();
            return operaciones;
        }

        public Operacion GetOperacionByID(int id)
        {
            Operacion operacion = _opDA.OperacionByID(id);
            return operacion;
        }

        public string VueltoQuantity(decimal montoAPagar, decimal montoPagado)
        {
            string message = "Entregar ";           
            List<TransaccionCambio> transacciones = new List<TransaccionCambio>();
            List<Divisa> divisas = _diDA.GetAllDivisas();//traigo una lista con todas las divisas que existen en la database
            
            decimal vuelto = montoPagado - montoAPagar; //vuelto al cliente
            Operacion operacionAdded = _opDA.AddOperacion(montoAPagar, montoPagado); //agrego la operacion a la tabla           

            foreach (var divisa in divisas) //voy divisa por divisa
            {
                var counter = 0;

                while (vuelto >= divisa.Valor) //si el vuelto es mayor que la divisa...
                {
                    vuelto -= divisa.Valor; //resto del vuelto el valor de la divisa...
                    counter++; // y le sumo 1 al contador asi me dice cuántas de esa divisa use
                }
                if (counter != 0) //si el contador es distinto de 0, implica que se uso esa divisa (para sacársela del vuelto)
                {
                    //creo la transaccion para registrar en la database
                    TransaccionCambio transaccion = _tcaDA.AddTransaccion(divisa.IdDivisa, counter, operacionAdded.IdOperacion);
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

                string nombreDivisa = _tdDA.DivisaById(divisaMatch.IdTipoDivisa);

                if (transacciones[i].CantidadDivisa > 1) //es mas de 1 billete o moneda
                {
                    nombreDivisa += "s";
                }

                if (i == 0)
                {
                    message = string.Concat(message, transacciones[i].CantidadDivisa, " ", nombreDivisa, " de AR$ ", divisaMatch.Valor);
                }
                else if (i == (transacciones.Count() - 1))
                {
                    message = string.Concat(message, " y ", transacciones[i].CantidadDivisa, " ", nombreDivisa, " de AR$ ", divisaMatch.Valor);
                }
                else
                {
                    message = string.Concat(message, ", ", transacciones[i].CantidadDivisa, " ", nombreDivisa, " de AR$ ", divisaMatch.Valor);
                }

            }

            if (message == "Entregar ")
            {
                message = "";
            }

            return message;
        }
    }
}

