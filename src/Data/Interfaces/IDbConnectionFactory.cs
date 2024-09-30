using System.Data;

namespace ExpenseTrackerGroup3.Data.Interfaces;

public interface IDbConnectionFactory 
{
    Task<IDbConnection> CreateConnectionAsync();
}
