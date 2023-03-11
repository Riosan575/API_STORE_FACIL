using Microsoft.Data.SqlClient;

namespace ApiStore.DB
{
    public class ConexionDB
    {
        private readonly string connectionString;
        public ConexionDB()
        {
            connectionString = "Server=DESKTOP-CQ425PM\\SQLEXPRESS; DataBase = tiendanueva; User Id=prueba;Password=12345";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
