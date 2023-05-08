using APIBillExchange.Context;
using APIBillExchange.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace APIBillExchange.Data_Access
{
    public class DivisaDA
    {
        #region Dapper
        //string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MoneyExchange;Integrated Security=True";
        #endregion

        #region EntityFrameworkCore
        private readonly MoneyExchangeContext _context;

        public DivisaDA(MoneyExchangeContext context)
        {
            _context = context;
        }
        #endregion

        public List<Divisa> GetAllDivisas()
        {
            #region Dapper
            //var divisas = new List<Divisa>();
            //var queryDivisas = "SELECT ID_Divisa AS IdDivisa, ID_Tipo_Divisa AS IdTipoDivisa, Valor AS Valor, Descripcion AS Descripcion FROM Divisa";

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    divisas = connection.Query<Divisa>(queryDivisas).ToList();
            //    divisas = divisas.OrderByDescending(d => d.Valor).ToList();
            //}
            #endregion

            #region EntityFrameworkCore
            List<Divisa> divisas = new(_context.Divisa);
            divisas = divisas.OrderByDescending(d => d.Valor).ToList();
            #endregion

            return divisas;
        }
    }
}
