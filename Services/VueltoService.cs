using APIBillExchange.Context;
using APIBillExchange.Data_Access;
using APIBillExchange.Interfaces;
using APIBillExchange.Models;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            List<Divisa> divisas = _diDA.GetAllDivisas();//Create a list with all the bills and coins in the DB
            
            decimal vuelto = montoPagado - montoAPagar; //Client's change (or vuelto)
            Operacion operacionAdded = _opDA.AddOperacion(montoAPagar, montoPagado); //Add the Operacion to the DB           

            foreach (var divisa in divisas) //Loop through all Divisas
            {
                var counter = 0;

                while (vuelto >= divisa.Valor) //If the amount of the change is greater than the bill's/coin's value...
                {
                    vuelto -= divisa.Valor; //Substract the bill's/coin's value from the change...
                    counter++; //And add 1 to the counter so it tells how many bills or coins of that value were used
                }
                if (counter != 0) //If the counter is different from 0, then that bill/coin was used (for subtraction from the change)
                {
                    //Add the Transaction to the DB
                    TransaccionCambio transaccion = _tcaDA.AddTransaccion(divisa.IdDivisa, counter, operacionAdded.IdOperacion);
                    transacciones.Add(transaccion); //And add that Transaction to a list of transactiones (for the afterwards message creation) to record all the transactions associated with the current operation
                }

                if (vuelto == 0)
                {
                    break;
                }

            }

            MessageCreation(transacciones, divisas, ref message);

            if (message == "Entregar ")
            {
                message = "";
            }

            return message;
        }


        public void MessageCreation(List<TransaccionCambio> transacciones, List<Divisa> divisas, ref string message)
        {           
            for (int i = 0; i < transacciones.Count(); i++) //Loop through the transacciones list
            {
                Divisa divisaMatch = divisas.FirstOrDefault(d => d.IdDivisa == transacciones[i].IdDivisa); //Check which bill/coin is associated with the transaction in place

                string nombreDivisa = _tdDA.DivisaById(divisaMatch.IdTipoDivisa).ToLower();

                if (transacciones[i].CantidadDivisa > 1) //If it's more than 1 bill or coin...
                {
                    nombreDivisa += "s"; //Add an 's' to the message, to make it plural
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
        }

    }
}

