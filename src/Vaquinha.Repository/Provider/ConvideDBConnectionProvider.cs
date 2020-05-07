using Microsoft.Data.SqlClient;
using System.Data;

namespace Vaquinha.Repository.Provider
{
    public sealed class ConvideDBConnectionProvider
    {
        private readonly string _connection;
        public ConvideDBConnectionProvider(string connection)
        {
            _connection = connection;
        }
        public IDbConnection GetConnection() => new SqlConnection(_connection);
    }
}