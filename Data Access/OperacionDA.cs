using APIBillExchange.Context;
using APIBillExchange.Models;
using Dapper;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIBillExchange.Data_Access
{
    public class OperacionDA
    {
        private readonly MoneyExchangeContext _context;
        string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MoneyExchange;Integrated Security=True";

        public OperacionDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public List<Operacion> AllOperaciones()
        {
            //var operaciones = new List<Operacion>();
            //var queryOperaciones = "SELECT ID_Operacion AS IdOperacion, MontoAPagar AS MontoApagar, MontoPagado AS MontoPagado, FechaHora AS FechaHora FROM Operacion";

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    operaciones = connection.Query<Operacion>(queryOperaciones).ToList();
            //}            

            List<Operacion> operaciones = _context.Operacion.ToList();

            return operaciones;
        }

        public Operacion OperacionByID(int id)
        {
            //Operacion operacion = new Operacion();
            //var queryOperaciones = $"SELECT ID_Operacion AS IdOperacion, MontoAPagar AS MontoApagar, MontoPagado AS MontoPagado, FechaHora AS FechaHora FROM Operacion WHERE ID_Operacion = {id}";

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    operacion = connection.QuerySingle<Operacion>(queryOperaciones);
            //}

            Operacion operacion = _context.Operacion.FirstOrDefault(op => op.IdOperacion == id);

            return operacion;
        }

        public Operacion AddOperacion(decimal montoAPagar, decimal montoPagado) //With Dapper turned to be a little ugly but it works!
        {
            Operacion operacion = new Operacion();           
            
            var queryInsertOperaciones = @"INSERT INTO Operacion (MontoApagar, MontoPagado, FechaHora) VALUES (@MontoApagar, @MontoPagado, @FechaHora)";
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(queryInsertOperaciones, new { MontoApagar = montoAPagar, MontoPagado = montoPagado, FechaHora = DateTime.Now } /*operacion*/);
            }

            var operaciones = new List<Operacion>();
            var queryOperaciones = "SELECT ID_Operacion AS IdOperacion, MontoAPagar AS MontoApagar, MontoPagado AS MontoPagado, FechaHora AS FechaHora FROM Operacion";

            using (var connection = new SqlConnection(connectionString))
            {
                operaciones = connection.Query<Operacion>(queryOperaciones).ToList();
            }

            operacion = operaciones.LastOrDefault();


            //Operacion operacion = new Operacion();
            //operacion.MontoApagar = montoAPagar;
            //operacion.MontoPagado = montoPagado;
            //operacion.FechaHora = DateTime.Now;
            //_context.Add(operacion);
            //_context.SaveChanges();


            return operacion;


        }

    }
}
