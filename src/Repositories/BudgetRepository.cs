using Dapper;

using Domain.Entities;

using ExpenseTrackerGroup3.Data;
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

        using var connection = await _dbConnection.CreateConnectionAsyn();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sql = "DELETE FROM Budget WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsyn();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<IEnumerable<Budget>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Budget";

        using var connection = await _dbConnection.CreateConnectionAsyn();
        return await connection.QueryAsync<Budget>(sql);
    }

    public async Task<Budget> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM Budget WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsyn();
        return await connection.QuerySingleOrDefaultAsync<Budget>(sql, new { Id = id });
    }

    public Task<Budget> GetMonthlyBudgetByUserId(Guid userId, DateTime month)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Budget item)
    {
        const string sql = @"
            UPDATE Budget
            SET UserId = @UserId, Month = @Month, BudgetAmount = @BudgetAmount, AlertThreshold = @AlertThreshold
            WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsyn();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }
}
