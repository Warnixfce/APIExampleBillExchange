using APIBillExchange.Context;
using APIBillExchange.Models;
using Dapper;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;

namespace APIBillExchange.Data_Access
{
    public class TransaccionCambioDA
    {
        private readonly MoneyExchangeContext _context;
        string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MoneyExchange;Integrated Security=True";

        public TransaccionCambioDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public TransaccionCambio AddTransaccion(int idDivisa, int contador, int idOperacion) //With Dapper turned to be a little ugly but it works! (Again)
        {
            TransaccionCambio transaccion = new TransaccionCambio();            

            var queryInsertTransacciones = "INSERT INTO Transaccion_Cambio (ID_Divisa, CantidadDivisa, ID_Operacion) VALUES (@IdDivisa, @CantidadDivisa, @IdOperacion)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(queryInsertTransacciones, new { IdDivisa = idDivisa, CantidadDivisa = contador, IdOperacion = idOperacion });
            }

            var transacciones = new List<TransaccionCambio>();
            var queryTransacciones = "SELECT ID_Transaccion_Cambio AS IdTransaccionCambio, ID_Divisa AS IdDivisa, CantidadDivisa AS CantidadDivisa, ID_Operacion AS IdOperacion FROM Transaccion_Cambio";

            using (var connection = new SqlConnection(connectionString))
            {
                transacciones = connection.Query<TransaccionCambio>(queryTransacciones).ToList();
            }

            transaccion = transacciones.LastOrDefault();



            //TransaccionCambio transaccion = new TransaccionCambio();
            //transaccion.IdDivisa = idDivisa;
            //transaccion.CantidadDivisa = contador;
            //transaccion.IdOperacion = idOperacion;
            //_context.Add(transaccion);            
            //_context.SaveChanges();

            return transaccion;
        }


    }
}
