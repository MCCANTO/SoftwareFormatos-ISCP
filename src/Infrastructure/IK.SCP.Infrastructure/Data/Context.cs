using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace IK.SCP.Infrastructure.Data;

public class Context
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public Context(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("IKSCPDbConn");
    }
    public IDbConnection CreateConnection => new SqlConnection(_connectionString);

}


