using Microsoft.Data.SqlClient;
using System.Data;

namespace Vaquinha.Repository.Provider
{
    public sealed class VaquinhaOnLineDbConnectionProvider
    {
        private readonly string _connection;
        public VaquinhaOnLineDbConnectionProvider(string connection)
        {
            _connection = connection;
        }
        public IDbConnection GetConnection() => new SqlConnection(_connection);
    }
}