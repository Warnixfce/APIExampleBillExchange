using APIBillExchange.Context;
using APIBillExchange.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIBillExchange.Data_Access
{
    public class TipoDivisaDA
    {
        private readonly MoneyExchangeContext _context;
        string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MoneyExchange;Integrated Security=True";

        public TipoDivisaDA(MoneyExchangeContext context)
        {
            _context = context;
        }

        public string DivisaById(int idTipoDivisa)
        {
            TipoDivisa tipoDivisa = new TipoDivisa();
            var queryTipoDivisa = $"SELECT ID_Tipo_Divisa AS IdTipoDivisa, Nombre AS Nombre FROM Tipo_Divisa WHERE ID_Tipo_Divisa = {idTipoDivisa}";

            using (var connection = new SqlConnection(connectionString))
            {
                tipoDivisa = connection.QuerySingle<TipoDivisa>(queryTipoDivisa);
            }


            //TipoDivisa tipoDivisa = _context.TipoDivisa.FirstOrDefault(td => td.IdTipoDivisa == idTipoDivisa);
            string nombreDivisa = tipoDivisa.Nombre;

            return nombreDivisa;
           
        }
    }
}
