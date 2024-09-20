using System.Data;

namespace ExpenseTrackerGroup3.Data;

public interface IDbConnectionFactory 
{
    Task<IDbConnection> CreateConnectionAsyn();
}
