using Microsoft.Extensions.Configuration;
using quickassist.Utils;
using System.Data;
using Npgsql;

namespace quickassist.Services.Dapper
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
           _configuration = configuration;
        }

        public IDbConnection Create()
        {
            string connectionString =
                CsnFunctions.ConnectionString(_configuration.GetConnectionString(CsnConstants.DATABASE_CONFIG_NAME) ?? string.Empty);

            return new NpgsqlConnection(connectionString);
        }

    }
}
