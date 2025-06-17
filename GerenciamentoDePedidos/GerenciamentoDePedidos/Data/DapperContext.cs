using System.Data;
using Microsoft.Data.SqlClient;

namespace GerenciamentoDePedidos.Data
{
  /// <summary>
  /// Provides a factory for creating SQL database connections using Dapper.
  /// </summary>
  public class DapperContext
  {
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="DapperContext"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public DapperContext(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("DefaultConnection")
                          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    /// <summary>
    /// Creates and returns a new SQL database connection.
    /// </summary>
    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
  }
}
