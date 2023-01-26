using System.Data;
using AdoNetCore.AseClient;
using Microsoft.Data.SqlClient;

namespace DapperApi2022.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
            => new AseConnection(_connectionString);
    }
}
