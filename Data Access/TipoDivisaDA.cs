using APIBillExchange.Context;
using APIBillExchange.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIBillExchange.Data_Access
{
    public class TipoDivisaDA
    {
        #region Dapper
        //string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MoneyExchange;Integrated Security=True";
        #endregion

        #region EntityFrameworkCore
        private readonly MoneyExchangeContext _context;

        public TipoDivisaDA(MoneyExchangeContext context)
        {
            _context = context;
        }
        #endregion

        public string DivisaById(int idTipoDivisa)
        {
            TipoDivisa tipoDivisa = new TipoDivisa();

            #region Dapper
            //var queryTipoDivisa = $"SELECT ID_Tipo_Divisa AS IdTipoDivisa, Nombre AS Nombre FROM Tipo_Divisa WHERE ID_Tipo_Divisa = {idTipoDivisa}";

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    tipoDivisa = connection.QuerySingle<TipoDivisa>(queryTipoDivisa);
            //}
            #endregion

            #region EntityFrameworkCore
            tipoDivisa = _context.TipoDivisa.FirstOrDefault(td => td.IdTipoDivisa == idTipoDivisa);
            #endregion

            string nombreDivisa = tipoDivisa.Nombre;

            return nombreDivisa;
           
        }
    }
}
