using APIBillExchange.Context;
using APIBillExchange.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace APIBillExchange.Data_Access
{
    public class DivisaDA
    {
        private readonly MoneyExchangeContext _context;
        string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MoneyExchange;Integrated Security=True";

        public DivisaDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public List<Divisa> GetAllDivisas()
        {
            var divisas = new List<Divisa>();
            var queryDivisas = "SELECT ID_Divisa AS IdDivisa, ID_Tipo_Divisa AS IdTipoDivisa, Valor AS Valor, Descripcion AS Descripcion FROM Divisa";

            using (var connection = new SqlConnection(connectionString))
            {
                divisas = connection.Query<Divisa>(queryDivisas).ToList();
                divisas = divisas.OrderByDescending(d => d.Valor).ToList();
            }
            

            //List<Divisa> divisas = new (_context.Divisa);
            //divisas = divisas.OrderByDescending(d => d.Valor).ToList();

            return divisas;
        }
    }
}
