using System.Data;

using Npgsql;

namespace ExpenseTrackerGroup3.Data;

public class DbConnection : IDisposable
{
    private readonly string _connectionString;
    private readonly IDbConnection _dbConnection;

    public DbConnection(string connectionString)
    {
        _connectionString = connectionString;
        _dbConnection = new NpgsqlConnection(_connectionString);
    }

    public IDbConnection CreateConnection()
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            _dbConnection.Open();
        }
        return _dbConnection;
    }

    public void Dispose()
    {
        if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
        {
            _dbConnection.Close();
        }
        _dbConnection?.Dispose();
    }
}
