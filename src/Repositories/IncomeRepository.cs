using Dapper;

using Domain.Entities;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Data.Interfaces;

namespace ExpenseTrackerGroup3.Repositories;

public class IncomeRepository : IIncomeRepository
{
    private readonly IDbConnectionFactory _dbConnection;
    public IncomeRepository(IDbConnectionFactory connectionFactory)
    {
        _dbConnection = connectionFactory;
    }

    public async Task<bool> CreateAsync(Income item)
    {
        const string sql = @"
            INSERT INTO Income (Id, UserId,
                                    Amount,
                                    Month, 
                                    Source,
                                    CreatedAt)
            VALUES (@Id,
                    @UserId, 
                    @Amount, 
                    @Month, 
                    @Source, 
                    @CreatedAt)";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sql = "DELETE FROM Income WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<IEnumerable<Income>> GetAllAsync()
    {
      const string sql = "SELECT * FROM Income";

      using var connection = await _dbConnection.CreateConnectionAsync();
      return await connection.QueryAsync<Income>(sql);   
    }

    public async Task<Income?> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM Income WHERE ID = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Income>(sql, new { Id = id });
    }

    public async Task<Income?> GetMonthlyIncomeByUserId(Guid userId, DateTime month)
    {
        const string sql = @"
            SELECT * FROM Income
            WHERE UserId = @UserId AND date_trunc('month', Month) = date_trunc('month', @Month)";
        
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Income>(sql, new { UserId = userId, Month = month });
    }

    public async Task<bool> UpdateAsync(Income item)
    {
        const string sql = @"
            UPDATE Income
            SET UserId = @UserId,
              Amount = @Amount,
              Month = @Month,
              Source = @Source, 
              CreatedAt = @CreatedAt
            WHERE Id = @Id";
        
        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }

}
