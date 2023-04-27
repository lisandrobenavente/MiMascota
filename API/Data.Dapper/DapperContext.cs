using MySql.Data.MySqlClient;
using System.Data;

namespace Data.Dapper
{
    public static class DapperContext
    {
        public static IDbConnection CreateConnection()
            => new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));
    }
}
