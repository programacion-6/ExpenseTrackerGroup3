using Dapper;

using Domain.Entities;

using ExpenseTrackerGroup3.Data.Interfaces;
using ExpenseTrackerGroup3.Repositories.Interfaces;

namespace ExpenseTrackerGroup3.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public BudgetRepository(IDbConnectionFactory connectionFactory)
    {
        _dbConnection = connectionFactory;
    }

    public async Task<bool> CreateAsync(Budget item)
    {
        const string sql = @"
            INSERT INTO Budget (Id, UserId, Month, BudgetAmount, AlertThreshold)
            VALUES (@Id, @UserId, @Month, @BudgetAmount, @AlertThreshold)";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sql = "DELETE FROM Budget WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<IEnumerable<Budget>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Budget";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Budget>(sql);
    }

    public async Task<Budget?> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM Budget WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Budget>(sql, new { Id = id });
    }

    public async Task<Budget?> GetMonthlyBudgetByUserId(Guid userId, DateTime month)
    {
        const string sql = @"
            SELECT * FROM Budget
            WHERE UserId = @UserId AND date_trunc('month', Month) = date_trunc('month', @Month)";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Budget>(sql, new { UserId = userId, Month = month });
    }

    public async Task<bool> UpdateAsync(Budget item)
    {
        const string sql = @"
            UPDATE Budget
            SET UserId = @UserId, Month = @Month, BudgetAmount = @BudgetAmount, AlertThreshold = @AlertThreshold
            WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }
}
